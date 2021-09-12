using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace OCR
{
    public partial class FormOverlay2_Name : Form
    {
        System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 3);
        Point selPoint;
        static Rectangle mRect;
        System.Media.SoundPlayer player;
        Image image;
        public static Rectangle SelectedRectangle;
        public static bool closed;
        public FormOverlay2_Name(Image image)
        {
            InitializeComponent();
            this.image = image;
        }
        private void FormOverlay2_Name_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = image;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            pictureBox1.Location = new Point(0,0);
            pictureBox1.Size = new Size(this.Size.Width, this.Size.Height);
            this.Location = new Point(0,0);
            this.KeyPreview = true;
            this.KeyUp += FormOverlay2_Name_KeyUp;
            closed = false;
        }

        private void FormOverlay2_Name_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                closed = true;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            selPoint = e.Location;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {              
                pictureBox1.Refresh();
                Point p = e.Location;
                int x = Math.Min(selPoint.X, p.X);
                int y = Math.Min(selPoint.Y, p.Y);
                int w = Math.Abs(p.X - selPoint.X);
                int h = Math.Abs(p.Y - selPoint.Y);
                mRect = new Rectangle(x, y, w, h);
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.DrawRectangle(pen, mRect);
                }
            }
        }
      
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
            try
            {
                player = new System.Media.SoundPlayer(@".\Sound.wav");
                player.Play();
                
                this.Close();
                ReturnPicture();
            }
            catch
            {
            }
           
        }
        public Image ReturnPicture()
        {
            if (mRect.Width != 0 || mRect.Height != 0)
            {
                Console.WriteLine(mRect);
                Bitmap captureBitmap = new Bitmap(mRect.Width, mRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb); //velikost obrazku
                Rectangle captureRectangle = new Rectangle(0, 0, mRect.Width, mRect.Height); //dve posledni je delak aa sirka vyberu
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                Console.WriteLine(captureRectangle.Size);
                captureGraphics.CopyFromScreen(mRect.X, mRect.Y, 0, 0, captureRectangle.Size);// dve prvni je startovni lokace
                Image image1 = captureBitmap;
                return image1;
            }
            else
            {                
                return null;
            }
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            System.Drawing.Pen penBorder = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
            e.Graphics.DrawRectangle(penBorder, 0, 0, pictureBox1.Width, pictureBox1.Height);
        }
    }
}
