using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Helpers
{
    public class BitmapHelper
    {
        public static Bitmap TrimBitmap(Bitmap source)
        {
            int minX = source.Width, minY = source.Height, maxX = 0, maxY = 0;
            bool hasAlpha = false;

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var c = source.GetPixel(x, y);
                    if (c.A > 0)
                    {
                        hasAlpha = true;
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            if (!hasAlpha)
                return new Bitmap(1, 1);

            int w = maxX - minX + 1;
            int h = maxY - minY + 1;
            var rect = new Rectangle(minX, minY, w, h);
            return source.Clone(rect, PixelFormat.Format32bppArgb);
        }
        public static Bitmap RenderGlyph(FontFamily family, char c, int size)
        {
            string text = c.ToString();
            using var font = new Font(family, size, FontStyle.Regular, GraphicsUnit.Pixel);

            // Measure text
            using var tmp = new Bitmap(1, 1);
            SizeF textSize;
            using (var g = Graphics.FromImage(tmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                textSize = g.MeasureString(text, font, PointF.Empty, StringFormat.GenericTypographic);
            }

            // Render
            var bmp = new Bitmap((int)Math.Ceiling(textSize.Width * 2), (int)Math.Ceiling(textSize.Height * 2), PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.DrawString(text, font, Brushes.White, 0, 0, StringFormat.GenericTypographic);
            }

            return BitmapHelper.TrimBitmap(bmp);
        }
    }
}
