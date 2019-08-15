using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Gif;
using Sop.Common.Img.Utility;
namespace Sop.Common.Img
{
    /// <summary>
    /// ImgWaterMark
    /// 1、水印支持多中图片，尤其支持gif
    /// 2、图片水印、文字水印，文字平铺水印、混合水印。
    /// </summary>
    public class ImgWaterMark
    {
        //todo
        //文字平铺水印、混合水印、GIF未处理



        #region Instance

        private static volatile ImgWaterMark _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgWaterMark Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgWaterMark();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance

        //https://developer.qiniu.com/dora/manual/1316/image-watermarking-processing-watermark


        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="sourcePath">源图片</param>
        /// <param name="waterPath">水印图片</param>
        /// <param name="dissolve">透明度，取值范围1-100，默认值为100（完全不透明）。</param>
        /// <param name="imagePosition">水印位置</param>
        /// <returns>水印图片</returns>
        public Bitmap SetWaterMark(string sourcePath, string waterPath, float dissolve = 100, ImagePosition imagePosition = ImagePosition.RigthBottom)
        {
            return new Bitmap(ImgWaterMark.Instance().SetWaterMark(Image.FromFile(sourcePath), Image.FromFile(waterPath), dissolve, imagePosition));
        }
        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="sourceImage">源图片</param>
        /// <param name="waterImage">水印图片</param>
        /// <param name="dissolve">透明度，取值范围1-100，默认值为100（完全不透明）。</param>
        /// <param name="imagePosition">水印位置</param>
        /// <param name="distanceX">横轴边距，单位:像素(px)，默认值为10。</param>
        /// <param name="distanceY">纵轴边距，单位:像素(px)，默认值为10。</param>
        /// <param name="watermarkScale">水印图片自适应原图的短边比例，ws的取值范围为0-1。具体是指水印图片保持原比例，并短边缩放到原图短边＊ws。</param>
        /// <param name="watermarkScaleType">水印图片自适应原图的类型，取值0、1、2、3分别表示为自适应原图的短边、长边、宽、高，默认值为0</param>
        /// <returns>水印图片</returns>
        public Image SetWaterMark(Image sourceImage, Image waterImage, float dissolve = 100, ImagePosition imagePosition = ImagePosition.RigthBottom, int distanceX = 10, int distanceY = 10, int watermarkScale = 0, int watermarkScaleType = 0)
        {
            dissolve = dissolve > 100 ? 100 : dissolve;
            dissolve = dissolve < 0 ? 0 : dissolve;


            //水印图片不支持gif
            if (waterImage.RawFormat.Guid == ImageFormat.Gif.Guid)
            {
                throw new Exception("waterImage ImageFormat Gif");
            }
            if (sourceImage.RawFormat.Guid == ImageFormat.Gif.Guid)
            {
                //拆分gif
                //临时
                string tempGif = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString("N") + ".gif");
                sourceImage.Save(tempGif, ImageFormat.Gif);
                List<string> list = new List<string>();
                //历史保存
                AnimatedGifDecoder de = new AnimatedGifDecoder();
                de.Read(tempGif);
                for (int i = 0, count = de.GetFrameCount(); i < count; i++)
                {
                    Image frame = de.GetFrame(i);
                    string tempPng = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString("N") + ".png");
                    frame.Save(tempPng);


                    var img = SetWaterMarkByImg(Image.FromFile(tempPng), waterImage, dissolve, imagePosition, distanceX, distanceY, watermarkScale, watermarkScaleType);

                    string tempWaterPng = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString("N") + "_waterImage.png");
                     
                    img.Save(tempWaterPng);
                    list.Add(tempWaterPng);

                }
                //拼接GIF
                AnimatedGifEncoder e1 = new AnimatedGifEncoder();
                e1.Start(tempGif);
                //e1.Delay = 500;    // 延迟间隔
                e1.SetDelay(500);
                e1.SetRepeat(0);  //-1:不循环,0:总是循环 播放  
                foreach (var imgFilePath in list)
                {
                    e1.AddFrame(Image.FromFile(imgFilePath));
                }
                e1.Finish();


                //清除历史文件
                try
                {
                    //File.Delete(tempGif);
                }
                catch
                {
                    GC.WaitForFullGCComplete();
                }

