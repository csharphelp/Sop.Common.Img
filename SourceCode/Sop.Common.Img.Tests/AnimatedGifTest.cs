using NUnit.Framework;
using Sop.Common.Img.Gif;
using System;
using System.IO;

namespace Sop.Common.Img.Tests
{
    public class AnimatedGifTest
    {
        private string[] imageFilePaths; 
        private string imageGifPath;
        private string filePath;

        private string outputFilePath;
        private string outputImagePath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
           
            filePath = $"{path}Resources\\gif"; 
            imageFilePaths = new String[]
            {
                $"{filePath}\\01.png",
                $"{filePath}\\02.png",
                $"{filePath}\\03.png",
            };
            //注意，这里使用简单的04.gif,这只含有3帧、
            //使用05.gif   含有140帧哦
            imageGifPath = $"{filePath}\\04.gif";


            //输出目录不应该存在，每次都是新的输出目录
            outputFilePath = $"{path}Resources\\gif-output\\";
            if (!Directory.Exists(outputFilePath))
            {
                Directory.CreateDirectory(outputFilePath);
            } 
            var name = Guid.NewGuid().ToString("N");
            outputImagePath = $"{outputFilePath}\\{name}.gif";
             
        }

        /// <summary>
        /// 生成gif图片
        /// </summary>
        [Test]
        public void GenerateAminmate_Test()
        {
            foreach (var imgFilePath in imageFilePaths)
            {
                if (!File.Exists(imgFilePath))
                {
                    Assert.False(false, "文件路径不存在");
                }
            }
            if (File.Exists(outputImagePath))
            {
                File.Delete(outputImagePath);
            }
            var isok = ImgAnimate.Instance().GenerateAminmate(imageFilePaths, outputImagePath);
            Assert.IsTrue(isok, "生成成功");

            var isExists = File.Exists(outputImagePath);
            Assert.IsTrue(isExists, "文件存在");

        }

        /// <summary>
        /// 分解gif图片
        /// </summary>
        [Test]
        public void DecomposeAminmate_Test()
        {
            var isExists = File.Exists(imageGifPath);
            Assert.IsTrue(isExists, "文件存在");

            var list = ImgAnimate.Instance().DecomposeAminmate(imageGifPath, outputFilePath);
            Assert.IsTrue(list.Success, "文件成功");
           
        }

        /// <summary>
        /// 生成gif图片
        /// </summary>
        [Test] 
        public void Create_Png_Img_To_Gif_Test()
        {
            foreach (var imgFilePath in imageFilePaths)
            {
                if (!File.Exists(imgFilePath))
                {
                    Assert.IsFalse(false, "文件路径不存在");
                }
            }
            if (File.Exists(outputImagePath))
            {
                File.Delete(outputImagePath);
            }
            AnimatedGifEncoder e1 = new AnimatedGifEncoder();
            e1.Start(outputImagePath);
            //e1.Delay = 500;    // 延迟间隔
            e1.SetDelay(500);
            e1.SetRepeat(0);  //-1:不循环,0:总是循环 播放  
            e1.SetSize(100, 200);
            foreach (var imgFilePath in imageFilePaths)
            {
                e1.AddFrame(System.DrawingCore.Image.FromFile(imgFilePath));
            }
            e1.Finish();
            var isExists = File.Exists(outputImagePath);
            Assert.IsTrue(isExists, "文件存在，生成成功");

        }

        /// <summary>
        /// 分解gif图片
        /// </summary>
        [Test]
        public void Create_Gif_Img_To_Png_Test()
        {
            var isExists = File.Exists(imageGifPath);
            Assert.IsTrue(isExists, "文件存在");
            AnimatedGifDecoder de = new AnimatedGifDecoder();
            de.Read(imageGifPath);
            for (int i = 0, count = de.GetFrameCount(); i < count; i++)
            {
                System.DrawingCore.Image frame = de.GetFrame(i);

                frame.Save(outputFilePath + Guid.NewGuid().ToString() + ".png");
            }

        }
    }
}