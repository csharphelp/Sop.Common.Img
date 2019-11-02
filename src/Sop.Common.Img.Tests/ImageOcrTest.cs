using NUnit.Framework;
using Sop.Common.Img.Servers;
using System;
using System.DrawingCore;
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
            imagePath = Path.Combine(filePath, "seccode.jpg");
            datapath = Path.Combine(filePath, "tessdata");

        }

        [Test]
        public void Get_png_text_Tests()
        {

            Image image = Image.FromFile(Path.Combine(filePath, "seccode.jpg"));
            Bitmap bitmap = new Bitmap(image);
            Bitmap bitmask = new Bitmap(bitmap.Width, bitmap.Height);



            bitmap = ImgManager.SetContrast(bitmap, 100);
            string NewFile = Path.Combine(filePath, "seccode_" + Guid.NewGuid().ToString("N") + ".jpg");
            bitmap.Save(NewFile);

            var nb = new Bitmap(Image.FromFile(NewFile));

            var img = ImgManager.SetReplacesFloodFill(nb, 0, 0, Color.White);
            string imgNewFile = Path.Combine(filePath, "seccode_flood_" + Guid.NewGuid().ToString("N") + ".jpg");
            img.Save(imgNewFile);
            

            ImgOcr.DataPath = datapath;
            var value = ImgOcr.GetStringFromImage(imgNewFile);

            Assert.IsEmpty("");


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