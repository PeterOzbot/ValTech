using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakDetection.Library {
    /// <summary>
    /// Used to detect peaks in a given list of points.
    /// </summary>
    public class PeakDetector {
        private DetectionSettings _detectionSettings;
        private IPeakFunctionComputor _peakFunctionComputor;
        private IMathzComputor _mathzComputor;
        private IDiluter _dilluter;



        /// <summary>
        /// Creates a new instance of the  <see cref="PeakDetector"/> class.
        /// </summary>
        public PeakDetector() {
            _peakFunctionComputor = new DefaultPeakFunctionComputor();
            _mathzComputor = new DefaultMathzComputor();
            _detectionSettings = new DetectionSettings();
            _dilluter = new DefaultDiluter();
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="PeakDetector"/> class.
        /// </summary>
        public PeakDetector(DetectionSettings detectionSettings) {
            _detectionSettings = detectionSettings;
            _peakFunctionComputor = new DefaultPeakFunctionComputor();
            _mathzComputor = new DefaultMathzComputor();
            _dilluter = new DefaultDiluter();
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="PeakDetector"/> class.
        /// </summary>
        public PeakDetector(IPeakFunctionComputor peakFunctionComputor, DetectionSettings detectionSettings) {
            _peakFunctionComputor = peakFunctionComputor;
            _detectionSettings = detectionSettings;
            _mathzComputor = new DefaultMathzComputor();
            _dilluter = new DefaultDiluter();
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="PeakDetector"/> class.
        /// </summary>
        public PeakDetector(IPeakFunctionComputor peakFunctionComputor) {
            _peakFunctionComputor = peakFunctionComputor;
            _detectionSettings = new DetectionSettings();
            _mathzComputor = new DefaultMathzComputor();
            _dilluter = new DefaultDiluter();
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="PeakDetector"/> class.
        /// </summary>
        public PeakDetector(IPeakFunctionComputor peakFunctionComputor, IMathzComputor mathzComputor, IDiluter dilluter, DetectionSettings detectionSettings) {
            _peakFunctionComputor = peakFunctionComputor;
            _detectionSettings = detectionSettings;
            _mathzComputor = mathzComputor;
            _dilluter = dilluter;
        }



        /// <summary>
        /// Sets IsPeak to peaks in points list. Also returns all peaks in a single list.
        /// </summary>
        public List<IPoint> Detect(IEnumerable<IPoint> points) {
            // list of peaks to be returned
            List<IPoint> peaks = new List<IPoint>();


            // initialize detection data
            PeakDetectionData peakDetectionData = new PeakDetectionData();


            // Compute peak function values
            peakDetectionData = _peakFunctionComputor.Compute(points, _detectionSettings);


            // calculate if min and max value are too near each other
            // this means that graph made of points is too flat
            if (peakDetectionData.MaxValue != 0 && peakDetectionData.MinValue != 0) {
                if (((peakDetectionData.MaxValue - peakDetectionData.MinValue) / peakDetectionData.MaxValue) < 0.25) { return new List<IPoint>(); }
            }


            // calculate  mean value and standard deviance
            peakDetectionData = _mathzComputor.Compute(peakDetectionData);


            // Collect only large peaks
            List<int> peakLocations = new List<int>();
            int counter = 0;

            foreach (double peakValue in peakDetectionData.PeakFunctionValues) {
                // get point
                IPoint point = points.ElementAt(counter);

                // check if peak
                if (_mathzComputor.IsPeak(peakValue, peakDetectionData, _detectionSettings)) {
                    point.IsPeak = true;
                    peaks.Add(point);
                }

                counter++;
            }


            // dilute peaks so the ones too near are excluded and return
            return _dilluter.Dillute(points, _detectionSettings);
        }
    }
}
