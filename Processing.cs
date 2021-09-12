using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Tesseract;
using System.Drawing;

namespace OCR
{
    public static class GrayScale
    {
        private static Bitmap GetArgbCopy(Image sourceImage)
        {
            Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }
            return bmpNew;
        }
        private static Bitmap ApplyColorMatrix(Image sourceImage, ColorMatrix colorMatrix)
        {
            Bitmap bmp32BppSource = GetArgbCopy(sourceImage);
            Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
            {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);
            }
            bmp32BppSource.Dispose();
            return bmp32BppDest;
        }

        public static Bitmap DrawAsGrayscale(this Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[]{.3f, .3f, .3f, 0, 0},
                new float[]{.59f, .59f, .59f, 0, 0},
                new float[]{.11f, .11f, .11f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
            });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        public static Bitmap DrawAsNegative(this Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[]{-1, 0, 0, 0, 0},
                new float[]{0, -1, 0, 0, 0},
                new float[]{0, 0, -1, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{1, 1, 1, 1, 1}
            });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }
    }
}
