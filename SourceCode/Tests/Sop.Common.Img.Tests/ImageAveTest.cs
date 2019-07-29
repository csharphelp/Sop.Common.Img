using NUnit.Framework;
using Sop.Common.Img;
using System;

namespace Sop.Common.Img.Tests
{
    public class ImageAveTest
    {
    
        private string outputFilePath;     
        private string filePath;

        private string imagePath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\";

            //https://o6nalx2hr.qnssl.com/0.jpg?imageAve
            //"RGB": "0x85694d"
            imagePath = $"{filePath}ave.jpg";

            var name = "ave-imagesAve-" + Guid.NewGuid().ToString("N");
            outputFilePath = $"{filePath}output\\{name}.jpg";
        }
        [Test]
        public void GetColorRGB_Tests()
        { 
            var result = ImageAve.GetColorRGB(imagePath);



            Assert.Equals(result, "ffa9a1a1");
        }
    }
}