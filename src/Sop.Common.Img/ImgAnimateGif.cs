using Sop.Common.Img.Gif;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;

namespace Sop.Common.Img
{
    /// <summary>
    /// 
    /// </summary>
    public class ImgAnimateGif
    {
        #region Instance

        private static volatile ImgAnimateGif _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgAnimateGif Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgAnimateGif();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance
        /// <summary>
        /// 生产动画图片（必须相同格式图片）
        /// </summary>
        /// <param name="imagePaths">输入源图片路径</param>
        /// <param name="outputPath">输出动画路径</param>
        /// <param name="delay">时间间隔（毫秒）</param>
        /// <param name="loop">重复 </param>
        /// <param name="w">宽度默认0 不设置</param>
        /// <param name="h">高度默认0 不设置</param>
        /// <returns>是否成功</returns>
        public bool GetGifImage(string[] imagePaths, string outputPath, int delay = 500, bool loop = true, int w = 0, int h = 0)
        {
            try
            {
                foreach (var imgFilePath in imagePaths)
                {
                    if (!File.Exists(imgFilePath))
                        return false;
                }
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                AnimatedGifEncoder animatedGifEncoder = new AnimatedGifEncoder();
                animatedGifEncoder.Start(outputPath);
                animatedGifEncoder.SetDelay(delay);    // 延迟间隔
                animatedGifEncoder.SetRepeat(loop ? 0 : -1);  //-1:不循环,0:总是循环 播放  
                if (w != 0 || h != 0)
                {
                    animatedGifEncoder.SetSize(w, h);
                }
                foreach (var sImage in imagePaths)
                {
                    animatedGifEncoder.AddFrame(Image.FromFile(sImage));
                }
                animatedGifEncoder.Finish();

                return File.Exists(outputPath);
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// <summary>
        /// 分解GIF
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="outputPath"></param>
        /// <param name="list"></param>
        /// <param name="fileNamePrefix"></param>
        /// <param name="fileNameSuffix"></param>
        /// <returns></returns>
        public bool SetGifImage(string imagePath, string outputPath, out List<string> list, string fileNamePrefix = null, string fileNameSuffix = ".png")
        {
            bool success = false;
            list = new List<string>();
            AnimatedGifDecoder de = new AnimatedGifDecoder();
            de.Read(imagePath);
            for (int i = 0, count = de.GetFrameCount(); i < count; i++)
            {
                Image frame = de.GetFrame(i);
               var saveOutputPath = outputPath + fileNamePrefix + Guid.NewGuid().ToString("N") + fileNameSuffix;

               frame.Save(saveOutputPath);
                list.Add(saveOutputPath);
                success = File.Exists(saveOutputPath) ? true : false;
            }
            return success;

        }

    }


}
