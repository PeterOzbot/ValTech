using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncVirtualization.Library;

namespace Async_Data_and_UI_Virtualization_Collection {
    /// <summary>
    /// Data provider used for example
    /// </summary>
    public class SampleDataProvider : IDataProvider {
        private List<IElement> _data;

        /// <summary>
        /// Creates a new instance of the  <see cref="SampleDataProvider"/> class.
        /// </summary>
        public SampleDataProvider() {
            _data = new List<IElement>();
            for (int i = 0 ; i < 200000 ; i++) {
                _data.Add(new SampleElement(i));
            }
        }

        #region IDataProvider Members

        public IEnumerable<IElement> Get(int elementIndex, int count = 100) {
            //Task.Delay(2500).Wait();
            Trace.TraceInformation(elementIndex.ToString());
            return _data.Skip(elementIndex).Take(count);
        }

        public int GetCount() {
            return _data.Count;
        }

        #endregion
    }
    /// <summary>
    /// Element used for the example
    /// </summary>
    public class SampleElement : IElement {
        public int ID { get; private set; }
        /// <summary>
        /// Just some identification
        /// </summary>
        public string Label { get; private set; }


        /// <summary>
        /// Creates a new instance of the  <see cref="SampleElement"/> class.
        /// </summary>
        public SampleElement(int index) {
            ID = index;
            Label = String.Format("Element {0}", index);
        }

        public override string ToString() {
            return Label;
        }
    }
}
