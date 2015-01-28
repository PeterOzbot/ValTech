using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPageImageCapture.Library;
using WebPageImageCapture.Library.Image;
using WebPageImageCapture.Library.PhantomJS;
using WebPageImageCapture.Library.Storage;

namespace WebPageImageCapture.Test {
    [TestClass]
    public class ImageSnapshotMakerTest {

        /// <summary>
        /// Tests if image is created.
        /// </summary>
        [TestMethod]
        public void ImageCreatedTest() {
            ImageInfo imageInfo = new ImageInfo(Guid.NewGuid().ToString() + ".png", "http://phantomjs.org/");

            string resultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            resultPath = Path.Combine(resultPath, imageInfo.Name);

            ILocationProvider locationProvider = new DefaultLocationProvider();

            ImageSnapshotMaker imageSnapshotMaker = new ImageSnapshotMaker(locationProvider, new DefaultStorage(resultPath), new DefaultScriptGenerator(locationProvider));
            imageSnapshotMaker.TakeSnapshot(imageInfo);


            Assert.IsTrue(File.Exists(resultPath));

            File.Delete(resultPath);
        }
    }
}
