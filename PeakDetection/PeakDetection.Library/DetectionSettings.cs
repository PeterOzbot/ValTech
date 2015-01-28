using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Holds settings for detection with which detection can be calibrated.
    /// </summary>
    public class DetectionSettings {
        /// <summary>
        /// The window size to look for peaks. A neighborhood of +/- windowSize will be inspected to search for peaks.
        /// </summary>
        public int WindowSize { get; private set; }
        /// <summary>
        /// Threshold for peak values. Peak with values lower than <code> mean + stringency * std</code> will be rejected.
        /// <code>Mean</code> and <code>std</code>(standard deviation) are calculated on the spikiness function. 
        /// </summary>
        public double Stringency { get; private set; }
        /// <summary>
        /// Percentage is used to calculate how far apart can peaks be.
        /// This is used when diluting peaks percentage is used to calculate how far apart can peaks be,
        /// so this means that for each window(percentage of total time span) there can be only one peak
        /// </summary>
        public double PeakDensityPercentage { get; private set; }




        /// <summary>
        /// Creates a new instance of the  <see cref="DetectionSettings"/> class.
        /// </summary>
        public DetectionSettings(int windowSize, double stringency, double peakDensityPercentage) {
            WindowSize = windowSize;
            Stringency = stringency;
            PeakDensityPercentage = peakDensityPercentage;
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="DetectionSettings"/> class.
        /// </summary>
        public DetectionSettings(int windowSize, double stringency) {
            WindowSize = windowSize;
            Stringency = stringency;
            PeakDensityPercentage = 0.035;
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="DetectionSettings"/> class.
        /// </summary>
        public DetectionSettings(int windowSize) {
            WindowSize = windowSize;
            Stringency = 1;
            PeakDensityPercentage = 0.035;
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="DetectionSettings"/> class.
        /// </summary>
        public DetectionSettings() {
            WindowSize = 5;
            Stringency = 1;
            PeakDensityPercentage = 0.035;
        }
    }
}
