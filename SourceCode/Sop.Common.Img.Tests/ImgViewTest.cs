using NUnit.Framework;
using Sop.Common.Img;
using System;
using System.DrawingCore;
using System.IO;

namespace Sop.Common.Img.Tests
{
    public class ImgViewTest
    {

        private string outputFilePath;
        private string filePath;

        private string imagePath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\";
             
            //2880 x 1800
            imagePath = $"{filePath}grape.jpg";
             
            outputFilePath = Path.Combine($"{filePath}output\\", "ImgView-" + Guid.NewGuid().ToString("N") + ".jpg");
        }
        [Test]
        public void GetCutThumbnailImg_Tests()
        {
            Image image = Image.FromFile(imagePath);
            var result = ImgView.Instance().GetThumbnailCutImg(image, ViewMode.None, 100, 90);

            outputFilePath = Path.Combine($"{filePath}output\\", "ImgView-" + Guid.NewGuid().ToString("N") + ".jpg");
            result.Save(outputFilePath);


            Assert.Equals(result, "ffa9a1a1");
        }
    }
}