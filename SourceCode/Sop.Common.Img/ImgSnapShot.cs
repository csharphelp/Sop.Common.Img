using System;
using System.Collections.Generic;
using System.Text;

namespace Sop.Common.Img
{
    public class ImgSnapShot
    {
        #region Instance

        private static volatile ImgSnapShot _instance = null;
        private static readonly object Lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ImgSnapShot Instance()
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ImgSnapShot();
                    }
                }
            }
            return _instance;
        }

        #endregion Instance



    }
}
