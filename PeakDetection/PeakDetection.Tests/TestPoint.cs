using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeakDetection.Library;

namespace PeakDetection.Tests {
    /// <summary>
    /// Point class used for testing
    /// </summary>
    internal class TestPoint : IPoint {
        #region IPoint Members

        public DateTime X { get; private set; }
        public double Y { get; private set; }
        public bool IsPeak { get; set; }

        #endregion


        public TestPoint(DateTime x, double y) {
            X = x;
            Y = y;
        }

        public override string ToString() {
            return String.Format("X = {0}, Y = {1}, IsPeak = {2}", X, Y, IsPeak);
        }
    }
}
