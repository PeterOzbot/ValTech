using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncVirtualization.Library {
    /// <summary>
    /// Collection supporting data virtualization and loads data asynchronous
    /// </summary>
    public class AsyncCollection : IList<IElement>, IList, INotifyCollectionChanged, INotifyPropertyChanged {
        private IElement[] _items;// internal list, null if first load did not happen yet

        private Task _loadingTask = null;

        private int _count = 0;

        private IDataProvider _dataProvider;

        // page size
        private readonly int PageSize = 5000;

        private object _lockObject = new object();

        private List<int> _pageLoaded;


        /// <summary>
        /// Task used with loading. Checking this can be known if data is loading.
        /// </summary>
        public Task LoadingTask { get { return _loadingTask; } }
        /// <summary>
        /// CollectionChanged event handler
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        /// <summary>
        /// Current count.
        /// Faked, always tells there is one more item left if there are any left. So data visualization is working.
        /// </summary>
        public int Count {
            get {
                // if first load did not yet happen
                if (_items == null && (_loadingTask == null || _loadingTask.IsCompleted)) {
                    Load();
                }

                // return count
                return _count;
            }
        }

        /// <summary>
        /// Indexer returning requested items. If item is not yet in internal collection then tries to get it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IElement this[int index] {
            get {

                // if first load did not yet happen 
                if (_items == null) {
                    if (_loadingTask == null || _loadingTask.IsCompleted) {
                        Load();
                    }
                }
                // if element is in internal collection just return it
                else if (index < _items.Length) {
                    if (_items[index] != null) {
                        return _items[index];
                    }
                }

                LoadNext(index);

                // if no element was returned return fake element used to indicate that data is loading
                return new LoadingElement(index);
            }
            set {
                throw new NotImplementedException();
            }
        }





        /// <summary>
        /// Creates a new instance of the  <see cref="AsyncCollection"/> class.
        /// </summary>
        public AsyncCollection(IDataProvider dataProvider, int pageSize = 0) {
            _dataProvider = dataProvider;
            if (pageSize != 0) {
                PageSize = pageSize;
            }
            _pageLoaded = new List<int>();
        }



        /// <summary>
        /// Loads data
        /// </summary>
        private void Load() {
            lock (_lockObject) {
                _loadingTask = Task.Factory.StartNew(() => {
                    // get data
                    int index = 0;

                    _items = new IElement[PageSize];

                    IEnumerable<IElement> elements = _dataProvider.Get(0, PageSize);

                    if (elements.Count() > PageSize) { throw new ArgumentOutOfRangeException("Data Provider returned more items that it should."); }

                    foreach (IElement element in elements) {
                        _items[index] = element;
                        index++;
                    }
                    //_items = new List<IElement>(_dataProvider.Get(0));


                    // get count
                    _count = _dataProvider.GetCount();
                }).ContinueWith((previousTask) => {
                    OnCollectionReset();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        /// <summary>
        /// Loads data
        /// </summary>
        private void LoadNext(int index) {
            lock (_lockObject) {
                _loadingTask = _loadingTask.ContinueWith<BatchInfo>((previousTask) => {
                    // calculate range for page in which element with index is
                    int page = (index / PageSize) + 1;
                    int firstElementIndexInPage = ((page - 1) * PageSize);
                    int newLength = page * PageSize;


                    // if page was already loaded or it is loading then do nothing here
                    if (_pageLoaded.Contains(page)) {
                        return null;
                    }
                    else {
                        _pageLoaded.Add(page);
                    }

                    // enlarge internal array if needed
                    if (index > _items.Length - 1) {
                        IElement[] oldList = new IElement[_items.Length];
                        Array.Copy(_items, oldList, _items.Length);

                        _items = new IElement[newLength];
                        Array.Copy(oldList, _items, oldList.Length);
                    }

                    BatchInfo batchInfo = new BatchInfo(firstElementIndexInPage);

                    int elementIndex = firstElementIndexInPage;
                    foreach (IElement element in _dataProvider.Get(firstElementIndexInPage, PageSize)) {
                        //_items.Add(element);
                        _items[elementIndex] = element;
                        elementIndex++;
                        batchInfo.Elements.Add(element);
                    }

                    return batchInfo;

                }).ContinueWith((previousTask) => {

                    BatchInfo batchInfo = previousTask.Result;
                    if (batchInfo == null) { return; }

                    int latestCount = batchInfo.PreviousCount;

                    foreach (IElement element in batchInfo.Elements) {
                        OnCollectionChanged(element, new LoadingElement(0), latestCount);
                        latestCount++;
                    }

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }


        /// <summary>
        /// Triggers collection changed with reset flag.
        /// </summary>
        private void OnCollectionReset() {
            if (CollectionChanged != null) {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Triggers collection changed with replace flag, with element and its position
        /// </summary>
        private void OnCollectionChanged(IElement element, IElement oldElement, int index) {
            if (CollectionChanged != null) {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, element, oldElement, index));
            }
        }

        #region -=Interface methods/not used=-
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <remarks>
        /// This method should be avoided on large collections due to poor performance.
        /// </remarks>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IElement> GetEnumerator() {
            for (int i = 0 ; i < Count ; i++) {
                yield return this[i];
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        /// <summary>
        /// Just redirect call to other this[index]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object IList.this[int index] {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(object value) {
            return IndexOf(value as IElement);
        }
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(IElement item) {
            return -1;
        }
        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot {
            get { return this; }
        }
        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>Always false.
        /// </returns>
        public bool IsSynchronized {
            get { return false; }
        }
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>Always true.
        /// </returns>
        public bool IsReadOnly {
            get { return true; }
        }
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </summary>
        /// <value></value>
        /// <returns>Always false.
        /// </returns>
        public bool IsFixedSize {
            get { return false; }
        }
        public void Insert(int index, IElement item) {
            throw new NotImplementedException();
        }
        public void RemoveAt(int index) {
            throw new NotImplementedException();
        }
        public void Add(IElement item) {
            throw new NotImplementedException();
        }
        public void Clear() {
            throw new NotImplementedException();
        }
        public bool Contains(IElement item) {
            throw new NotImplementedException();
        }
        public void CopyTo(IElement[] array, int arrayIndex) {
            throw new NotImplementedException();
        }
        public bool Remove(IElement item) {
            throw new NotImplementedException();
        }
        public int Add(object value) {
            throw new NotImplementedException();
        }
        public bool Contains(object value) {
            //throw new NotImplementedException();
            return false;
        }
        public void Insert(int index, object value) {
            throw new NotImplementedException();
        }
        public void Remove(object value) {
            throw new NotImplementedException();
        }
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Class representing new batch of items
        /// </summary>
        private class BatchInfo {
            /// <summary>
            /// New items
            /// </summary>
            public List<IElement> Elements { get; set; }
            /// <summary>
            /// Count of elements before the new batch
            /// </summary>
            public int PreviousCount { get; set; }



            /// <summary>
            /// Creates a new instance of the  <see cref="BatchInfo"/> class.
            /// </summary>
            public BatchInfo(int previousCount) {
                Elements = new List<IElement>();
                PreviousCount = previousCount;
            }
        }
    }

    /// <summary>
    /// Defines element in the collection
    /// </summary>
    public interface IElement { }

    /// <summary>
    /// Event used to show loading is in progress when next batch is loading
    /// </summary>
    public class LoadingElement : IElement {
        public int ID { get; private set; }


        /// <summary>
        /// Creates Loading event with ID as index
        /// </summary>
        /// <param name="index"></param>
        public LoadingElement(int index) { ID = index; }
    }
}