                return Image.FromFile(tempGif);

            }
            return SetWaterMarkByImg(sourceImage, waterImage, dissolve, imagePosition, distanceX, distanceY, watermarkScale, watermarkScaleType);


        }
        private Image SetWaterMarkByImg(Image sourceImage, Image waterImage, float dissolve = 100, ImagePosition imagePosition = ImagePosition.RigthBottom, int distanceX = 10, int distanceY = 10, int watermarkScale = 0, int watermarkScaleType = 0)
        {
            Image imgPhoto = sourceImage;
            int sWidth = imgPhoto.Width;
            int sHeight = imgPhoto.Height;
            using (Bitmap bmPhoto = new Bitmap(sWidth, sHeight, PixelFormat.Format24bppRgb))
            {
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                Image imgWatermark = new Bitmap(waterImage);
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, sWidth, sHeight), 0, 0, sWidth, sHeight, GraphicsUnit.Pixel);
                Bitmap bmWatermark = new Bitmap(bmPhoto);
                bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grWatermark = Graphics.FromImage(bmWatermark);
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = {
               new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, // red红色
               new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, //green绿色
               new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, //blue蓝色       
               new float[] {0.0f,  0.0f,  0.0f, (float)(dissolve / 100), 0.0f}, //透明度     
               new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
            };
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                 ColorAdjustType.Bitmap);
                int xPosOfWm;
                int yPosOfWm;
                switch (imagePosition)
                {
                    case ImagePosition.BottomMiddle:
                        xPosOfWm = ((sWidth - wmWidth) / 2) - distanceX;
                        yPosOfWm = sHeight - wmHeight - distanceY;
                        break;
                    case ImagePosition.Center:
                        xPosOfWm = (sWidth - wmWidth) / 2;
                        yPosOfWm = (sHeight - wmHeight) / 2;
                        break;
                    case ImagePosition.LeftBottom:
                        xPosOfWm = distanceX;
                        yPosOfWm = sHeight - wmHeight - distanceY;
                        break;
                    case ImagePosition.LeftTop:
                        xPosOfWm = distanceX;
                        yPosOfWm = distanceY;
                        break;
                    case ImagePosition.RightTop:
                        xPosOfWm = sWidth - wmWidth - distanceX;
                        yPosOfWm = distanceY;
                        break;
                    case ImagePosition.RigthBottom:
                        xPosOfWm = sWidth - wmWidth - distanceX;
                        yPosOfWm = sHeight - wmHeight - distanceY;
                        break;
                    case ImagePosition.TopMiddle:
                        xPosOfWm = (sWidth - wmWidth) / 2;
                        yPosOfWm = distanceY;
                        break;
                    default:
                        xPosOfWm = distanceX;
                        yPosOfWm = sHeight - wmHeight - distanceY;
                        break;
                }

                grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);

                imgPhoto = bmWatermark;
                grPhoto.Dispose();
                grWatermark.Dispose();

                return imgPhoto;
            }

        }

        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="sourceImage">源图片</param>
        /// <param name="waterWords">水印文字内容</param>
        /// <param name="dissolve">透明度，取值范围1-100，默认值为100（完全不透明）。</param>
        /// <param name="imagePosition">水印位置</param>
        /// <param name="distanceX">横轴边距，单位:像素(px)，默认值为10。</param>
        /// <param name="distanceY">纵轴边距，单位:像素(px)，默认值为10。</param>
        /// <param name="familyName">水印文字字体，默认为黑体</param>
        /// <param name="fontSize">水印文字字体，默认为黑体</param>
        /// <returns>水印图片</returns>
        public Image SetWaterMark(Image sourceImage, string waterWords, float dissolve = 100, ImagePosition imagePosition = ImagePosition.RigthBottom, int distanceX = 10, int distanceY = 10, string familyName = "宋体", int fontSize = 16)
        {
            #region  判断参数是否有效
            dissolve = dissolve > 100 ? 100 : dissolve;
            dissolve = dissolve < 0 ? 0 : dissolve;
            #endregion

            Image imgPhoto = sourceImage;
            int sWidth = imgPhoto.Width;
            int sHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(sWidth, sHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //将我们要添加水印的图片按照原始大小描绘（复制）到图形中
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, sWidth, sHeight),
             0, 0, sWidth, sHeight, GraphicsUnit.Pixel);

            Font crFont = null;
            SizeF crSize = new SizeF();
            int tempFontSize = fontSize;
            do
            {
                crFont = new Font(familyName, tempFontSize, FontStyle.Bold);
                crSize = grPhoto.MeasureString(waterWords, crFont);
                tempFontSize = sWidth / 2;

            } while ((ushort)crSize.Width < (ushort)sWidth);

            int yPixlesFromBottom = (int)(sHeight * 0.05);
            float wmHeight = crSize.Height;
            float wmWidth = crSize.Width;

            float xPosOfWm;
            float yPosOfWm;

            switch (imagePosition)
            {
                case ImagePosition.BottomMiddle:
                    xPosOfWm = sWidth / 2;
                    yPosOfWm = sHeight - wmHeight - distanceY;
                    break;
                case ImagePosition.Center:
                    xPosOfWm = sWidth / 2;
                    yPosOfWm = sHeight / 2;
                    break;
                case ImagePosition.LeftBottom:
                    xPosOfWm = wmWidth;
                    yPosOfWm = sHeight - wmHeight - distanceY;
                    break;
                case ImagePosition.LeftTop:
                    xPosOfWm = wmWidth / 2;
                    yPosOfWm = wmHeight / 2;
                    break;
                case ImagePosition.RightTop:
                    xPosOfWm = sWidth - wmWidth - distanceX;
                    yPosOfWm = wmHeight;
                    break;
                case ImagePosition.RigthBottom:
                    xPosOfWm = sWidth - wmWidth - distanceX;
                    yPosOfWm = sHeight - wmHeight - distanceY;
                    break;
                case ImagePosition.TopMiddle:
                    xPosOfWm = sWidth / 2;
                    yPosOfWm = wmWidth;
                    break;
                default:
                    xPosOfWm = wmWidth;
                    yPosOfWm = sHeight - wmHeight - distanceY;
                    break;
            }
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            int alpha = Convert.ToInt32(256 * dissolve);
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));

            grPhoto.DrawString(waterWords, crFont, semiTransBrush2, new PointF(xPosOfWm + 1, yPosOfWm + 1), StrFormat);
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            grPhoto.DrawString(waterWords, crFont, semiTransBrush, new PointF(xPosOfWm, yPosOfWm), StrFormat);
            imgPhoto = bmPhoto;
            //释放资源，将定义的Graphics实例grPhoto释放，grPhoto功德圆满
            grPhoto.Dispose();


            return new Bitmap(imgPhoto);
        }






    }


}