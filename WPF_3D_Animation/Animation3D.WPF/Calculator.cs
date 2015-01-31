using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Animation3D.WPF {
    /// <summary>
    /// Used to perform calculations needed for animations
    /// </summary>
    public static class Calculator {
        public static CalculatorResult Calculate(FrameworkElement element, Point touchPoint) {

            // get center of element
            Point elementCenter = new Point(element.ActualWidth / 2, element.ActualHeight / 2);

            // normalize touch point with center point
            Point normalizedPoint = new Point(
                Math.Min(Math.Max(touchPoint.X / (elementCenter.X * 2), 0), 1),
                Math.Min(Math.Max(touchPoint.Y / (elementCenter.Y * 2), 0), 1));

            // get magnitude and direction
            double xMagnitude = Math.Abs(normalizedPoint.X - 0.5);
            double yMagnitude = Math.Abs(normalizedPoint.Y - 0.5);
            double xDirection = -Math.Sign(normalizedPoint.X - 0.5);
            double yDirection = Math.Sign(normalizedPoint.Y - 0.5);

            // get direction  depending on magnitude
            double direction = Math.Atan2(yMagnitude, xMagnitude) * (180 / Math.PI);

            // calculate radius
            double radius = Math.Sqrt(Math.Pow(xMagnitude, 2) + Math.Pow(yMagnitude, 2));
            // get angle from radius
            double angle = (radius / 0.5) * 10;

            // display calculated values
            Logger.Write(String.Format("xMagnitude = {0} \n yMagnitude = {1}  \n xDirection = {2} \n yDirection = {3} \n normalizedX = {4} \n normalizedY = {5}  \n direction = {6} \n radius = {7}",
                            xMagnitude, yMagnitude, xDirection, yDirection, normalizedPoint.X, normalizedPoint.Y, direction, radius));

            // 45-90
            if (direction > 45) {
                if ((xDirection == 1 && yDirection == -1) || (xDirection == -1 && yDirection == -1)) {
                    // top
                    return new CalculatorResult(new double[] { -1, -1, 0 }, new Vector3D(-1, 0, 0), angle);
                }
                else {
                    //if ((xDirection == -1 && yDirection == 1) || (xDirection == 1 && yDirection == 1)) {
                    // bottom
                    return new CalculatorResult(new double[] { 1, 1, 0 }, new Vector3D(1, 0, 0), angle);
                }
            }
            //0-45
            else {
                if ((xDirection == -1 && yDirection == 1) || (xDirection == -1 && yDirection == -1)) {
                    // right
                    return new CalculatorResult(new double[] { -1, -1, 0 }, new Vector3D(0, 1, 0), angle);
                }
                else {
                    //if ((xDirection == 1 && yDirection == -1) || (xDirection == 1 && yDirection == 1)) {
                    // left
                    return new CalculatorResult(new double[] { 1, 1, 0 }, new Vector3D(0, -1, 0), angle);
                }
            }
        }
    }

    /// <summary>
    /// Holds calculated data
    /// </summary>
    public class CalculatorResult {
        /// <summary>
        /// Position 0 is value for X, 1 is for Y, 2 is for Z
        /// </summary>
        public double[] Center { get; private set; }
        /// <summary>
        /// Vector for axis
        /// </summary>
        public Vector3D Axis { get; private set; }
        /// <summary>
        /// Angle to be animated
        /// </summary>
        public double Angle { get; private set; }



        /// <summary>
        /// Creates a new instance of the  <see cref="CalculatorResult"/> class.
        /// </summary>
        public CalculatorResult(double[] center, Vector3D axis, double angle) {
            Center = center;
            Axis = axis;
            Angle = angle;
        }
    }
}
