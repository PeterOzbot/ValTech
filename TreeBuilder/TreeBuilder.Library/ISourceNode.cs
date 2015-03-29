using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeBuilder.Library {
    /// <summary>
    /// Defines the node given to builder as source
    /// </summary>
    public interface ISourceNode {
        /// <summary>
        /// Node parent ID
        /// </summary>
        int? ParentID { get; }
        /// <summary>
        /// Node ID
        /// </summary>
        int ID { get; }
    }
}
