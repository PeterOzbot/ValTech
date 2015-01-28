using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPageImageCapture.Library.Image;
using WebPageImageCapture.Library.PhantomJS;
using WebPageImageCapture.Library.Storage;

namespace WebPageImageCapture.Library {
    /// <summary>
    /// Holds all components together to create images from specific URL.
    /// </summary>
    public class ImageSnapshotMaker {
        private ILocationProvider _locationProvider;
        private IStorage _storage;
        private IScriptGenerator _scriptGenerator;


        /// <summary>
        /// Creates a new instance of the  <see cref="ImageSnapshotMaker"/> class.
        /// </summary>
        public ImageSnapshotMaker(ILocationProvider locationProvider, IStorage storage, IScriptGenerator scriptGenerator) {
            _locationProvider = locationProvider;
            _storage = storage;
            _scriptGenerator = scriptGenerator;
        }


        /// <summary>
        /// Creates image from data in ImageInfo.
        /// </summary>
        public void TakeSnapshot(ImageInfo imageInfo) {

            // get phantom location
            string phatomPath = _locationProvider.Get();

            // string get directory path
            string workingDirectoryPath = _locationProvider.GetWorkingDirectory();

            // get script
            string scriptPath = _scriptGenerator.GenerateScript(imageInfo);

            // execute phantom
            ExecutePhantom(phatomPath, scriptPath, workingDirectoryPath);

            // delete the script
            _scriptGenerator.Clean();

            // save to storage
            _storage.Save(Path.Combine(workingDirectoryPath, imageInfo.Name));
        }



        /// <summary>
        /// Executes PhantomJS.exe with generated script
        /// </summary>
        private void ExecutePhantom(string phatomPath, string scriptPath, string workingDirectoryPath) {
            var startInfo = new ProcessStartInfo {
                FileName = phatomPath,
                Arguments = String.Format("\"{0}\"", scriptPath),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                WorkingDirectory = workingDirectoryPath
            };
            Process proc = Process.Start(startInfo);

            // error gathering
            StringBuilder errorMessage = new StringBuilder();
            StringBuilder outputMessage = new StringBuilder();

            proc.ErrorDataReceived += (sender, errorLine) => { errorMessage.Append(errorLine.Data); };
            proc.OutputDataReceived += (sender, outputLine) => { outputMessage.Append(outputLine.Data); };
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            // if phantom is not done in 3 minute then just abort
            if (!proc.WaitForExit(60000 * 3)) {
                throw new ApplicationException(String.Format("ErrorMessage :{0}  OutputMessage: {1}", errorMessage, outputMessage));
            }
        }
    }
}
