using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Utility;
namespace Sop.Common.Img
{
    /// <summary>
    /// ImgView 提供简单快捷的图片格式转换、缩略、剪裁功能 
    /// </summary>
    public class ImgView
    {
        public static int ToShortOrHeight { get; private set; } = 0;
        public static int ToLongOrWidth { get; private set; } = 0;
        public static bool IsCut { get; private set; } = false;

        public static int ToX { get; private set; } = 0;
        public static int ToY { get; private set; } = 0;

        #region Instance

        private static volatile ImgView _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgView Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgView();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance

        //2、4、8、16、32、64、128或256

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="mode"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        /// <param name="interlace"></param>
        /// <param name="quality"></param>
        /// <param name="ignoreError"></param>
        /// <returns></returns>
        public Image GetThumbnailCutImg(Image image, ViewMode mode, int width = 0, int height = 0, ImageFormat format = null, int interlace = 0, int quality = 75, bool ignoreError = true)
        {

            SetWidthHeight(ViewMode.SetLongEdgeAndSetShortEdgeCut, height, width, image.Height, image.Width);
            try
            {
                Image bitmap = new Bitmap(ToLongOrWidth, ToShortOrHeight);
                //新建一个画板
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    //在指定的位置上,并按指定大小绘制原图片的指定部分
                    g.DrawImage(image, new Rectangle(0, 0, ToLongOrWidth, ToShortOrHeight), new Rectangle(ToX, ToY, image.Width, image.Height), GraphicsUnit.Pixel);
                }

                return bitmap;
            }
            catch
            {
                 return image;
            }


            //Image oldImage = image;
            //Image thumbnailImage = oldImage.GetThumbnailImage(reduceWidth, reduceHeight, new Image.GetThumbnailImageAbort(null), IntPtr.Zero);
            //Bitmap bm = new Bitmap(thumbnailImage);

            //// ＝＝＝处理JPG质量的函数＝＝＝
            //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            //ImageCodecInfo ici = null;
            //foreach (ImageCodecInfo codec in codecs)
            //{
            //    if (codec.MimeType == "image/jpeg")
            //        ici = codec;
            //}
            //EncoderParameters ep = new EncoderParameters();
            //ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);


            //Rectangle cloneRect = new Rectangle(0, 0, cutOutWidth, cutOutHeight);
            //PixelFormat format1 = bm.PixelFormat;
            //Bitmap cloneBitmap = bm.Clone(cloneRect, format1);

            //return cloneBitmap;
            return image;
        }







