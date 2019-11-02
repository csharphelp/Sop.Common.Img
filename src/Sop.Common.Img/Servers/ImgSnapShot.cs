namespace Sop.Common.Img.Servers
{
    /// <summary>
    /// 图片截图服务
    /// </summary>
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
