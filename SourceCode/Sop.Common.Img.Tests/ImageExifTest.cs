using NUnit.Framework;
using Sop.Common.Img;
using System;

namespace Sop.Common.Img.Tests
{
    public class ImageExifTest
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

            ///http://odum9helk.qnssl.com/resource/gogopher.jpg?exif
            ///{"ApertureValue":{"val":"5.00 EV (f/5.7)","type":5},"ColorSpace":{"val":"sRGB","type":3},"ComponentsConfiguration":{"val":"- - - -","type":7},"CustomRendered":{"val":"Normal process","type":3},"DateTime":{"val":"2011:11:19 17:09:23","type":2},"DateTimeDigitized":{"val":"2011:11:19 17:09:23","type":2},"DateTimeOriginal":{"val":"2011:11:19 17:09:23","type":2},"ExifVersion":{"val":"Unknown Exif Version","type":7},"ExposureBiasValue":{"val":"0.33 EV","type":10},"ExposureMode":{"val":"Auto exposure","type":3},"ExposureProgram":{"val":"Aperture priority","type":3},"ExposureTime":{"val":"1/50 sec.","type":5},"FNumber":{"val":"f/5.6","type":5},"Flash":{"val":"Flash did not fire, compulsory flash mode","type":3},"FlashPixVersion":{"val":"FlashPix Version 1.0","type":7},"FocalLength":{"val":"45.0 mm","type":5},"FocalPlaneResolutionUnit":{"val":"Inch","type":3},"FocalPlaneXResolution":{"val":"5728.18","type":5},"FocalPlaneYResolution":{"val":"5808.40","type":5},"ISOSpeedRatings":{"val":"3200","type":3},"Make":{"val":"Canon","type":2},"MaxApertureValue":{"val":"5.19 EV (f/6.0)","type":5},"MeteringMode":{"val":"Pattern","type":3},"Model":{"val":"Canon EOS 600D","type":2},"Orientation":{"val":"Top-left","type":3},"PixelXDimension":{"val":"640","type":4},"PixelYDimension":{"val":"427","type":4},"ResolutionUnit":{"val":"Inch","type":3},"SceneCaptureType":{"val":"Standard","type":3},"ShutterSpeedValue":{"val":"5.62 EV (1/49 sec.)","type":10},"SubSecTimeDigitized":{"val":"11","type":2},"SubSecTimeOriginal":{"val":"11","type":2},"SubsecTime":{"val":"11","type":2},"WhiteBalance":{"val":"Auto white balance","type":3},"XResolution":{"val":"72","type":5},"YResolution":{"val":"72","type":5}}
            imagePath = $"{filePath}gogopher.jpg";

            var name = "grape-imageslim-" + Guid.NewGuid().ToString("N");
            outputFilePath = $"{filePath}output\\{name}.jpg";
        }
        [Test]
        public void GetExifDateTime_Tests()
        { 
            var result = ImgExif.GetExifDateTime(imagePath);
            Assert.AreEqual(result, "");
            


        }
        [Test]
        public void GetExifInfo_Tests()
        {
            var result = ImgExif.GetExifInfo(imagePath);

            var re = result;


        }


    }
}