        public static Image MakeThumbnail(Image originalImage, int width, int height, ThumbnailMode mode)
        {
            int toWidth = width;
            int toHeight = height;

            int x = 0;
            int y = 0;
            int initWidth = originalImage.Width;
            int initHeight = originalImage.Height;

            switch (mode)
            {
                case ThumbnailMode.UsrHeightWidth: //指定高宽缩放（可能变形）
                    break;
                case ThumbnailMode.UsrHeightWidthBound: //指定高宽缩放（可能变形）（过小则不变）
                    if (originalImage.Width <= width && originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    if (originalImage.Width < width)
                    {
                        toWidth = originalImage.Width;
                    }
                    if (originalImage.Height < height)
                    {
                        toHeight = originalImage.Height;
                    }
                    break;
                case ThumbnailMode.UsrWidth: //指定宽，高按比例
                    toHeight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMode.UsrWidthBound: //指定宽（过小则不变），高按比例
                    if (originalImage.Width <= width)
                    {
                        return originalImage;
                    }
                    else
                    {
                        toHeight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case ThumbnailMode.UsrHeight: //指定高，宽按比例
                    toWidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMode.UsrHeightBound: //指定高（过小则不变），宽按比例
                    if (originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    else
                    {
                        toWidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case ThumbnailMode.Cut: //指定高宽裁减（不变形）
                    //计算宽高比
                    double srcScale = (double)originalImage.Width / (double)originalImage.Height;
                    double destScale = (double)toWidth / (double)toHeight;
                    //宽高比相同
                    if (srcScale - destScale >= 0 && srcScale - destScale <= 0.001)
                    {
                        x = 0;
                        y = 0;
                        initWidth = originalImage.Width;
                        initHeight = originalImage.Height;
                    }
                    //源宽高比大于目标宽高比
                    //(源的宽比目标的宽大)
                    else if (srcScale > destScale)
                    {
                        initWidth = originalImage.Height * toWidth / toHeight;
                        initHeight = originalImage.Height;
                        x = (originalImage.Width - initWidth) / 2;
                        y = 0;
                    }
                    //源宽高比小于目标宽高小，源的高度大于目标的高度
                    else
                    {
                        initWidth = originalImage.Width;
                        initHeight = originalImage.Width * height / toWidth;
                        x = 0;
                        y = (originalImage.Height - initHeight) / 2;
                    }
                    break;
                default:
                    break;
            }
            Image bitmap = new Bitmap(toWidth, toHeight);
            //新建一个画板
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //设置高质量插值法
                g.CompositingQuality = CompositingQuality.HighQuality;
                //设置高质量,低速段呈现的平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;

                //在指定的位置上,并按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, toWidth, toHeight), new Rectangle(x, y, initWidth, initHeight), GraphicsUnit.Pixel);
            }
            return bitmap;
        }








        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="oldHeight"></param>
        /// <param name="oldWidth"></param>
        private void SetWidthHeight(ViewMode mode, int height = 0, int width = 0, int oldHeight = 0, int oldWidth = 0)
        {
            if (oldHeight == 0 || oldWidth == 0)
            {
                return;
            }
            //裁剪缩小必须比原图小
            var tempWidthValue = (width >= oldWidth || width <= 0) ? oldWidth : width;
            var tempHeightValue = (height >= oldHeight || width <= 0) ? oldHeight : height;

            var oldLongValue = oldWidth > oldHeight
                ? oldWidth
                : oldHeight;
            var oldShortValue = oldWidth < oldHeight
                ? oldWidth
                : oldHeight;

            var tempLongValue = tempWidthValue > tempHeightValue
                ? tempWidthValue
                : tempHeightValue;
            var tempShortValue = tempWidthValue < tempHeightValue
                ? tempWidthValue
                : tempHeightValue;


            IsCut = false;

            switch (mode)
            {
                case ViewMode.SetLongEdgeAndSetShortEdgeNoCut:
                    {
                        IsCut = false;
                        if (tempShortValue == 0)
                        {

                            //短边自适应
                            tempShortValue = tempLongValue != 0
                                ? oldLongValue / tempLongValue * oldShortValue
                                : tempShortValue;

                        }
                        else
                        {
                            //长边自适应
                            tempLongValue = tempLongValue == 0
                                ? oldShortValue / tempShortValue * oldLongValue
                                : tempLongValue;
                        }

                        ToShortOrHeight = tempShortValue;
                        ToLongOrWidth = tempLongValue;
                    }
                    break;
                case ViewMode.SetHeightAndSetWidthCut:
                    {
                        IsCut = true;
                        if (tempHeightValue == 0)
                        {


                            tempHeightValue = tempWidthValue == 0
                                ? oldHeight
                                : tempWidthValue;
                        }
                        else
                        {
                            tempWidthValue = tempWidthValue == 0
                                ? tempHeightValue
                                : tempWidthValue;
                        }
                    }
                    break;
                case ViewMode.SetHeightAndSetWidthNoCut:
                    {

                        if (tempHeightValue == 0)
                        {
                            tempHeightValue = tempWidthValue != 0
                                ? oldLongValue / tempWidthValue * oldShortValue
                                : tempHeightValue;
                        }
                        else
                        {
                            tempWidthValue = tempWidthValue == 0
                                ? oldShortValue / tempHeightValue * oldLongValue
                                : tempWidthValue;
                        }
                    }
                    break;
                case ViewMode.SetHeightAndLimitWidthNoCut:
                    {

                        if (tempHeightValue == 0)
                        {
                            tempHeightValue = tempWidthValue != 0
                                ? oldLongValue / tempWidthValue * oldShortValue
                                : tempHeightValue;
                        }
                        else
                        {
                            tempWidthValue = tempWidthValue == 0
                                ? oldShortValue / tempHeightValue * oldLongValue
                                : tempWidthValue;
                        }
                    }
                    break;
                case ViewMode.SetLongEdgeAndLimitShortEdgeNoCut:
                    {

                        if (tempHeightValue == 0)
                        {
                            tempHeightValue = tempWidthValue != 0
                                ? oldLongValue / tempWidthValue * oldShortValue
                                : tempHeightValue;
                        }
                        else
                        {
                            tempWidthValue = tempWidthValue == 0
                                ? oldShortValue / tempHeightValue * oldLongValue
                                : tempWidthValue;
                        }
                    }
                    break;
                case ViewMode.SetLongEdgeAndSetShortEdgeCut:

                    ToShortOrHeight = tempShortValue;
                    ToLongOrWidth = tempLongValue;
                    break;
                default:
                    {
                        ToShortOrHeight = tempShortValue;
                        ToLongOrWidth = tempLongValue;
                    }
                    break;
            }





        }



    }


}