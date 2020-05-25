using System.Collections.Generic;

namespace Sop.Common.Img
{
    /// <summary>
    /// 动图合成接口用于将数张图片合成 GIF
    /// </summary>
    public interface IAnimate
    {
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
        bool GetGifImage(string[] imagePaths, string outputPath, int delay = 500, bool loop = true, int w = 0,
          int h = 0);
        /// <summary>
        /// 分解GIF
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="outputPath"></param>
        /// <param name="list"></param>
        /// <param name="fileNamePrefix"></param>
        /// <param name="fileNameSuffix"></param>
        /// <returns></returns>
        bool SetGifImage(string imagePath,
            string outputPath,
            out List<string> list,
            string fileNamePrefix = null,
            string fileNameSuffix = ".png");
    }
}