using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;
using Tesseract.Interop;

namespace Sop.Common.Img
{
    /// <summary>
    /// 3.05.02
    /// </summary>
    public class ImgOcr
    {
        /*
         
             https://github.com/tesseract-ocr/tesseract/wiki/Data-Files
        */

        /// <summary>
        /// 
        /// </summary>
        public static string DataPath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static TesseractEngine CreateEngine(string lang = "eng", EngineMode mode = EngineMode.Default)
        {
            string datapath = DataPath;
            return new TesseractEngine(datapath, lang, mode);
        }
        /// <summary>
        /// Gets all the text as string from image.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns></returns>
        public static string GetStringFromImage(string imagePath, string lang = "eng", EngineMode mode = EngineMode.Default)
        {
            if (!Directory.Exists(DataPath))
            {
                throw new Exception(" Directory NOT exist DataPath");
            }
            var textValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    using (var engine = CreateEngine())
                    {
                        using (var img = Pix.LoadFromFile(imagePath))
                        {
                            //FBDH67 \FﬂbHaT

                            var fileName = Path.GetFileName(imagePath).ToLowerInvariant();
                            //using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.CircleWord))
                            //{

                            //    textValue = page.GetText();
                            //    var s = String.Format("{0:P}", page.GetMeanConfidence());
                            //    var d = page.GetIterator();
                            //    var f = page.GetThresholdedImage();
                            //    var aSymbol = page.GetSegmentedRegions(PageIteratorLevel.Symbol);
                            //    var aBlock = page.GetSegmentedRegions(PageIteratorLevel.Block);
                            //    var aPara = page.GetSegmentedRegions(PageIteratorLevel.Para);
                            //    var aTextLine = page.GetSegmentedRegions(PageIteratorLevel.TextLine);
                            //    var aWord = page.GetSegmentedRegions(PageIteratorLevel.Word);

                            //}
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.CircleWord))
                            { 
                                textValue = page.GetText(); 
                            }
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.SparseTextOsd))
                            {
                                textValue = page.GetText();
                            }
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.Count))
                            {
                                textValue = page.GetText();
                            }
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.SingleColumn))
                            {
                                textValue = page.GetText();
                            }
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.SparseText))
                            {
                                textValue = page.GetText();
                            }



                        }

                      

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                }
            }
            return textValue;
        }


 
    }
}

