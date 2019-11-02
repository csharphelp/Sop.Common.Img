using System.DrawingCore.Imaging;
using System.IO;

namespace Sop.Common.Img
{
    /// <summary>
    /// 图片压缩
    /// </summary>
    public interface IImgLim
    {
        /// <summary>
        /// 按照图片质量生成图片
        /// </summary>
        /// <param name="sourceFile">原始图片文件</param>
        /// <param name="outputFile">输出文件名</param>
        /// <param name="quality">图片要保存的压缩质量，该参数的值为1至100的整数，数值越大，保存质量越好</param>
        /// <param name="multiple">缩小倍数</param>
        /// <returns></returns>
        bool GetThumbnails(string sourceFile, string outputFile, long quality = 100, int multiple = 1);

        /// <summary>
        /// 按图片尺寸大小压缩图片
        /// </summary>
        /// <param name="sourceFile">原始图片文件</param>
        /// <param name="outputFile">输出文件名</param>
        /// <param name="quality">图片要保存的压缩质量，该参数的值为1至100的整数，数值越大，保存质量越好</param>
        /// <param name="xWidth">x长度</param>
        /// <param name="yWidth">y长度</param>
        /// <returns></returns>
        bool GetThumbnails(string sourceFile, string outputFile, int xWidth, int yWidth, long quality = 100);

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        byte[] CompressionImage(Stream fileStream, long quality);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        ImageCodecInfo GetImageCodecInfo(string mimeType);
    }
}