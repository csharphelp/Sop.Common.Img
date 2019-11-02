using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Text;

namespace Sop.Common.Img
{
    public static class Common
    {
        /// <summary> 
        /// 字节流转换成图片 
        /// </summary> 
        /// <param name="bytes">要转换的字节流</param> 
        /// <returns>转换得到的Image对象</returns>
        public static Image ToImage(this byte[] bytes)
        {
            try
            {
                MemoryStream ms = new MemoryStream(bytes);
                Image img = Image.FromStream(ms);
                return img;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static byte[] ToBytes(this Image img)
        { 
            byte[] b = (byte[])new ImageConverter().ConvertTo(img, typeof(byte[]));
            return b;
        }


        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream ToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }



 
 
    }
}
