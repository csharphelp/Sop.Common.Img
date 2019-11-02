using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Runtime.InteropServices;

namespace Sop.Common.Img.Servers
{

    /// <summary>
    /// 图片平均色调 
    /// </summary>
    public class ImgAve : IImgAve
    {

        #region Instance

        private static volatile IImgAve _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IImgAve Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgAve();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance



        /// <summary>
        /// 图片平均色调
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        public byte[] GetByteRgb(string sourceFile)
        {
            using (Bitmap image = new Bitmap(sourceFile))
            {
                // Lock the bitmap's bits.    
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
                // Get the address of the first line.  
                IntPtr ptr = bmpData.Scan0;
                // Declare an array to hold the bytes of the bitmap.  
                int bytes = Math.Abs(bmpData.Stride) * image.Height;
                byte[] rgbValues = new byte[bytes];
                // Copy the RGB values into the array.  
                Marshal.Copy(ptr, rgbValues, 0, bytes);
                // Unlock the bits.  
                image.UnlockBits(bmpData);
                return rgbValues;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        public Color GetColorRgb(string sourceFile)
        {
            using (Bitmap bitmap = new Bitmap(sourceFile))
            { //色调的总和
                var sum_hue = 0d;
                //色差的阈值
                var threshold = 30;
                //计算色调总和
                for (int h = 0; h < bitmap.Height; h++)
                {
                    for (int w = 0; w < bitmap.Width; w++)
                    {
                        var hue = bitmap.GetPixel(w, h).GetHue();
                        sum_hue += hue;
                    }
                }
                var avg_hue = sum_hue / (bitmap.Width * bitmap.Height);

                //色差大于阈值的颜色值
                var rgbs = new List<Color>();
                for (int h = 0; h < bitmap.Height; h++)
                {
                    for (int w = 0; w < bitmap.Width; w++)
                    {
                        var color = bitmap.GetPixel(w, h);
                        var hue = color.GetHue();
                        //如果色差大于阈值，则加入列表
                        if (Math.Abs(hue - avg_hue) > threshold)
                        {
                            rgbs.Add(color);
                        }
                    }
                }
                if (rgbs.Count == 0)
                    return Color.Black;
                //计算列表中的颜色均值，结果即为该图片的主色调
                int sum_r = 0, sum_g = 0, sum_b = 0;
                foreach (var rgb in rgbs)
                {
                    sum_r += rgb.R;
                    sum_g += rgb.G;
                    sum_b += rgb.B;
                }
                return Color.FromArgb(sum_r / rgbs.Count,
                    sum_g / rgbs.Count,
                    sum_b / rgbs.Count);
            }

        }


    }

}
