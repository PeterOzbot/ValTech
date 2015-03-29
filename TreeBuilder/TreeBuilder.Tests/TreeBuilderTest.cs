using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeBuilder.Library;
using System.Linq;

namespace TreeBuilder.Tests {
    [TestClass]
    public class TreeBuilderTest {

        /// <summary>
        /// Sample class used in testing
        /// </summary>
        internal class TestNode : ISourceNode {

            public int? ParentID { get; set; }
            public int ID { get; set; }

            public string Value { get; set; }

            /// <summary>
            /// Creates the instance with given data.
            /// </summary>
            public TestNode(int? parentID, int id, string value) {
                ID = id;
                ParentID = parentID;
                Value = value;
            }


            public override string ToString() {
                return Value;
            }
        }

        [TestMethod]
        public void TreeCreationTest() {
            List<TestNode> sourceData = new List<TestNode>() {
                new TestNode(10, 13, "Node-3-1-2"),
                new TestNode(10, 12, "Node-3-1-1"),
                new TestNode(null, 1, "Node-1"),        
                new TestNode(null, 3, "Node-3"),
                new TestNode(1, 4, "Node-1-1"),
                new TestNode(1, 5, "Node-1-2"),
                new TestNode(2, 6, "Node-2-1"),
                new TestNode(3, 10, "Node-3-1"),
                new TestNode(3, 11, "Node-3-2"),
                new TestNode(13, 14, "Node-3-1-2-1"),
                new TestNode(2, 7, "Node-2-2"),
                new TestNode(6, 8, "Node-2-1-1"),
                new TestNode(6, 9, "Node-2-1-2"),
                new TestNode(null, 2, "Node-2"),
            };


            TreeBuilder.Library.TreeBuilder<TestNode> treeBuilder = new TreeBuilder<TestNode>();
            List<Node> result = treeBuilder.Build(sourceData).ToList();

            // top parents are 3
            Assert.AreEqual(3, result.Count);

            // try to find correct elements
            Assert.IsNotNull(result.Find(el => el.Value.ID == 1));
            Assert.IsNotNull(result.Find(el => el.Value.ID == 2));
            Assert.IsNotNull(result.Find(el => el.Value.ID == 3));
            // all should have parents null
            Assert.IsNull(result[0].Parent);
            Assert.IsNull(result[1].Parent);
            Assert.IsNull(result[2].Parent);

            // check the first one
            Node currentNode = result.Find(el => el.Value.ID == 1);
            // check children
            Assert.IsNotNull(currentNode.Children);
            Assert.AreEqual(2, currentNode.Children.Count);
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 4));
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 5));
            // both children must not have children
            Assert.AreEqual(0, currentNode.Children[0].Children != null ? currentNode.Children[0].Children.Count : 0);
            Assert.AreEqual(0, currentNode.Children[1].Children != null ? currentNode.Children[1].Children.Count : 0);
            // both children should have parent
            Assert.IsNotNull(currentNode.Children[0].Parent);
            Assert.AreEqual(1, currentNode.Children[0].Parent.Value.ID);
            Assert.IsNotNull(currentNode.Children[1].Parent);
            Assert.AreEqual(1, currentNode.Children[1].Parent.Value.ID);

            // check the second one
            currentNode = result.Find(el => el.Value.ID == 2);
            Assert.IsNotNull(currentNode.Children);
            Assert.AreEqual(2, currentNode.Children.Count);
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 6));
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 7));
            // both children should have parent
            Assert.IsNotNull(currentNode.Children[0].Parent);
            Assert.AreEqual(2, currentNode.Children[0].Parent.Value.ID);
            Assert.IsNotNull(currentNode.Children[1].Parent);
            Assert.AreEqual(2, currentNode.Children[1].Parent.Value.ID);

            // child with ID=6 (2-1) must have 2 children, other child (ID=7 2-2) must have zero children
            Assert.AreEqual(2, currentNode.Children.Find(el => el.Value.ID == 6).Children.Count);
            Assert.AreEqual(0, currentNode.Children.Find(el => el.Value.ID == 7).Children.Count);
            // both children should have parent
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 6).Children[0].Parent);
            Assert.AreEqual(6, currentNode.Children.Find(el => el.Value.ID == 6).Children[0].Parent.Value.ID);
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 6).Children[1].Parent);
            Assert.AreEqual(6, currentNode.Children.Find(el => el.Value.ID == 6).Children[1].Parent.Value.ID);


            //children of child ID=6 must no have children
            Node child1 = currentNode.Children.Find(el => el.Value.ID == 6).Children[0];
            Node child2 = currentNode.Children.Find(el => el.Value.ID == 6).Children[1];
            Assert.AreEqual(0, child1.Children != null ? child1.Children.Count : 0);
            Assert.AreEqual(0, child2.Children != null ? child2.Children.Count : 0);

            // check the third one
            currentNode = result.Find(el => el.Value.ID == 3);
            Assert.IsNotNull(currentNode.Children);
            Assert.AreEqual(2, currentNode.Children.Count);
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 10));
            Assert.IsNotNull(currentNode.Children.Find(el => el.Value.ID == 11));
            // both children should have parent
            Assert.IsNotNull(currentNode.Children[0].Parent);
            Assert.AreEqual(3, currentNode.Children[0].Parent.Value.ID);
            Assert.IsNotNull(currentNode.Children[1].Parent);
            Assert.AreEqual(3, currentNode.Children[1].Parent.Value.ID);

            // child with ID=10 (3-1) must have 2 children, other child (ID=11 3-2) must have zero children
            Assert.AreEqual(2, currentNode.Children.Find(el => el.Value.ID == 10).Children.Count);
            Assert.AreEqual(0, currentNode.Children.Find(el => el.Value.ID == 11).Children.Count);

            // check children of child ID=10
            currentNode = currentNode.Children.Find(el => el.Value.ID == 10);
            // both children should have parent
            Assert.IsNotNull(currentNode.Children[0].Parent);
            Assert.AreEqual(10, currentNode.Children[0].Parent.Value.ID);
            Assert.IsNotNull(currentNode.Children[1].Parent);
            Assert.AreEqual(10, currentNode.Children[1].Parent.Value.ID);

            // check children ID
            child1 = currentNode.Children.Find(el => el.Value.ID == 12);
            child2 = currentNode.Children.Find(el => el.Value.ID == 13);
            Assert.IsNotNull(child1);
            Assert.IsNotNull(child2);

            // first child should not have children, second one must have 1
            Assert.AreEqual(0, child1.Children.Count);
            Assert.AreEqual(1, child2.Children.Count);
            // check the last child
            Assert.AreEqual(14, child2.Children[0].Value.ID);
            //check parent
            Assert.IsNotNull(child2.Children[0].Parent);
            Assert.AreEqual(13, child2.Children[0].Parent.Value.ID);
        }
    }
}
