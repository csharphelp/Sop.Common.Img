using System.DrawingCore;

namespace Sop.Common.Img
{
    /// <summary>
    /// 图片平均色调
    /// </summary>
    public interface IImgAve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        byte[] GetByteRgb(string sourceFile);
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        Color GetColorRgb(string sourceFile);
    }
}