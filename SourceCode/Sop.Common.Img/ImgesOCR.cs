using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;
using System.DrawingCore;
using Bitmap = System.Drawing.Bitmap;

namespace Sop.Common.Img
{
    /// <summary>
    /// 
    /// </summary>
    public class ImgesOcr
    {
        public string GetText(string dataPath, string filename)
        {
            using (var engine = new TesseractEngine(dataPath, "eng", EngineMode.Default))
            {
                // have to load Pix via a bitmap since Pix doesn't support loading a stream.
                using (var image = new Bitmap(filename))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            var text = String.Format("{0:P}", page.GetMeanConfidence());
                            return page.GetText();
                        }
                    }
                }

            }

        }
    }
