using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy
{
    public static class ImageExtensions
    {
        public static Bitmap ResizeImage(this Image image, double ratio)
        {
            return image.ResizeImage((int)(image.Width * ratio), (int)(image.Height * ratio));
        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap Cut(this Image img, Rectangle rect)
        {
            return img.Cut(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Bitmap Cut(this Image img, int x, int y, int width, int height)
        {
            var newBitmap = new Bitmap(width, height);
            var g = Graphics.FromImage(newBitmap);
            var dest = new RectangleF(0, 0, width, height);
            var src = new RectangleF(x, y, width, height);
            g.DrawImage(img, dest, src, GraphicsUnit.Pixel);
            g.Dispose();
            return newBitmap;
        }
    }
}
