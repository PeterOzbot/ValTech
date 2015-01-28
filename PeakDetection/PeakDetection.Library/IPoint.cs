using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Defines point
    /// </summary>
    public interface IPoint {
        /// <summary>
        /// X coordinate
        /// </summary>
        DateTime X { get; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        double Y { get; }
        /// <summary>
        /// Indicates if point is peak.
        /// </summary>
        bool IsPeak { get; set; }
    }
}
