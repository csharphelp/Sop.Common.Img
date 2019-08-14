using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Utility;
namespace Sop.Common.Img
{
    /// <summary>
    /// 图片服务 
    /// </summary>
    public class ImgView
    {
        #region Instance

        private static volatile ImgView _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgView Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgView();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance

 

    }

    
}