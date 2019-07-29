namespace Sop.Common.Img
{
    /// <summary>
    /// 动图合成接口（animate）用于将多张图片合成 GIF 动图。
    /// </summary>
    public interface IAnimate
    {
        /// <summary>
        /// 生产动画图片（必须相同格式图片）
        /// </summary>
        /// <param name="imageFilePaths">输入源图片路径</param>
        /// <param name="outputFilePath">输出动画路径</param>
        /// <param name="delay">时间间隔（毫秒）</param>
        /// <param name="loop">重复 </param>
        /// <param name="w">宽度默认0 不设置</param>
        /// <param name="h">高度默认0 不设置</param>
        /// <returns></returns>
        bool GenerateAminmate(string[] imageFilePaths, string outputFilePath, int delay = 500, bool loop = true, int w = 0, int h = 0);

        /// <summary>
        /// 分解动画图片
        /// </summary>
        /// <param name="imageGifPath">输入源图片路径</param>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="fileNamePrefix">文件前缀</param>
        /// <param name="imageFormat">文件后缀名称</param>
        /// <returns></returns>
        ReturnValues DecomposeAminmate(string imageGifPath, string outputFilePath, string fileNamePrefix = null, string FileNameSuffix = ".png");
    }
}
