using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTreeLibrary {
    /// <summary>
    /// An interface which enables flexible distance functions.
    /// </summary>
    public interface DistanceFunctions {
        /// <summary>
        /// Compute a distance between two n-dimensional points.
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>The n-dimensional distance.</returns>
        double Distance(double[] p1, double[] p2);

        /// <summary>
        /// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
        /// </summary>
        /// <param name="point">The point of interest.</param>
        /// <param name="min">The minimum coordinate of the rectangle.</param>
        /// <param name="max">The maximum coorindate of the rectangle.</param>
        /// <returns>The shortest n-dimensional distance between the point and rectangle.</returns>
        double DistanceToRectangle(double[] point, double[] min, double[] max);
    }

    /// <summary>
    /// A distance function for our KD-Tree which returns squared euclidean distances.
    /// </summary>
    public class SquareEuclideanDistanceFunction : DistanceFunctions {
        /// <summary>
        /// Find the squared distance between two n-dimensional points.
        /// Rectangle.LeftTop.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.X, Rectangle.RightBottom.Y 
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>The n-dimensional squared distance.</returns>
        public double Distance(double[] p1, double[] p2) {


            //double centerX1 = p1[0] + ((p1[2] - p1[0]) / 2);
            //double centerY1 = p1[1] + ((p1[3] - p1[1]) / 2);

            //double centerX2 = p2[0] + ((p2[2] - p2[0]) / 2);
            //double centerY2 = p2[1] + ((p2[3] - p2[1]) / 2);

            //return Distance2D(new double[] { centerX1, centerY1 }, new double[] { centerX2, centerY2 });






            return CalculateDistance(p1, p2);
        }

        //private double Distance2D(double[] p1, double[] p2) {
        //    double fSum = 0;
        //    for (int i = 0 ; i < p1.Length ; i++) {
        //        double fDifference = (p1[i] - p2[i]);
        //        fSum += fDifference * fDifference;
        //    }
        //    return fSum;
        //}

        ///** * Determines the angle of a straight line drawn between point one and two. The number returned, which is a float in degrees
        // * , tells us how much we have to rotate a horizontal line clockwise for it to match the line between the two points. 
        // * * If you prefer to deal with angles using radians instead of degrees, just change the last line to: "return Math.Atan2(yDiff, xDiff);" */
        //public static double GetAngleOfLineBetweenTwoPoints(double[] p1, double[] p2) {
        //    double xDiff = p2[0] - p1[0];
        //    double yDiff = p2[1] - p1[1];
        //    return Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        //}

        /// <summary>
        /// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
        /// </summary>
        /// <param name="point">The point of interest.</param>
        /// <param name="min">The minimum coordinate of the rectangle.</param>
        /// <param name="max">The maximum coorindate of the rectangle.</param>
        /// <returns>The shortest squared n-dimensional squared distance between the point and rectangle.</returns>
        public double DistanceToRectangle(double[] point, double[] min, double[] max) {
            //double centerXPoint = point[0] + ((point[2] - point[0]) / 2);
            //double centerYPoint = point[1] + ((point[3] - point[1]) / 2);

            //double centerXMin = min[0] + ((min[2] - min[0]) / 2);
            //double centerYMin = min[1] + ((min[3] - min[1]) / 2);

            //double centerXMax = max[0] + ((max[2] - max[0]) / 2);
            //double centerYMax = max[1] + ((max[3] - max[1]) / 2);


            //return DistanceToRectangle2D(new double[] { centerXPoint, centerYPoint }
            //                            , new double[] { centerXMin, centerYMin }
            //                            , new double[] { centerXMax, centerYMax });



            //return Math.Min(CalculateDistance(point, min), CalculateDistance(point, max));




            double[] minMaxRect = new double[4];

            for (int i = 0 ; i < point.Length ; ++i) {


                if (point[i] > max[i])
                    minMaxRect[i] = max[i];

                else if (point[i] < min[i])
                    minMaxRect[i] = min[i];

                else {
                    minMaxRect[i] = point[i];
                }
            }

            // X1 = med min[0] in max[0]
            // Y2 = med min[1] in max[1]
            // X2 = med min[2] in max[2]
            // Y2 = med min[3] in max[3]




            //minMaxRect = new double[] { min[0], min[1], max[2], max[3] };

            return CalculateDistance(point, minMaxRect);

            // return DistanceToRectangle2D(point, min, max);
            //return 0;
        }

        private double CalculateDistance(double[] p1, double[] p2) {
            var dx = Math.Max(p1[0], p2[0]) - Math.Min(p1[2], p2[2]);
            var dy = Math.Max(p1[1], p2[1]) - Math.Min(p1[3], p2[3]);
            if (dx < 0) return Math.Max(dy, 0);
            if (dy < 0) return dx;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private double DistanceToRectangle2D(double[] point, double[] min, double[] max) {

            double fSum = 0;
            double fDifference = 0;


            for (int i = 0 ; i < point.Length ; ++i) {

                fDifference = 0;

                if (point[i] > max[i])
                    fDifference = (point[i] - max[i]);

                else if (point[i] < min[i])
                    fDifference = (point[i] - min[i]);

                fSum += fDifference * fDifference;
            }

            return fSum;
        }
    }
}
