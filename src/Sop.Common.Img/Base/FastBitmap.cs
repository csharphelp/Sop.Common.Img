using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Runtime.InteropServices;
using Color = System.DrawingCore.Color;
using Point = System.DrawingCore.Point;
using Rectangle = System.DrawingCore.Rectangle;
using RectangleF = System.DrawingCore.RectangleF;

namespace Sop.Common.Img
{
    /// <summary>
    /// Used to efficiently and quickly manipulate bitmaps.
    /// </summary>
    unsafe public class FastBitmap
    {
        #region field
        Rectangle _bounds;
        int _width;
        BitmapData _imageData;

        byte* _pBase;
        PixelData* _pixelData;
        ColorFormat _cFormat = ColorFormat.FormatABGR;


        #endregion

        #region GET SET
        /// <summary>
        /// Gets the width of the wrapped Image.
        /// </summary>
        public int Width => Image.Width;

        /// <summary>
        /// Gets the height of the wrapped Image.
        /// </summary>
        public int Height => Image.Height;

        /// <summary>
        /// 获取或设置此包装的图像。
        /// </summary>
        public Bitmap Image { get; set; }
        #endregion


        /// <summary>
        /// Creates a FastBitmap wrapper for an image object.
        /// </summary>
        /// <param name="img">The image object to wrap.</param>
        public FastBitmap(Bitmap img)
        {
            Image = img;
        }

