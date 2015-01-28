using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageImageCapture.Library.PhantomJS {
    /// <summary>
    /// Defines PhantomJS.exe location provider
    /// </summary>
    public interface ILocationProvider {
        /// <summary>
        /// Returns path to PhantomJS.exe
        /// </summary>
        /// <returns></returns>
        string Get();
        /// <summary>
        /// Returns path to working directory
        /// </summary>
        /// <returns></returns>
        string GetWorkingDirectory();
    }


    /// <summary>
    /// Location provider which returns path to ExecutingAssembly
    /// </summary>
    public class DefaultLocationProvider : ILocationProvider {
        /// <summary>
        /// Returns path to PhantomJS.exe
        /// </summary>
        /// <returns></returns>
        public string Get() {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // get directory path
            return Path.Combine(location, "PhantomJS/phantomjs.exe");
        }

        /// <summary>
        /// Returns path to working directory
        /// </summary>
        public string GetWorkingDirectory() {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // get directory path
            return Path.Combine(location, "PhantomJS");
        }
    }
}
