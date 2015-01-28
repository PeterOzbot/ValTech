using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncVirtualization.Library {
    /// <summary>
    /// Defines the data provider for the collection.
    /// </summary>
    public interface IDataProvider {
        /// <summary>
        /// Returns the batch of elements from elementIndex
        /// </summary>
        IEnumerable<IElement> Get(int elementIndex, int count = 100);
        /// <summary>
        /// Returns the total count of all elements
        /// </summary>
        int GetCount();
    }
}
