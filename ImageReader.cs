using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCR
{
    class ImageReader
    {
        Image image;
        string EncoderPath;
        string Language;
        public ImageReader(Image image, string EncoderPath, string Language)
        {
            this.image = image;
            this.EncoderPath = EncoderPath;
            this.Language = Language;
        }
        public string GetText()
        {
            string text = "";
            if (image != null)
            {               
                var engine = new TesseractEngine(EncoderPath, Language, EngineMode.Default);
                Bitmap bitmap = new Bitmap(image);
                var img = PixConverter.ToPix(bitmap);
                var page = engine.Process(img);
                Console.WriteLine("Confidece: " + page.GetMeanConfidence() * 100);
                if (page.GetMeanConfidence() * 100 <= 65)
                {
                    img.Dispose();
                    page.Dispose();
                    bitmap.Dispose();
                    image = image.DrawAsGrayscale().DrawAsNegative();
                    Console.WriteLine("Negative");
                    bitmap = new Bitmap(image);
                    img = PixConverter.ToPix(bitmap);
                    page = engine.Process(img);
                }
                text = page.GetText();               
            }
            return text;
        }
    }
}
