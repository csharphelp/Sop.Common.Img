using NUnit.Framework;
using Sop.Common.Img.Servers;
using System;

namespace Sop.Common.Img.Tests
{
    /// <summary>
    /// 将JPEG、PNG格式的图片实时压缩而尽可能不影响画质
    /// </summary>
    public class ImgLimTest
    {
        private string[] _imageFilePaths;
        private string _outputFilePath;
        private string _imagePath;
        private string _filePath;
        private IImgLim _imagesLim;

        [SetUp]
        public void Setup()
        {
            _imagesLim = new ImgLim();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = $"{path}Resources\\";

            ///http://7xkv1q.com1.z0.glb.clouddn.com/grape.jpg?imageslim
            _imagePath = $"{_filePath}grape.jpg";

            var name = "grape-imageslim-" + Guid.NewGuid().ToString("N");
            _outputFilePath = $"{_filePath}output\\{name}.jpg";
        }
        [Test]
        public void GetThumbnails_Tests()
        {
            var isok = _imagesLim.GetThumbnails(_imagePath, _outputFilePath);
            Assert.IsTrue(isok, "成功");
        }
    }
}