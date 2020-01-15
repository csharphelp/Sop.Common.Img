using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Utility;

namespace Sop.Common.Img.Servers
{
    /// <summary>
    /// 图片瘦身服务 
    /// </summary>
    public class ImgLim : IImgLim
    {
        #region Instance

        private static volatile IImgLim _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IImgLim Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgLim();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance


        /// <summary>
        /// 按照图片质量生成图片
        /// </summary>
        /// <param name="sourceFile">原始图片文件</param>
        /// <param name="outputFile">输出文件名</param>
        /// <param name="quality">图片要保存的压缩质量，该参数的值为1至100的整数，数值越大，保存质量越好</param>
        /// <param name="multiple">缩小倍数</param>
        /// <returns></returns>
        public bool GetThumbnails(string sourceFile, string outputFile, long quality = 100, int multiple = 1)
        {
            bool flag = false;
            try
            {
                using (Bitmap sourceImage = new Bitmap(sourceFile))
                {
                    string mimeType = MimeTypeMap.GetMimeType(sourceFile, "image/png");
                    ImageCodecInfo myImageCodecInfo = GetImageCodecInfo(mimeType);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    int xWidth = (int)((float)sourceImage.Width / multiple);
                    int yWidth = (int)((float)sourceImage.Height / multiple);
                    using (Bitmap newImage = new Bitmap(xWidth, yWidth))
                    {
                        using (Graphics g = Graphics.FromImage(newImage))
                        {
                            g.DrawImage(sourceImage, 0, 0, xWidth, yWidth);
                            sourceImage.Dispose();
                            g.Dispose(); 
                            newImage.Save(outputFile, myImageCodecInfo, myEncoderParameters);
                        }
                    }
                }
                flag = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                GC.Collect();
                GC.WaitForFullGCComplete();
                return false;
            }
            return flag;

        }

        /// <summary>
        /// 按图片尺寸大小压缩图片
        /// </summary>
        /// <param name="sourceFile">原始图片文件</param>
        /// <param name="outputFile">输出文件名</param>
        /// <param name="quality">图片要保存的压缩质量，该参数的值为1至100的整数，数值越大，保存质量越好</param>
        /// <param name="xWidth">x长度</param>
        /// <param name="yWidth">y长度</param>
        /// <returns></returns>
        public bool GetThumbnails(string sourceFile, string outputFile, int xWidth, int yWidth, long quality = 100)
        {
            try
            {
                using (Bitmap sourceImage = new Bitmap(sourceFile))
                {
                    string mimeType = MimeTypeMap.GetMimeType(sourceFile, "image/png");
                    ImageCodecInfo myImageCodecInfo = GetImageCodecInfo(mimeType);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    using (Bitmap newImage = new Bitmap(xWidth, yWidth))
                    {
                        using (Graphics g = Graphics.FromImage(newImage))
                        {
                            g.DrawImage(sourceImage, 0, 0, xWidth, yWidth);
                            sourceImage.Dispose();
                            g.Dispose();
                            newImage.Save(outputFile, myImageCodecInfo, myEncoderParameters);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                GC.Collect();
                GC.WaitForFullGCComplete();
                return false;
            }
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public byte[] CompressionImage(Stream fileStream, long quality)
        {
            using (Image img = Image.FromStream(fileStream))
            {
                using (Bitmap bitmap = new Bitmap(img))
                {
                    string mimeType = MimeTypeMap.GetMimeType(img.RawFormat.ToString(), "image/png");
                    ImageCodecInfo CodecInfo = GetImageCodecInfo(mimeType);


                    var myEncoder = System.DrawingCore.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    using (MemoryStream ms = new MemoryStream())
                    {

                        bitmap.Save(ms, CodecInfo, myEncoderParameters);
                        myEncoderParameters.Dispose();
                        myEncoderParameter.Dispose();
                        return ms.ToArray();
                    }
                }
            }
        }
        /// <summary>
        /// 获取图片特征
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {

                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }

  
}