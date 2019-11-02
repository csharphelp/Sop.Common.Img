using System;
using System.Collections.Generic;
using System.Text;

namespace Sop.Common.Img.Exif
{
    /// <summary>
    /// 转换数据结构
    /// </summary>
    public class MetadataDetail
    {
        /// <summary>
        /// 十六进制字符串
        /// </summary>
        public string Hex { get; set; }
        /// <summary>
        /// 原始值串
        /// </summary>
        public string RawValueAsString { get; set; }
        /// <summary>
        /// 显示值串
        /// </summary>
        public string DisplayValue { get; set; }
    }
}
