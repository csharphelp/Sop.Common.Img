using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using Sop.Common.Img.Utility;
namespace Sop.Common.Img
{
    /// <summary>
    /// ImgWaterMark 
    /// </summary>
    public class ImgWaterMark
    {
        #region Instance

        private static volatile ImgWaterMark _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgWaterMark Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgWaterMark();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance

 
    }

   
}