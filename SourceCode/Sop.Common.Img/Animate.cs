using Sop.Common.Img.Gif;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sop.Common.Img
{
    /// <summary>
    /// 
    /// </summary>
    public class Animate : IAnimate
    {
        #region Instance

        private static volatile IAnimate _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IAnimate Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Animate();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance

        /// <summary>
        /// 生产GIF
        /// </summary>
        /// <param name="imageFilePaths">输入文件路径</param>
        /// <param name="outputFilePath">输出文件路径</param>
        /// <param name="delay">延时毫秒数，大于0</param>
        /// <param name="loop">是否循环</param>
        /// <param name="w">默认0，保持原来宽度</param>
        /// <param name="h">默认0，保持原来高度</param>
        /// <returns></returns>
        public bool GenerateAminmate(string[] imageFilePaths, string outputFilePath, int delay = 500, bool loop = true, int w = 0, int h = 0)
        {
            try
            {
                foreach (var imgFilePath in imageFilePaths)
                {
                    if (!File.Exists(imgFilePath))
                    {
                        //throw new Exception("imageFilePaths have not Exist file");
                        return false;
                    }
                }
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                AnimatedGifEncoder animatedGifEncoder = new AnimatedGifEncoder();
                animatedGifEncoder.Start(outputFilePath);
                animatedGifEncoder.SetDelay(delay);    // 延迟间隔
                animatedGifEncoder.SetRepeat(loop ? 0 : -1);  //-1:不循环,0:总是循环 播放  
                if (w != 0 || h != 0)
                {
                    animatedGifEncoder.SetSize(w, h);
                }

                foreach (var imgFilePath in imageFilePaths)
                {
                    animatedGifEncoder.AddFrame(System.DrawingCore.Image.FromFile(imgFilePath));
                }
                animatedGifEncoder.Finish();
                return File.Exists(outputFilePath);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageGifPath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public ReturnValues DecomposeAminmate(string imageGifPath, string outputFilePath, string fileNamePrefix = null, string FileNameSuffix = ".png")
        {

            var isExists = File.Exists(imageGifPath);
            if (!isExists)
            {
                return new ReturnValues()
                {
                    Success = false
                };
            }
            AnimatedGifDecoder de = new AnimatedGifDecoder();
            de.Read(imageGifPath);

            List<string> list = new List<string>();
            for (int i = 0, count = de.GetFrameCount(); i < count; i++)
            {

                if (string.IsNullOrWhiteSpace(fileNamePrefix))
                {
                    fileNamePrefix = Guid.NewGuid().ToString();
                }
                outputFilePath = outputFilePath + fileNamePrefix + FileNameSuffix;
                try
                {
                    System.DrawingCore.Image frame = de.GetFrame(i);
                    frame.Save(outputFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("捕获异常，不处理" + ex.Message);
                }
                list.Add(outputFilePath);

            }
            return new ReturnValues()
            {
                Success = list.Count == de.GetFrameCount() ? true : false,
                FrameCount = de.GetFrameCount(),
                Delay = de.GetDelay(0),
                FrameSize = de.GetFrameSize(),
                OutputFilePaths = list,
            };
        }


    }


}
