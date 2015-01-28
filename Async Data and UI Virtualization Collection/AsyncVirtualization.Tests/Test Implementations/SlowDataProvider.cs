using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncVirtualization.Library;

namespace AsyncVirtualization.Tests {
    /// <summary>
    /// DataProvider implementation used for testing
    /// </summary>
    public class SlowDataProvider : IDataProvider {
        private int _delay;

        /// <summary>
        /// Creates a new instance of the  <see cref="SlowDataProvider"/> class.
        /// </summary>
        public SlowDataProvider(int delay = 1000) {
            _delay = delay;
        }



        #region IDataProvider Members

        /// <summary>
        /// Returns the batch of elements from elementIndex
        /// </summary>
        public IEnumerable<IElement> Get(int lastElementID, int count = 3) {
            Task.Delay(_delay).Wait();
            yield return new TestElement(1);

            if (count < 2) { yield break; }

            Task.Delay(_delay).Wait();
            yield return new TestElement(2);

            if (count < 3) { yield break; }

            Task.Delay(_delay).Wait();
            yield return new TestElement(3);
        }

        /// <summary>
        /// Returns the total count of all elements
        /// </summary>
        public int GetCount() {
            return 3;
        }

        #endregion
    }
}
