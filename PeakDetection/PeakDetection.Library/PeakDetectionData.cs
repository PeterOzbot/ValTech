using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Holds peak detection data which is passed through different stages
    /// </summary>
    public class PeakDetectionData {
        /// <summary>
        /// Min value through all the points
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// Max value through all the points
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// Standard Deviance through all the points
        /// </summary>
        public double StandardDeviance { get; set; }
        /// <summary>
        /// Mean value through all the points
        /// </summary>
        public double Mean { get; set; }
        /// <summary>
        /// List of values for each point calculated with peak detection function.
        /// </summary>
        public double[] PeakFunctionValues { get; set; }
    }
}
