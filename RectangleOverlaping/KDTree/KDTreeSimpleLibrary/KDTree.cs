using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTreeSimpleLibrary {
    /// <summary>
    /// A KDTree class represents the root of a variable-dimension KD-Tree.
    /// </summary>
    /// <typeparam name="T">The generic data type we want this tree to contain.</typeparam>
    /// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
    public class Tree : Node {
        /// <summary>
        /// Create a new KD-Tree given a number of dimensions.
        /// </summary>
        /// <param name="iDimensions">The number of data sorting dimensions. i.e. 3 for a 3D point.</param>
        public Tree()
            : base() {
        }

        /// <summary>
        /// Get the nearest neighbours to a point in the kd tree using a square euclidean distance function.
        /// </summary>
        /// <param name="tSearchPoint">The point of interest.</param>
        /// <param name="iMaxReturned">The maximum number of points which can be returned by the iterator.</param>
        /// <param name="fDistance">A threshold distance to apply.  Optional.  Negative values mean that it is not applied.</param>
        /// <returns>A new nearest neighbour iterator with the given parameters.</returns>
        public NearestNeighbour NearestNeighbors(double[] tSearchPoint) {
            return new NearestNeighbour(this, tSearchPoint);
        }
    }

    public struct Point {
        public double x, y;

        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
    };

    /// <summarY>
    /// A data item which is stored in each kd node. Rectangle
    /// </summarY>
    public class KDRectangle {
        public bool Filled;
        public Point LeftTop;
        public Point RightBottom;
        public string Name;

        public KDRectangle(double x1, double y1, double x2, double y2, string name) {
            LeftTop = new Point(x1, y2);
            RightBottom = new Point(x2, y1);
            this.Name = name;
            this.Filled = false;
        }
    }
}
