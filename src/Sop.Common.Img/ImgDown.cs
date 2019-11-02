using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;

using System.IO;
using System.Net;

namespace Sop.Common.Img
{
    /// <summary>
    /// 图像下载
    /// </summary>
    public class ImgDown
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetRemoteImg(string remotePath, string virtualPath)
        {
            if (string.IsNullOrEmpty(remotePath))
                return null;
            int imgNamele = remotePath.Length - remotePath.LastIndexOf("/", StringComparison.Ordinal);
            string imgName = remotePath.Substring(remotePath.LastIndexOf("/", StringComparison.Ordinal), imgNamele);

            return GetRemoteImg(remotePath, virtualPath, imgName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="filePath"></param>
        /// <param name="imgName"></param>
        /// <param name="imgExt"></param>
        /// <returns></returns>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public string GetRemoteImg(string remotePath, string filePath, string imgName = null, string imgExt = null)
        {
            if (string.IsNullOrEmpty(remotePath))
                return null;
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            if (string.IsNullOrWhiteSpace(imgExt))
            {
                imgExt = remotePath.Substring(remotePath.LastIndexOf(".", StringComparison.Ordinal), remotePath.Length - remotePath.LastIndexOf(".", StringComparison.Ordinal));
            }
            if (string.IsNullOrWhiteSpace(imgName))
            {
                imgName = Guid.NewGuid() + imgExt;
            }
            try
            {
                WebRequest webRequest = WebRequest.Create(remotePath);
                webRequest.Timeout = 10000;
                webRequest.Method = "GET";
                HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                using (var stream = httpWebResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            string imgPathAndName = filePath + imgName;
                            switch (imgExt.ToLower())
                            {
                                case ".gif":
                                    img.Save(imgPathAndName, ImageFormat.Gif);
                                    break;
                                case ".jpg":
                                case ".jpeg":
                                    img.Save(imgPathAndName, ImageFormat.Jpeg);
                                    break;
                                case ".png":
                                    img.Save(imgPathAndName, ImageFormat.Png);
                                    break;
                                case ".icon":
                                    img.Save(imgPathAndName, ImageFormat.Icon);
                                    break;
                                case ".bmp":
                                    img.Save(imgPathAndName, ImageFormat.Bmp);
                                    break;
                                default:
                                    img.Save(imgPathAndName);
                                    break; ;
                            }

                        }
                    }
                }
                return filePath + imgName;
            }
            catch  
            {
                return null;
            }
        }









    }
}
