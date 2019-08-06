using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;
using System.DrawingCore;

namespace Sop.Common.Img
{
    /// <summary>
    /// 
    /// </summary>
    public class ImgOcr
    {
        public string GetText(string dataPath, string filename)
        {
            using (var engine = new TesseractEngine(dataPath, "eng", EngineMode.Default))
            {
                // have to load Pix via a bitmap since Pix doesn't support loading a stream.
                using (var image = new Bitmap(filename))
                {
                    //using (var pix = PixConverter.ToPix(image))
                    //{
                    //    using (var page = engine.Process(pix))
                    //    {
                    //        var text = $"{page.GetMeanConfidence():P}";
                    //        return page.GetText();
                    //    }
                    //}
                    return "";
                }

            }

        }
    }
}
