using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncVirtualization.Library;

namespace Async_Data_and_UI_Virtualization_Collection {
    /// <summary>
    /// View model for the example
    /// </summary>
    public class AsyncExampleViewModel {
        /// <summary>
        /// Collection of data
        /// </summary>
        public AsyncCollection Data { get; private set; }


        /// <summary>
        /// Creates a new instance of the  <see cref="AsyncExampleViewModel"/> class.
        /// </summary>
        public AsyncExampleViewModel() {
            Data = new AsyncCollection(new SampleDataProvider());
        }
    }
}
