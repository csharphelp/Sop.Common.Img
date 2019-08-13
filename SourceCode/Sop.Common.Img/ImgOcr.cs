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
            var textValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    using (var engine = CreateEngine())
                    {
                        using (var img = Pix.LoadFromFile(imagePath))
                        {
                            var fileName = Path.GetFileName(imagePath).ToLowerInvariant();
                            using (var page = engine.Process(img, fileName, new Rect(0, 0, img.Width, img.Height), PageSegMode.RawLine))
                            {

                                textValue = page.GetText();
                                var s = String.Format("{0:P}", page.GetMeanConfidence());
                                var d = page.GetIterator();
                                var f = page.GetThresholdedImage();
                                var aSymbol = page.GetSegmentedRegions(PageIteratorLevel.Symbol);
                                var aBlock = page.GetSegmentedRegions(PageIteratorLevel.Block);
                                var aPara = page.GetSegmentedRegions(PageIteratorLevel.Para);
                                var aTextLine = page.GetSegmentedRegions(PageIteratorLevel.TextLine);
                                var aWord = page.GetSegmentedRegions(PageIteratorLevel.Word);

                                List<Rectangle> boxes = page.GetSegmentedRegions(PageIteratorLevel.TextLine);

                                for (int i = 0; i < boxes.Count; i++)
                                {
                                    Rectangle box = boxes[i];
                                    string asdasda = String.Format("Box[{0}]: x={1}, y={2}, w={3}, h={4}", i, box.X, box.Y, box.Width, box.Height);
                                }

                              

                                string varult = "";
                                using (var iter = page.GetIterator())
                                {
                                    iter.Begin();

                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                do
                                                {
                                                    if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                    {
                                                        varult = varult + "<BLOCK>";
                                                    }
                                                    varult = iter.GetText(PageIteratorLevel.Word);
                                                    varult = varult + " ";

                                                    if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                    {
                                                        varult = varult + " |";
                                                    }
                                                } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                                if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                                {
                                                    varult = varult + " |";
                                                }
                                            } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                        } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                    } while (iter.Next(PageIteratorLevel.Block));

                                }


                                string asdasd = varult;
                            }
                        }

                        using (var pix = Pix.LoadFromFile(imagePath))
                        {
                            using (var page = engine.Process(pix, PageSegMode.SingleWord))
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

