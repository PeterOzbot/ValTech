using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeBuilder.Library {
    /// <summary>
    /// Builds tree structure from flat list of nodes
    /// </summary>
    public class TreeBuilder<T> where T : ISourceNode {
        private Dictionary<int, Node> _parents = null;
        private Dictionary<int, List<Node>> _children = null;

        /// <summary>
        /// Builds the tree
        /// </summary>
        public IEnumerable<Node> Build(IEnumerable<ISourceNode> sourceData) {

            List<Node> treeList = new List<Node>();

            _children = new Dictionary<int, List<Node>>();
            _parents = new Dictionary<int, Node>();

            foreach (ISourceNode sourceNode in sourceData) {

                Node createdNode = null;

                // if it has no parent that means its top most node
                if (!sourceNode.ParentID.HasValue) {
                    createdNode = CreateNode(sourceNode);
                    treeList.Add(createdNode);
                }
                else {

                    // if it has ParentID then it means that its a child
                    // initialize a list if it was not yet created
                    if (!_children.ContainsKey(sourceNode.ParentID.Value)) {
                        _children.Add(sourceNode.ParentID.Value, new List<Node>());
                    }

                    // add to list of children
                    createdNode = CreateNode(sourceNode);
                    _children[sourceNode.ParentID.Value].Add(createdNode);
                }

                // synchronize or add as parent
                if (_parents.ContainsKey(createdNode.Value.ID)) {
                    Node existingNode = _parents[createdNode.Value.ID];
                    existingNode.Children = createdNode.Children;
                    existingNode.Parent = createdNode.Parent;
                    existingNode.Value = createdNode.Value;
                }
                else {
                    _parents.Add(createdNode.Value.ID, createdNode);
                }
            }

            return treeList;
        }

        /// <summary>
        /// Creates the Node and if the children are not yet initialize, initializes the list
        /// </summary>
        private Node CreateNode(ISourceNode value) {

            // if children list not initialized do so
            if (!_children.ContainsKey(value.ID)) {
                _children.Add(value.ID, new List<Node>());
            }

            // get parent
            Node parent = null;
            // if it even has parent
            if (value.ParentID.HasValue) {

                // if parent is not initialized yet do so
                if (!_parents.ContainsKey(value.ParentID.Value)) {
                    _parents.Add(value.ParentID.Value, new Node());
                }
                parent = _parents[value.ParentID.Value];
            }

            // return constructed node
            return new Node() {
                Parent = parent,
                Value = value,
                Children = _children[value.ID]
            };
        }
    }
}
