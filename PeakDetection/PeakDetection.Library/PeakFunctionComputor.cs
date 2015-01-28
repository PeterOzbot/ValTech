using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Defines class used to calculate values for each point with specific peak detection function.
    /// </summary>
    public interface IPeakFunctionComputor {
        /// <summary>
        /// Computes the values and stores to peak data which is returned.
        /// </summary>
        PeakDetectionData Compute(IEnumerable<IPoint> points, DetectionSettings detectionSettings);
    }

    /// <summary>
    /// Default implementation of IPeakFunctionComputor
    /// </summary>
    public class DefaultPeakFunctionComputor : IPeakFunctionComputor {

        /// <summary>
        /// Computes the values checking points in specific window. Gets the min and max value in that window and result is average of that.
        /// </summary>
        public PeakDetectionData Compute(IEnumerable<IPoint> points, DetectionSettings detectionSettings) {
            PeakDetectionData peakDetectionData = new PeakDetectionData();
            // initialize min/max values
            peakDetectionData.MinValue = peakDetectionData.MaxValue = 0;

            // Compute peak function values
            double[] S = new double[points.Count()];
            double maxLeft, maxRight;
            for (int i = detectionSettings.WindowSize ; i < S.Length - detectionSettings.WindowSize ; i++) {

                IPoint currentChartPoint = points.ElementAt(i);
                IPoint leftChartPoint = points.ElementAt(i - 1);
                IPoint rightChartPoint = points.ElementAt(i + 1);

                // used to remember min and max value
                if (currentChartPoint.Y > peakDetectionData.MaxValue) { peakDetectionData.MaxValue = currentChartPoint.Y; }
                if (currentChartPoint.Y < peakDetectionData.MinValue || peakDetectionData.MinValue == 0) { peakDetectionData.MinValue = currentChartPoint.Y; }

                // get difference between point and its left neighbor 
                maxLeft = currentChartPoint.Y - leftChartPoint.Y;
                maxRight = currentChartPoint.Y - rightChartPoint.Y;

                // get the max difference for left and right neighbors
                for (int j = 2 ; j <= detectionSettings.WindowSize ; j++) {
                    leftChartPoint = points.ElementAt(i - j);
                    rightChartPoint = points.ElementAt(i + j);

                    if (currentChartPoint.Y - leftChartPoint.Y > maxLeft) {
                        maxLeft = currentChartPoint.Y - leftChartPoint.Y;
                    }

                    if (currentChartPoint.Y - rightChartPoint.Y > maxRight) {
                        maxRight = currentChartPoint.Y - rightChartPoint.Y;
                    }
                }
                S[i] = 0.5f * (maxRight + maxLeft);
            }

            // set the peak function values and return
            peakDetectionData.PeakFunctionValues = S;
            return peakDetectionData;
        }
    }
}
