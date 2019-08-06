using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Utility;
namespace Sop.Common.Img
{
    /// <summary>
    /// 图片瘦身服务（imageslim）在尽可能不影响画质的情况下，
    /// 将JPEG、PNG格式的图片实时压缩，大幅缩小文件体积：
    /// 加快客户端图片的加载速度，提升用户体验
    /// 使用须知
    /// 支持 JPEG、PNG 格式。瘦身后画质不变，分辨率不变，格式不变，文件体积大幅缩小。
    /// 对图片大小/分辨率没有限制，处理异常（处理超时、处理后图片大小大于原图、处理出错等）则返回原图。
    /// 原图  http://7xkv1q.com1.z0.glb.clouddn.com/grape.jpg
    /// 瘦身后的图片 http://7xkv1q.com1.z0.glb.clouddn.com/grape.jpg?imageslim
    /// 画质基本不变、格式不变、分辨率不变、图片文件体积大幅减少
    /// </summary>
    public class ImgLim
    {
        #region Instance

        private static volatile ImgLim _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgLim Instance()
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
        public static bool GetThumbnails(string sourceFile, string outputFile, long quality = 100, int multiple = 1)
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
        public static bool GetThumbnails(string sourceFile, string outputFile, int xWidth, int yWidth, long quality = 100)
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
        public static byte[] CompressionImage(Stream fileStream, long quality)
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
        public static ImageCodecInfo GetImageCodecInfo(string mimeType)
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