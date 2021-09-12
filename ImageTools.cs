using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace OCR
{
    static class ImageTools
    {
       static  public String OCRFromImage(Image image)
       {
            ImageReader imageReader = new ImageReader(image, AppContext.BaseDirectory, "ces");
            return imageReader.GetText();
       }
        static public Image CaptureFullScreee()
        {
            Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(0, 0, Screen.AllScreens[0].Bounds.X, Screen.AllScreens[0].Bounds.Y, captureRectangle.Size);
            Image image1 = captureBitmap;
            return image1;
        }
        static public Image LoadImageFromFileDialog()
        {
            string filePath;
            Image image = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe,*.png) | *.jpg; *.jpeg; *.jpe;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {             
                filePath = openFileDialog.FileName;
                image = Image.FromFile(filePath);
            }
            return image;
        }
    }
}
