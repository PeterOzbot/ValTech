using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PeakDetection.Library;

namespace PeakDetectionSample.Controllers {
    public class SamplePoint : IPoint {
        #region IPoint Members

        public DateTime X { get; private set; }
        public double Y { get; private set; }
        public bool IsPeak { get; set; }

        #endregion

        public SamplePoint(DateTime x, double y) {
            X = x;
            Y = y;
        }

        public override string ToString() {
            return String.Format("X = {0}, Y = {1}, IsPeak = {2}", X, Y, IsPeak);
        }
    }


    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetData(string WindowSize, string Stringency, string PeakDensityPercentage) {
            DetectionSettings detectionSettings = new DetectionSettings();
            try {
                int windowSizeValue = Convert.ToInt32(WindowSize);
                double stringency = Convert.ToDouble(Stringency);
                double peakDensityPercentage = Convert.ToDouble(PeakDensityPercentage);

                detectionSettings = new DetectionSettings(windowSizeValue, stringency, peakDensityPercentage);
            }
            catch { }



            List<IPoint> points = GeneratePoints();

            PeakDetector peakDetector = new PeakDetector(detectionSettings);

            peakDetector.Detect(points);



            return Json(points, JsonRequestBehavior.AllowGet);
        }



        private List<IPoint> GeneratePoints() {
            List<IPoint> points = new List<IPoint>();

            Random rand = new Random();

            for (double x = -50 ; x < 50 ; x += 0.5) {


                //double value = Math.Abs(Math.Sin(x));
                double value = rand.NextDouble();

                points.Add(new SamplePoint(DateTime.Now.AddMinutes(x), 1 - value));
            }

            return points;
        }

        #region Custom Serialization
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            return new JsonNetResult {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        public class JsonNetResult : JsonResult {
            public JsonNetResult() {
                Settings = new JsonSerializerSettings {
                    ReferenceLoopHandling = ReferenceLoopHandling.Error
                };
            }

            public JsonSerializerSettings Settings { get; private set; }

            public override void ExecuteResult(ControllerContext context) {
                if (context == null)
                    throw new ArgumentNullException("context");
                if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("JSON GET is not allowed");

                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

                if (this.ContentEncoding != null)
                    response.ContentEncoding = this.ContentEncoding;
                if (this.Data == null)
                    return;

                var scriptSerializer = JsonSerializer.Create(this.Settings);

                using (var sw = new StringWriter()) {
                    scriptSerializer.Serialize(sw, this.Data);
                    response.Write(sw.ToString());
                }
            }
        }
        #endregion
    }
}