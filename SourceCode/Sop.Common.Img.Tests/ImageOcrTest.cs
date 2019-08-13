using NUnit.Framework;
using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Sop.Common.Img.Tests
{
    public class ImageOcrTest
    {

        private string imagePath;
        private string filePath; private string datapath;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\ocr\\";
            imagePath = Path.Combine(filePath, "seccode.png");
            datapath = Path.Combine(filePath, "tessdata");

        }

        [Test]
        public void GetThumbnails_Tests1()
        {

            Image image = Image.FromFile(Path.Combine(filePath, "seccode.jpg"));
            Bitmap bitmap = new Bitmap(image);
            Bitmap bitmask = new Bitmap(bitmap.Width, bitmap.Height);

            

            bitmap = KiContrast(bitmap, 100);
            bitmap.Save(Path.Combine(filePath, "seccode_" + Guid.NewGuid().ToString("N") + ".jpg"));


            //var newBitmap = MakeTransparentKeepColour(bitmap, Color.White);
            ////   //rgb(57, 41, 41)
            //newBitmap = MakeTransparentKeepColour(newBitmap, Color.FromArgb(57, 41, 41));

            //Graphics graphic = Graphics.FromImage(bitmask);
            //graphic.ResetTransform();
            //graphic.Clear(Color.Red);
            var bit = DoApplyMask(bitmap, bitmask);

            Assert.IsEmpty("");


        }
        /// <summary>
        /// Clears the alpha value of all pixels matching the given colour.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="clearColour"></param>
        /// <returns></returns>
        public static Bitmap MakeTransparentKeepColour(Bitmap image, Color clearColour)
        {
            Int32 width = image.Width;
            Int32 height = image.Height;


            // Paint on 32bppargb, so we're sure of the byte data format
            Bitmap bm32 = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(bm32))
                gr.DrawImage(image, new Rectangle(0, 0, width, height));

            BitmapData sourceData = bm32.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bm32.PixelFormat);
            Int32 stride = sourceData.Stride;
            // Copy the image data into a local array so we can use managed functions to manipulate it.
            Byte[] data = new Byte[stride * height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            Byte colR = clearColour.R;
            Byte colG = clearColour.G;
            Byte colB = clearColour.B;
            for (Int32 y = 0; y < height; y++)
            {
                Int32 inputOffs = y * stride;
                for (Int32 x = 0; x < width; x++)
                {
                    if (data[inputOffs + 2] == colR && data[inputOffs + 1] == colG && data[inputOffs] == colB)
                        data[inputOffs + 3] = 0;
                    inputOffs += 4;
                }
            }
            // Copy the edited image data back.
            Marshal.Copy(data, 0, sourceData.Scan0, data.Length);
            bm32.UnlockBits(sourceData);
            return bm32;
        }
        public Bitmap MakeCompositeBitmap(params string[] names)
        {
            Bitmap output = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
            output.MakeTransparent();

            foreach (string name in names)
            {
                Bitmap layer = new Bitmap(Image.FromFile(name));

                for (int x = 0; x < output.Width; x++)
                {
                    for (int y = 0; y < output.Height; y++)
                    {
                        Color pixel = layer.GetPixel(x, y);
                        if (pixel.A > 0) // 0 means transparent, > 0 means opaque
                            output.SetPixel(x, y, Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B));
                    }
                }
            }

            return output;
        }
        private static Bitmap DoApplyMask(Bitmap input, Bitmap mask)
        {
            Bitmap output = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);
            output.MakeTransparent();
            var rect = new Rectangle(0, 0, input.Width, input.Height);

            var bitsMask = mask.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsInput = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsOutput = output.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (int y = 0; y < input.Height; y++)
                {
                    byte* ptrMask = (byte*)bitsMask.Scan0 + y * bitsMask.Stride;
                    byte* ptrInput = (byte*)bitsInput.Scan0 + y * bitsInput.Stride;
                    byte* ptrOutput = (byte*)bitsOutput.Scan0 + y * bitsOutput.Stride;
                    for (int x = 0; x < input.Width; x++)
                    {
                        //I think this is right - if the blue channel is 0 than all of them are (monochrome mask) which makes the mask black
                        if (ptrMask[4 * x] == 0)
                        {
                            ptrOutput[4 * x] = ptrInput[4 * x]; // blue
                            ptrOutput[4 * x + 1] = ptrInput[4 * x + 1]; // green
                            ptrOutput[4 * x + 2] = ptrInput[4 * x + 2]; // red

                            //Ensure opaque
                            ptrOutput[4 * x + 3] = 255;
                        }
                        else
                        {
                            ptrOutput[4 * x] = 0; // blue
                            ptrOutput[4 * x + 1] = 0; // green
                            ptrOutput[4 * x + 2] = 0; // red

                            //Ensure Transparent
                            ptrOutput[4 * x + 3] = 0; // alpha
                        }
                    }
                }

            }
            mask.UnlockBits(bitsMask);
            input.UnlockBits(bitsInput);
            output.UnlockBits(bitsOutput);

            return output;
        }
     


        [Test]
        public void GetThumbnails_Tests()
        {


            ImgOcr.DataPath = datapath;
            //var value1 = ImgOcr.GetStringFromImage(Path.Combine(filePath, "PSM_SingleLine.png"));
            var value = ImgOcr.GetStringFromImage(Path.Combine(filePath, "seccode.png"));



            Assert.AreEqual(value.ToLower(), "aar474");

        }
    }
}