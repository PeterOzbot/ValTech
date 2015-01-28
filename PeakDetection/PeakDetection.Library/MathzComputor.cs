using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Defines class for calculating standard deviance, mean value and for checking for peak condition
    /// </summary>
    public interface IMathzComputor {
        /// <summary>
        /// Calculates standard deviance and mean value and writes to peak detection data
        /// </summary>
        PeakDetectionData Compute(PeakDetectionData peakDetectionData);
        /// <summary>
        /// Checks for peak conditions
        /// </summary>
        bool IsPeak(double peakValue, PeakDetectionData peakDetectionData, DetectionSettings detectionSettings);
    }
    /// <summary>
    /// Default implementation for IMathzComputor
    /// </summary>
    public class DefaultMathzComputor : IMathzComputor {

        /// <summary>
        /// Calculates standard deviance and mean value and writes to peak detection data 
        /// </summary>
        public PeakDetectionData Compute(PeakDetectionData peakDetectionData) {
            // initialize default values
            peakDetectionData.Mean = 0;
            peakDetectionData.StandardDeviance = 0;

            // Compute mean and std of peak function
            int counter = 0;
            double M2 = 0;
            double delta;

            // process data
            foreach (double functionValue in peakDetectionData.PeakFunctionValues) {
                counter++;

                delta = functionValue - peakDetectionData.Mean;
                peakDetectionData.Mean = peakDetectionData.Mean + delta / counter;
                M2 = M2 + delta * (functionValue - peakDetectionData.Mean);
            }

            // calculate deviance
            double variance = M2 / (counter - 1);
            peakDetectionData.StandardDeviance = Math.Sqrt(variance);

            return peakDetectionData;
        }
        /// <summary>
        /// Checks for peak conditions
        /// </summary>
        public bool IsPeak(double peakValue, PeakDetectionData peakDetectionData, DetectionSettings detectionSettings) {
            // if peak function value minus mean is greater than Stringency * StandardDeviance then point is a peak
            return (peakValue > 0 && (peakValue - peakDetectionData.Mean) > detectionSettings.Stringency * peakDetectionData.StandardDeviance);

        }
    }
}
