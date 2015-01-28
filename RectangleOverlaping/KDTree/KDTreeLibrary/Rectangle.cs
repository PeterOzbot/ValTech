using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTreeLibrary {
    public struct Point {
        public double X, Y;

        public Point(double x, double y) {
            this.X = x;
            this.Y = y;
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
            LeftTop = new Point(x1, y1);
            RightBottom = new Point(x2, y2);
            this.Name = name;
            this.Filled = false;
        }
    }
}
