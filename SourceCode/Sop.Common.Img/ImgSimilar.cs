using System;
using System.DrawingCore;
using System.IO;

namespace Sop.Common.Img
{
    /// <summary>
    /// 图片相似计算
    /// TODO:数据测试不全面，目前不能做到相似匹配，具体待优化，目前没有优化方案
    /// </summary>
    public class ImgSimilar
    {
        readonly Image sourceImg;
        public int Width { get; set; } = 8;
        public int Height { get; set; } = 8;

        public ImgSimilar(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("filePath:" + filePath);
            }
            sourceImg = Image.FromFile(filePath);
        }

        public ImgSimilar(Stream stream)
        {
            sourceImg = Image.FromStream(stream);
        }
        /// <summary>
        /// 获取图片的Hash
        /// </summary>
        /// <returns></returns>
        public string GetHash()
        {
            Image image = ReduceSize(Width, Height);
            Byte[] grayValues = ReduceColor(image);
            Byte average = CalcAverage(grayValues);
            string result = ComputeBits(grayValues, average);
            return result;
        }
        /// <summary>
        /// 算哈希值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns> 将上一步的比较结果, 组合在一起, 就构成了一个64位的二进制整数, 这就是这张图片的指纹.</returns>
        public static int CalcSimilarDegree(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException();
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    count++;
            }
            return count;
        }


        #region private

        /// <summary>
        /// 第一步 缩小图片尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Image ReduceSize(int width = 8, int height = 8)
        {
            Image image = sourceImg.GetThumbnailImage(width, height, () =>
            {
                return false;
            }, IntPtr.Zero);
            return image;
        }

        /// <summary>
        /// 转为灰度图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Byte[] ReduceColor(Image image)
        {
            Bitmap bitMap = new Bitmap(image);
            Byte[] grayValues = new Byte[image.Width * image.Height];

            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = bitMap.GetPixel(x, y);
                    byte grayValue = (byte)((color.R * 30 + color.G * 59 + color.B * 11) / 100);
                    grayValues[x * image.Width + y] = grayValue;
                }
            return grayValues;
        }

        /// <summary>
        /// 计算灰度平均值
        /// </summary>
        /// <param name="values"></param>
        /// <returns>计算图片中所有像素的灰度平均值</returns>
        private Byte CalcAverage(byte[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
                sum += (int)values[i];
            return Convert.ToByte(sum / values.Length);
        }

        /// <summary>
        /// 比较像素的灰度
        /// </summary>
        /// <param name="values"></param>
        /// <param name="averageValue"></param>
        /// <returns>如果大于或等于平均值记为1, 小于平均值记为0.</returns>
        private String ComputeBits(byte[] values, byte averageValue)
        {
            char[] result = new char[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < averageValue)
                    result[i] = '0';
                else
                    result[i] = '1';
            }
            return new String(result);
        }


        #endregion
    }
}