        /// <summary>
        /// Locks the image to get it ready for fast manipluations.
        /// </summary>
        public void LockImage()
        {
            _bounds = new Rectangle(Point.Empty, Image.Size);
            _width = _bounds.Width * sizeof(PixelData);
            if (_width % 4 != 0) _width = ((_width >> 2) + 1) << 2;

            _imageData = Image.LockBits(_bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _pBase = (byte*)_imageData.Scan0.ToPointer();
        }

        /// <summary>
        /// Gets a pixel at the x/y location.
        /// </summary>
        /// <param name="x">The x pixel location.</param>
        /// <param name="y">The y pixel location.</param>
        /// <returns>The Color at the x/y location.</returns>
        /// <exception cref="Exception">Invalid color format.</exception>
        public Color GetPixel(int x, int y)
        {
            _pixelData = (PixelData*)(_pBase + y * _width + x * sizeof(PixelData));

            switch (_cFormat)
            {
                case ColorFormat.FormatABGR:
                    return Color.FromArgb(_pixelData->a, _pixelData->b, _pixelData->g, _pixelData->r);
                case ColorFormat.FormatARGB:
                    return Color.FromArgb(_pixelData->a, _pixelData->r, _pixelData->g, _pixelData->b);
                case ColorFormat.FormatBGRA:
                    return Color.FromArgb(_pixelData->b, _pixelData->g, _pixelData->r, _pixelData->a);
                case ColorFormat.FormatRGBA:
                    return Color.FromArgb(_pixelData->r, _pixelData->g, _pixelData->b, _pixelData->a);
            }

            throw new Exception("Invalid color format.");
        }

        /// <summary>
        /// Sets a pixel at the x/y location to the new color.
        /// </summary>
        /// <param name="x">The x pixel location.</param>
        /// <param name="y">The y pixel location.</param>
        /// <param name="color">The color to set the pixel to.</param>
        public void SetPixel(int x, int y, Color color)
        {
            PixelData* pixel = (PixelData*)(_pBase + y * _width + x * sizeof(PixelData));
            switch (_cFormat)
            {
                case ColorFormat.FormatABGR:
                    pixel->a = color.A; pixel->r = color.B;
                    pixel->g = color.G; pixel->b = color.R;
                    break;
                case ColorFormat.FormatARGB:
                    pixel->a = color.A; pixel->r = color.R;
                    pixel->g = color.G; pixel->b = color.B;
                    break;
                case ColorFormat.FormatBGRA:
                    pixel->a = color.B; pixel->r = color.G;
                    pixel->g = color.R; pixel->b = color.A;
                    break;
                case ColorFormat.FormatRGBA:
                    pixel->a = color.R; pixel->r = color.G;
                    pixel->g = color.B; pixel->b = color.A;
                    break;
            }
        }

        private void SetAlpha(int x, int y, byte alpha)
        {
            PixelData* pixel = (PixelData*)(_pBase + y * _width + x * sizeof(PixelData));
            pixel->a = alpha;
        }

        private void ToGray(int x, int y)
        {
            PixelData* pixel = (PixelData*)(_pBase + y * _width + x * sizeof(PixelData));
            byte value = (byte)((int)(pixel->r + pixel->g + pixel->b) / 3);
            pixel->r = value;
            pixel->g = value;
            pixel->b = value;
        }

        /// <summary>
        /// Unlocks the image.
        /// </summary>
        public void UnlockImage()
        {
            Image.UnlockBits(_imageData);
        }

        /// <summary>
        /// Creates a clone of the wrapped image object.
        /// </summary>
        /// <returns>A copy of the image object.</returns>
        public Bitmap Clone()
        {
            return Image.Clone(_bounds, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Grabs a sub-section clone of the wrapped image.
        /// </summary>
        /// <param name="rect">The rectangle to cut from.</param>
        /// <param name="format">The PixelFomat to use.</param>
        /// <returns>A sub-bitmap object.</returns>
        public Bitmap Clone(Rectangle rect, PixelFormat format)
        {
            return Image.Clone(rect, format);
        }

        /// <summary>
        /// Grabs a sub-section clone of the wrapped image.
        /// </summary>
        /// <param name="rect">The rectangle to cut from.</param>
        /// <param name="format">The PixelFomat to use.</param>
        /// <returns>A sub-bitmap object.</returns>
        public Bitmap Clone(RectangleF rect, PixelFormat format)
        {
            return Image.Clone(rect, format);
        }



        /// <summary>
        /// Gets or sets the Color format used by this.
        /// </summary>
        public ColorFormat ColorFormat
        {
            get { return _cFormat; }
            set { _cFormat = value; }
        }

        /// <summary>
        /// Replaces an old color with a new color.
        /// </summary>
        /// <param name="oldC">The color in the image to edit.</param>
        /// <param name="newC">The color to replace with.</param>
        public void ReplaceColor(Color oldC, Color newC)
        {
            for (int y = 0; y < Image.Height; ++y)
            {
                for (int x = 0; x < Image.Width; ++x)
                {
                    if (ColorsEqual(GetPixel(x, y), oldC)) SetPixel(x, y, newC);
                }
            }
        }

        /// <summary>
        /// Converts the image to a grayscale representation.
        /// </summary>
        public void Grayscale()
        {
            for (int y = 0; y < Image.Height; ++y)
            {
                for (int x = 0; x < Image.Width; ++x) ToGray(x, y);
            }
        }

        /// <summary>
        /// Sets all transparency to max value.
        /// </summary>
        public void FlattenAlpha()
        {
            for (int y = 0; y < Image.Height; ++y)
            {
                for (int x = 0; x < Image.Width; ++x) SetAlpha(x, y, 255);
            }
        }

        /// <summary>
        /// Replaces the colors of a particular area:
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="newColor"></param>
        /// <returns></returns>
        public Rectangle FloodFill(int x, int y, Color newColor, int width = 0, int height = 0)
        {
            Color oldColor = GetPixel(x, y);
            if (ColorsEqual(oldColor, newColor))
                return Rectangle.Empty;
            Rectangle editRegion = new Rectangle(x, y, width, height);

            Stack<Point> points = new Stack<Point>();
            points.Push(new Point(x, y));
            SetPixel(x, y, newColor);

            while (points.Count > 0)
            {
                Point pt = points.Pop();
                if (pt.X > 0)
                    CheckFloodPoint(points, pt.X - 1, pt.Y, oldColor, newColor);
                if (pt.Y > 0)
                    CheckFloodPoint(points, pt.X, pt.Y - 1, oldColor, newColor);
                if (pt.X < Width - 1)
                    CheckFloodPoint(points, pt.X + 1, pt.Y, oldColor, newColor);
                if (pt.Y < Height - 1)
                    CheckFloodPoint(points, pt.X, pt.Y + 1, oldColor, newColor);

                // grow the edit region:
                if (pt.X < editRegion.X)
                {
                    editRegion.Width += editRegion.X - pt.X;
                    editRegion.X = pt.X;
                }
                else if (pt.X > editRegion.Right)
                {
                    editRegion.Width = pt.X - editRegion.X;
                }

                if (pt.Y < editRegion.Y)
                {
                    editRegion.Height += editRegion.Y - pt.Y;
                    editRegion.Y = pt.Y;
                }
                else if (pt.Y > editRegion.Bottom)
                {
                    editRegion.Height = pt.Y - editRegion.Y;
                }
            }

            return editRegion;
        }

        private void CheckFloodPoint(Stack<Point> points, int x, int y, Color oldC, Color newC)
        {
            if (ColorsEqual(GetPixel(x, y), oldC))
            {
                points.Push(new Point(x, y));
                SetPixel(x, y, newC);
            }
        }

        /// <summary>
        /// 确定两种颜色是否相等。
        /// </summary>
        /// <param name="col1">The color to compare.</param>
        /// <param name="col2">The color to compare against.</param>
        /// <returns>True if they field-wise match.</returns>
        public static bool ColorsEqual(Color col1, Color col2) =>
            (col1.A == col2.A && col1.R == col2.R && col1.G == col2.G && col1.B == col2.B);

        /// <summary>
        /// 使用直接向上的像素数据在此图像中绘制另一个位图。
        /// </summary>
        /// <param name="img">The source image.</param>
        /// <param name="x">x location in pixels.</param>
        /// <param name="y">y location in pixels.</param>
        public void DrawImage(Bitmap img, int x, int y)
        {
            FastBitmap fastSource = new FastBitmap(img);
            fastSource.LockImage();
            for (int y0 = y; y0 < Image.Height; ++y0)
            {
                if (y0 == Height) continue;
                for (int x0 = x; x0 < Image.Width; ++x0)
                {
                    if (x0 == Width) continue;
                    SetPixel(x0, y0, fastSource.GetPixel(x0 - x, y0 - y));
                }
            }
            fastSource.UnlockImage();
        }
    }

    /// <summary>
    /// Pixel data of an image.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelData
    {
        /// <summary>
        /// Red component.
        /// </summary>
        public byte r;

        /// <summary>
        /// Green component.
        /// </summary>
        public byte g;

        /// <summary>
        /// Blue component.
        /// </summary>
        public byte b;

        /// <summary>
        /// Alpha component
        /// </summary>
        public byte a;
    }

    /// <summary>
    /// A color format for loading/saving color data.
    /// </summary>
    public enum ColorFormat
    {
        /// <summary>
        /// ARGB style.
        /// </summary>
        FormatARGB = 0,

        /// <summary>
        /// RGBA style.
        /// </summary>
        FormatRGBA = 1,

        /// <summary>
        /// BGRA style.
        /// </summary>
        FormatBGRA = 2,

        /// <summary>
        /// ABGR style.
        /// </summary>
        FormatABGR = 3,
    }
}
