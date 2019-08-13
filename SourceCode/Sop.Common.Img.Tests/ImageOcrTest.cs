using NUnit.Framework;
using Sop.Common.Img;
using System;
using System.IO;

namespace Sop.Common.Img.Tests
{
    public class ImageOcrTest
    {

        private string imagePath;
        private string filePath; private string datapath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\ocr\\";
            imagePath = Path.Combine(filePath, "seccode.png");
            datapath = Path.Combine(filePath, "tessdata");

        }
        [Test]
        public void GetThumbnails_Tests()
        {
            ImgOcr.DataPath = datapath;
            //var value1 = ImgOcr.GetStringFromImage(Path.Combine(filePath, "PSM_SingleLine.png"));
            var value = ImgOcr.GetStringFromImage(Path.Combine(filePath, "seccode.png"));



            Assert.AreEqual(value.ToLower(), "aar474");

        }
    }
}