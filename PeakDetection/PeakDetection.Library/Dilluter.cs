using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Defines the peak dilute logic
    /// </summary>
    public interface IDiluter {
        /// <summary>
        /// Returns new peaks from peaks in all points list.
        /// </summary>
        List<IPoint> Dillute(IEnumerable<IPoint> points, DetectionSettings detectionSettings);
    }

    /// <summary>
    /// default implementation of IDiluter.
    /// </summary>
    public class DefaultDiluter : IDiluter {

        /// <summary>
        /// Calculates range between the first and last date. Then gets the minimal distance between two peaks as percentage of range between min/max dates.
        /// Removes all peaks which are closes as that minimal distance.
        /// </summary>
        public List<IPoint> Dillute(IEnumerable<IPoint> points, DetectionSettings detectionSettings) {
            if (points.Count() == 0) { return new List<IPoint>(); }

            // get peaks ordered by their X value
            List<IPoint> orderedPeaks = new List<IPoint>(points.Where((point) => { return point.IsPeak; }).OrderBy((point) => { return point.X; }));

            int length = orderedPeaks.Count;

            long distanceBetweenPeaks = points.Last().X.Ticks - points.First().X.Ticks;
            distanceBetweenPeaks = Convert.ToInt64(distanceBetweenPeaks * detectionSettings.PeakDensityPercentage);

            for (int index = 0 ; index < length ; index++) {

                if (index + 1 >= length) { continue; }

                if (Math.Abs(orderedPeaks[index + 1].X.Ticks - orderedPeaks[index].X.Ticks) <= distanceBetweenPeaks) {

                    if (orderedPeaks[index + 1].Y > orderedPeaks[index].Y) {
                        orderedPeaks[index].IsPeak = false;
                        orderedPeaks.Remove(orderedPeaks[index]);
                    }
                    else {
                        orderedPeaks[index + 1].IsPeak = false;
                        orderedPeaks.Remove(orderedPeaks[index + 1]);
                    }
                    length--;
                    index--;

                }
            }
            return orderedPeaks;
        }
    }
}
