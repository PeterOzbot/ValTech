using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageImageCapture.Library.Storage {
    /// <summary>
    /// Defines storage manager for saving taken images
    /// </summary>
    public interface IStorage {
        /// <summary>
        /// Save image from path to specific storage location
        /// </summary>
        /// <param name="imagePath">path to the image taken with phantomJS</param>
        void Save(string imagePath);
    }

    /// <summary>
    /// Save images to given location on local disk
    /// </summary>
    public class DefaultStorage : IStorage {
        private string _destinationPath;



        /// <summary>
        /// Initializes default storage with path to where images should be saved.
        /// </summary>
        public DefaultStorage(string destinationPath) {
            _destinationPath = destinationPath;
        }



        /// <summary>
        /// Save image from path to given destination
        /// </summary>
        /// <param name="imagePath">path to the image taken with phantomJS</param>
        public void Save(string imagePath) {
            // To copy a file to another location and  
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(imagePath, _destinationPath, true);

            // delete the original
            if (System.IO.File.Exists(imagePath)) {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
