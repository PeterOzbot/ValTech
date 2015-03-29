using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeBuilder.Library {
    /// <summary>
    /// Node used as result. Constructed with parent and children.
    /// Value is source Node
    /// </summary>
    public class Node {
        /// <summary>
        /// Parent Node, null if top most.
        /// </summary>
        public Node Parent { get; set; }
        /// <summary>
        /// Children of a node.
        /// </summary>
        public List<Node> Children { get; set; }
        /// <summary>
        /// Source Node
        /// </summary>
        public ISourceNode Value { get; set; }
    }
}
