using NUnit.Framework;
using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Sop.Common.Img.Tests
{
    public class ImgWaterMarkTest
    {

        private string imagePath;
        private string filePath;
        private string datapath;
        private string imageGifPath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\";
            datapath = $"{path}Resources\\WaterTemp";
            imagePath = Path.Combine(filePath, "gogopher.jpg");
            if (!Directory.Exists(datapath))
            {
                Directory.CreateDirectory(datapath);
            }
            //注意，这里使用简单的04.gif,这只含有3帧、
            //使用05.gif   含有140帧哦
            imageGifPath = $"{filePath}\\gif\\04.gif";

        }

        [Test]
        public void Set_Water_Img_Tests()
        {
            string logo = Path.Combine(filePath, "LOGO_32x32.png");

            var bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo));
            bitmap.Save(Path.Combine(datapath, "gogopher_water_" + Guid.NewGuid().ToString("N") + ".jpg"));
           
            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo),100,ImagePosition.TopMiddle);
            bitmap.Save(Path.Combine(datapath, "gogopher_TopMiddle_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 120, ImagePosition.Center);
            bitmap.Save(Path.Combine(datapath, "gogopher_Center_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 80, ImagePosition.BottomMiddle);
            bitmap.Save(Path.Combine(datapath, "gogopher_BottomMiddle_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 60, ImagePosition.RightTop);
            bitmap.Save(Path.Combine(datapath, "gogopher_RightTop_water_" + Guid.NewGuid().ToString("N") + ".jpg"));
            
            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 40, ImagePosition.RigthBottom);
            bitmap.Save(Path.Combine(datapath, "gogopher_RigthBottom_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 100, ImagePosition.LeftBottom);
            bitmap.Save(Path.Combine(datapath, "gogopher_LeftBottom_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

            bitmap = ImgWaterMark.Instance().SetWaterMark(Image.FromFile(imagePath), Image.FromFile(logo), 100, ImagePosition.LeftTop);
            bitmap.Save(Path.Combine(datapath, "gogopher_LeftTop_water_" + Guid.NewGuid().ToString("N") + ".jpg"));

             



            Assert.IsEmpty("");


        }

        [Test]
        public void Set_Water_Img_Gif_Tests()
        {
            string logo = Path.Combine(filePath, "LOGO_32x32.png");
            var sourceImage = Image.FromFile(imageGifPath);

            sourceImage = Image.FromFile($"{filePath}\\gif\\05.gif"); 


            var bitmap = ImgWaterMark.Instance().SetWaterMark(sourceImage, Image.FromFile(logo));
            bitmap.Save(Path.Combine(datapath, "gif_water_" + Guid.NewGuid().ToString("N") + ".gif"));
  
            Assert.IsEmpty("");


        }

    }
}