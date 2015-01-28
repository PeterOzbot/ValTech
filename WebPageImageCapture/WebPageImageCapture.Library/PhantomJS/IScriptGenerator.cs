using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPageImageCapture.Library.Image;

namespace WebPageImageCapture.Library.PhantomJS {
    /// <summary>
    /// Defines script generator
    /// </summary>
    public interface IScriptGenerator {
        /// <summary>
        /// Creates script and returns ts path.
        /// </summary>
        /// <returns></returns>
        string GenerateScript(ImageInfo imageInfo);
        /// <summary>
        /// Deletes the script generated
        /// </summary>
        void Clean();
    }

    /// <summary>
    /// Default implementation of script generator
    /// </summary>
    public class DefaultScriptGenerator : IScriptGenerator {
        private ILocationProvider _locationProvider;
        private string _scriptPath = null;



        /// <summary>
        /// Creates script generator with location provider
        /// </summary>
        public DefaultScriptGenerator(ILocationProvider locationProvider) {
            _locationProvider = locationProvider;
        }




        /// <summary>
        /// Creates script and returns its path.
        /// </summary>
        /// <returns></returns>
        public string GenerateScript(ImageInfo imageInfo) {

            // create script path
            _scriptPath = Path.Combine(_locationProvider.GetWorkingDirectory(), String.Format("{0}.js", Guid.NewGuid().ToString()));

            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.Append("var page = require('webpage').create();");
            scriptBuilder.Append(String.Format(@"page.open('{0}', function () {{
                                                  
                                                       page.render('{1}');
                                                       phantom.exit();

                                                 }});", imageInfo.SourceURL, imageInfo.Name));


            // save to file
            System.IO.File.WriteAllText(_scriptPath, scriptBuilder.ToString());


            // return path
            return _scriptPath;
        }

        /// <summary>
        /// Deletes the script generated
        /// </summary>
        public void Clean() {
            if (_scriptPath != null) {
                if (File.Exists(_scriptPath)) {
                    File.Delete(_scriptPath);
                }
            }
        }
    }
}
