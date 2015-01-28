using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageImageCapture.Library.Image {
    /// <summary>
    /// Holds information about the image to be generated.
    /// </summary>
    public class ImageInfo {
        /// <summary>
        /// Name of the image
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// URL from where the image must be taken.
        /// </summary>
        public string SourceURL { get; private set; }



        /// <summary>
        /// Creates a new instance of the  <see cref="BatchInfoImageInfo"/> class.
        /// </summary>
        public ImageInfo(string name, string sourceURL) {
            Name = name;
            SourceURL = sourceURL;
        }
    }
}
