using NUnit.Framework;
using Sop.Common.Img;
using System;

namespace Sop.Common.Img.Tests
{
    public class ImageLimTest
    {
        private string[] imageFilePaths;
        private string outputFilePath;
        private string imagePath;
        private string filePath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\";

            ///http://7xkv1q.com1.z0.glb.clouddn.com/grape.jpg?imageslim
            imagePath = $"{filePath}grape.jpg";

            var name = "grape-imageslim-" + Guid.NewGuid().ToString("N");
            outputFilePath = $"{filePath}output\\{name}.jpg";
        }
        [Test]
        public void GetThumbnails_Tests()
        { 
            var isok = ImgLim.GetThumbnails(imagePath, outputFilePath);
            Assert.IsTrue(isok,"³É¹¦");
        }
    }
}