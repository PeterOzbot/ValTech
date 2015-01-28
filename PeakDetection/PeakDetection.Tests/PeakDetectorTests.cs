using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeakDetection.Library;

namespace PeakDetection.Tests {
    [TestClass]
    public class PeakDetectorTests {
        #region HelperMethods
        /// <summary>
        /// Generates list of points. All points with value greater than 1 are peaks.
        /// </summary>
        /// <returns></returns>
        private List<TestPoint> GeneratePoints() {
            List<TestPoint> points = new List<TestPoint>();
            points.Add(new TestPoint(DateTime.Now.AddMinutes(0), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(1), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(2), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(3), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(4), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(5), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(6), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(7), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(8), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(9), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(10), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(11), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(12), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(13), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(14), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(15), 10));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(16), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(17), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(18), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(19), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(20), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(21), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(22), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(23), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(24), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(25), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(26), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(27), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(28), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(29), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(30), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(31), 10));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(32), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(33), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(34), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(35), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(36), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(37), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(38), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(39), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(40), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(41), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(42), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(43), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(44), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(45), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(46), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(47), 10));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(48), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(49), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(50), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(51), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(52), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(53), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(54), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(55), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(56), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(57), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(58), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(59), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(60), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(61), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(62), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(63), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(64), 10));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(65), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(66), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(67), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(68), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(69), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(70), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(71), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(72), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(73), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(74), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(75), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(76), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(77), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(78), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(79), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(80), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(81), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(82), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(83), 10));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(84), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(85), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(86), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(87), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(88), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(89), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(90), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(91), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(92), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(93), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(94), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(95), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(96), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(97), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(98), 1));
            points.Add(new TestPoint(DateTime.Now.AddMinutes(99), 1));

            return points;
        }
        #endregion

        /// <summary>
        /// Tests scenario with no points
        /// </summary>
        [TestMethod]
        public void EmptyListTest() {
            List<TestPoint> points = new List<TestPoint>();

            PeakDetector peakDetector = new PeakDetector();

            List<IPoint> result = peakDetector.Detect(points);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Tests scenario with single point
        /// </summary>
        [TestMethod]
        public void SinglePointTest() {
            List<TestPoint> points = new List<TestPoint>();
            points.Add(new TestPoint(DateTime.Now, 1));

            PeakDetector peakDetector = new PeakDetector();

            List<IPoint> result = peakDetector.Detect(points);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Tests scenario with few points with obvious peak
        /// </summary>
        [TestMethod]
        public void SimpleDetectionTest() {
            List<TestPoint> points = new List<TestPoint>();
            points.Add(new TestPoint(DateTime.Now, 1));
            points.Add(new TestPoint(DateTime.Now.AddDays(1), 1));
            points.Add(new TestPoint(DateTime.Now.AddDays(2), 5));
            points.Add(new TestPoint(DateTime.Now.AddDays(3), 1));
            points.Add(new TestPoint(DateTime.Now.AddDays(4), 1));

            PeakDetector peakDetector = new PeakDetector(new DetectionSettings(1));

            List<IPoint> result = peakDetector.Detect(points);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(5, result[0].Y);
        }


        /// <summary>
        /// Tests scenario with many points
        /// </summary>
        [TestMethod]
        public void AdvancedDetectionTest() {
            List<TestPoint> points = GeneratePoints();
            PeakDetector peakDetector = new PeakDetector();
            List<IPoint> result = peakDetector.Detect(points);

            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count);

            foreach (IPoint point in points) {
                if (point.IsPeak) {
                    Assert.IsTrue(point.Y > 1, point.Y.ToString());
                }
                else {
                    Assert.IsTrue(point.Y == 1, point.Y.ToString());
                }
            }
        }

        /// <summary>
        /// Tests if original collection has objects changed.
        /// </summary>
        [TestMethod]
        public void AdvancedDetection_ResultCollectionTest() {
            List<TestPoint> points = GeneratePoints();
            PeakDetector peakDetector = new PeakDetector();
            List<IPoint> result = peakDetector.Detect(points);

            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count);

            foreach (IPoint point in result) {
                Assert.IsTrue(point.IsPeak);
                Assert.IsTrue(point.Y > 1);
            }
        }
    }
}
