namespace Sop.Common.Img
{
    public enum ThumbnailMode
    {
        /// <summary>
        /// 宽高缩放模式,可能变形
        /// </summary>
        UsrHeightWidth,
        /// <summary>
        /// 
        /// </summary>
        UsrHeightWidthBound,
        /// <summary>
        /// 指定宽度,高按比例
        /// </summary>
        UsrWidth,
        /// <summary>
        /// 指定宽（过小则不变），高按比例
        /// </summary>
        UsrWidthBound,
        /// <summary>
        /// 自定高度,宽按比例
        /// </summary>
        UsrHeight,
        /// <summary>
        /// 指定高(过小则不变),宽按比例
        /// </summary>
        UsrHeightBound,
        /// <summary>
        /// 剪切
        /// </summary>
        Cut,
        /// <summary>
        /// 
        /// </summary>
        None,
    }


    public enum ViewMode
    { 
        /// <summary>
        ///  限定缩略图的长边最多为LongEdge，短边最多为ShortEdge，进行等比缩放，不裁剪。如果只指定 w 参数则表示限定长边（短边自适应），只指定 h 参数则表示限定短边（长边自适应）模式0适合移动设备上做缩略图。
        /// </summary>
        SetLongEdgeAndSetShortEdgeNoCut,
         
        /// <summary>
        /// 限定缩略图的宽高，进行等比缩放，居中裁剪。转后的缩略图通常恰好是 Width x Height 的大小（有一个边缩放的时候会因为超出矩形框而被裁剪掉多余部分）。如果只指定 w 参数或只指定 h 参数，代表限定为长宽相等的正方图。
        /// </summary>
        SetHeightAndSetWidthCut,

        /// <summary>
        ///  限定缩略图的宽最多为Width，高最多为Height，进行等比缩放，不裁剪。如果只指定 w 参数则表示限定宽（高自适应），只指定 h 参数则表示限定高（宽自适应）。它和模式0类似，区别只是限定宽和高，不是限定长边和短边。从应用场景来说，模式0适合移动设备上做缩略图，模式2适合PC上做缩略图。
        /// </summary>
        SetHeightAndSetWidthNoCut,
         

        /// <summary>
        /// 限定缩略图的宽最少为Width，高最少为Height，进行等比缩放，不裁剪。如果只指定 w 参数或只指定 h 参数，代表长宽限定为同样的值。你可以理解为模式1是模式3的结果再做居中裁剪得到的。
        /// </summary>
        SetHeightAndLimitWidthNoCut,

        /// <summary>
        ///  限定缩略图的长边最少为LongEdge，短边最少为ShortEdge，进行等比缩放，不裁剪。如果只指定 w 参数或只指定 h 参数，表示长边短边限定为同样的值。这个模式很适合在手持设备做图片的全屏查看（把这里的长边短边分别设为手机屏幕的分辨率即可），生成的图片尺寸刚好充满整个屏幕（某一个边可能会超出屏幕）。
        /// </summary>
        SetLongEdgeAndLimitShortEdgeNoCut,

         
        /// <summary>
        ///  限定缩略图的长边最少为LongEdge，短边最少为ShortEdge，进行等比缩放，居中裁剪。如果只指定 w 参数或只指定 h 参数，表示长边短边限定为同样的值。同上模式4，但超出限定的矩形部分会被裁剪
        /// </summary>
        SetLongEdgeAndSetShortEdgeCut,
  
        None,
    }

}