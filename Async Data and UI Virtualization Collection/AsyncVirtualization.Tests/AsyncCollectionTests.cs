using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncVirtualization.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsyncVirtualization.Tests {
    [TestClass]
    public class AsyncCollectionTests {

        [TestInitialize()]
        public void Initialize() {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        /// <summary>
        /// Test first load on fast multiple get for count
        /// </summary>
        [TestMethod]
        public void FirstLoadCountGetMultipleTest() {
            AsyncCollection asyncCollection = new AsyncCollection(new SlowDataProvider());

            int count = asyncCollection.Count;
            Assert.AreEqual(0, count);
            count = asyncCollection.Count;
            Assert.AreEqual(0, count);
            count = asyncCollection.Count;
            Assert.AreEqual(0, count);
        }


        /// <summary>
        /// Test indexer and how data is loaded on fast multiple gets.
        /// </summary>
        [TestMethod]
        public void IndexerGetMultipleTest() {
            AsyncCollection asyncCollection = new AsyncCollection(new SlowDataProvider());

            IElement element = asyncCollection[0];
            element = asyncCollection[0];
            element = asyncCollection[0];
            element = asyncCollection[0];

            Assert.AreNotEqual(null, asyncCollection.LoadingTask);
        }


        /// <summary>
        /// Tests if consecutive calls to different indexes loads all data
        /// </summary>
        [TestMethod]
        public void LoadMultipleTest() {
            AsyncCollection asyncCollection = new AsyncCollection(new SlowDataProvider(2000), 1);
            Assert.AreNotEqual(null, asyncCollection[0] as LoadingElement);
            Assert.AreNotEqual(null, asyncCollection[2] as LoadingElement);

            asyncCollection.LoadingTask.Wait();

            Assert.AreEqual(null, asyncCollection[0] as LoadingElement);
            Assert.AreEqual(null, asyncCollection[2] as LoadingElement);
        }

        /// <summary>
        /// Tests if consecutive calls to the same index loads data
        /// </summary>
        [TestMethod]
        [Timeout(10000)]
        public void LoadMultipleSameIndexTest() {
            AsyncCollection asyncCollection = new AsyncCollection(new SlowDataProvider(2000), 1);
            Assert.AreNotEqual(null, asyncCollection[0] as LoadingElement);
            Assert.AreNotEqual(null, asyncCollection[0] as LoadingElement);

            asyncCollection.LoadingTask.Wait();

            Assert.AreEqual(null, asyncCollection[0] as LoadingElement);
        }
    }
}