using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
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

        //水印源图片网址（经过URL安全的Base64编码），必须有效且返回一张图片。
        //透明度，取值范围1-100，默认值为100（完全不透明）。
        //水印位置，参考水印锚点参数表，默认值为SouthEast（右下角）。
        //横轴边距，单位:像素(px)，默认值为10。
        //纵轴边距，单位:像素(px)，默认值为10。
        //水印图片自适应原图的短边比例，ws的取值范围为0-1。具体是指水印图片保持原比例，并短边缩放到原图短边＊
        //ws例如：原图大小为250x250，水印图片大小为91x61，如果ws=1，那么最终水印图片的大小为：372x250。


        //水印图片自适应原图的类型，取值0、1、2、3分别表示为自适应原图的短边、长边、宽、高，默认值为0

        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="warerPath"></param>
        /// <returns></returns>
        public Bitmap SetWaterMark(string sourcePath, string warerPath)
        {
            using (Image image = Image.FromFile(sourcePath))
            {
                Bitmap b = new Bitmap(image.Width, image.Height);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.DrawImage(image, 0, 0, image.Width, image.Height);

                Image watermark = new Bitmap(warerPath);

                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                float[][] colorMatrixElements = {
                     new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                     new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                     new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                     new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
                     new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                 };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int xpos = 0;
                int ypos = 0;

                xpos = ((image.Width - watermark.Width) - 10);
                ypos = image.Height - watermark.Height - 10;

                g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

                watermark.Dispose();
                imageAttributes.Dispose();

                return new Bitmap(image);
            }



        }

     

        /// <summary> 
        /// 给图片上水印 
        /// </summary> 
        /// <param name="filePath">原图片地址</param> 
        /// <param name="waterFile">水印图片地址</param> 
        public void MarkWater(string filePath, string waterFile)
        {
            //GIF不水印 
            int i = filePath.LastIndexOf(".");
            string ex = filePath.Substring(i, filePath.Length - i);
            if (string.Compare(ex, ".gif", true) == 0)
            {
                return;
            }

            string ModifyImagePath =  filePath;//修改的图像路径 
            int lucencyPercent = 25;
            Image modifyImage = null;
            Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = Image.FromFile(ModifyImagePath, true);
                drawedImage = Image.FromFile(waterFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标 
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height;
                //设置颜色矩阵 
                float[][] matrixItems ={
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0},
            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像 
                g.DrawImage(drawedImage, new Rectangle(x, y, drawedImage.Width, drawedImage.Height), 10, 10, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(ModifyImagePath);
                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg": imageType = ImageFormat.Jpeg; break;
                    case ".gif": imageType = ImageFormat.Gif; break;
                    case ".png": imageType = ImageFormat.Png; break;
                    case ".bmp": imageType = ImageFormat.Bmp; break;
                    case ".tif": imageType = ImageFormat.Tiff; break;
                    case ".wmf": imageType = ImageFormat.Wmf; break;
                    case ".ico": imageType = ImageFormat.Icon; break;
                    default: break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(ModifyImagePath);
                fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            finally
            {
                try
                {
                    drawedImage.Dispose();
                    modifyImage.Dispose();
                    g.Dispose();
                }
                catch
                {
                }
            }
        }






    }


}