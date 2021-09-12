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

namespace OCR
{
    public partial class Form1 : Form
    {
        readonly KeyboardHook hook = new KeyboardHook();
        bool Opened = false;
        Image image3;
        FormOverlay2_Name formOverlay_Math;
        FormOverlay2_Name formOverlay2;
        private bool allowVisible;
        private bool allowClose;
        readonly Settings1 settings = new Settings1();
        UserControlShowMath showMath;
        RoundedButton RoundedButtonOCR;
        RoundedButton RoundedButtonCopy;

        bool moved = false;
        public Form1() //hlasitost + ikony
        {           
            InitializeComponent();
            SetBalloonTip();
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Size.Width - 5, Screen.PrimaryScreen.Bounds.Height - this.Size.Height - 40);
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Hook_KeyPressed); //udela event handler
            hook.RegisterHotKey(OCR.ModifierKeys.Control | OCR.ModifierKeys.Shift, Keys.X); //co zmacknout

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;

            RoundedButtonOCR = new RoundedButton();
            this.Controls.Add(RoundedButtonOCR);
            RoundedButtonCopy = new RoundedButton();
            this.Controls.Add(RoundedButtonCopy);
            showMath = new UserControlShowMath();
        }
        
        private void SetBalloonTip()
        {
            Container bpcomponents = new Container();
            ContextMenu contextMenu1 = new ContextMenu();

            MenuItem CaptureMenu = new MenuItem();
            CaptureMenu.Index = 1;
            CaptureMenu.Click += CaptureMenu_Click;

            MenuItem OpenMenu = new MenuItem();
            OpenMenu.Index = 2;
            OpenMenu.Click += OpenMenu_Click;

            MenuItem exitMenu = new MenuItem();
            exitMenu.Index = 3; 
            exitMenu.Click += ExitMenu_Click;

            MenuItem MathMenu = new MenuItem();
            MathMenu.Index = 4;  
            MathMenu.Click += MathMenu_Click;

            MenuItem HelpMenu = new MenuItem();
            HelpMenu.Index = 5;  
            HelpMenu.Click += HelpMenu_Click;

            string[] LangCes = new string[] {"Zaznamenat", "Vybrat soubor", "Zavřít", "Matematika (beta)", "Pomoc", "Kopírovat"};
            string[] LangEng = new string[] { "Capture", "Open file", "Exit", "Math (beta)", "Help", "Copy"};

            switch (settings.Language) //0- ces ; 1 - eng
            {
                case 0:
                 CaptureMenu.Text = LangCes[0];
                    OpenMenu.Text = LangCes[1];
                    exitMenu.Text = LangCes[2];
                    MathMenu.Text = LangCes[3];
                    HelpMenu.Text = LangCes[4];
                    break;
                case 1:
                 CaptureMenu.Text = LangEng[0];
                    OpenMenu.Text = LangEng[1];
                    exitMenu.Text = LangEng[2];
                    MathMenu.Text = LangEng[3];
                    HelpMenu.Text = LangEng[4];
                    break;
            }
            contextMenu1.MenuItems.AddRange(new MenuItem[] { CaptureMenu, OpenMenu});
            if (settings.DeveloperOptions)
            {
                contextMenu1.MenuItems.Add(MathMenu);
            }
            contextMenu1.MenuItems.Add("-");
            contextMenu1.MenuItems.Add(HelpMenu);
            contextMenu1.MenuItems.Add("-");//separator
            contextMenu1.MenuItems.Add(exitMenu);
            
            notifyIcon1.ContextMenu = contextMenu1;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = "Image Processer Pro";
            notifyIcon1.BalloonTipText = "Image Processer Pro";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
        }

        bool InfoOpened = false;
        private void HelpMenu_Click(object sender, EventArgs e)
        {
            if (!InfoOpened)
            {
                FormInfo formInfo = new FormInfo();
                formInfo.Visible = true;
                formInfo.FormClosed += FormInfo_FormClosed;
                InfoOpened = true;
            }
        }

        private void FormInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (InfoOpened)
            {
                InfoOpened = false;
            }
        }

        private void MathMenu_Click(object sender, EventArgs e)
        {
            //ClassMath ma = new ClassMath();

            //ma.Matika("-34236+235235-235+22345-5555555-112373097+40=");
            if (!Opened)
            {
                formOverlay_Math = new FormOverlay2_Name(ImageTools.CaptureFullScreee());
                Cursor.Current = Cursors.Cross;
                formOverlay_Math.Show();
                formOverlay_Math.FormClosed += FormOverlay_Math_FormClosed;
                Opened = true;
            }
        }
        private void FormOverlay_Math_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Image image3 = formOverlay_Math.ReturnPicture();
            Console.WriteLine(ImageTools.OCRFromImage(image3).Trim());
            Cursor.Current = Cursors.Default;
            MathFormToFront(ImageTools.OCRFromImage(image3).Trim());
            Opened = false;
        }
        public void MathFormToFront(string priklad)
        {
            allowVisible = true;
            Show();           
            showMath.Show();           
            progressBar1.Visible = false;
            pictureBox1.Visible = false;
            richTextBox1.Visible = false;
            showMath.Priklad = priklad;
            showMath.FillTextBoxes();
            showMath.BringToFront();
            showMath.Location = new Point(0,0);
            RoundedButtonOCR.Visible = false;
            RoundedButtonCopy.Visible = false;
            this.Controls.Add(showMath);
        }
        public void PictureBoxToFront()
        {
            richTextBox1.Visible = true;
            allowVisible = true;
            Show();
            pictureBox1.Visible = true;
            pictureBox1.BringToFront();
            showMath.Hide();
            

         //   progressBar1.Visible = true;
            RoundedButtonOCR.Visible = true;
            RoundedButtonCopy.Visible = false;
            RoundedButtonOCR.Location = new Point((ClientSize.Width /2) - 56, 400);
            RoundedButtonOCR.Size = new Size(130, 40);
            RoundedButtonOCR.Text = "OK";
            RoundedButtonOCR.Font = new Font("Comnic Sans MS", 14, FontStyle.Bold);
            RoundedButtonOCR.ForeColor = Color.Black;
            RoundedButtonOCR.BackColor = Color.FromArgb(120, 220, 120);
            RoundedButtonOCR.BorderColor = Color.FromArgb(120, 220, 120);
            RoundedButtonOCR.Cursor = Cursors.Hand;
            RoundedButtonOCR.MouseMove += RoundedButton_MouseMove;
            RoundedButtonOCR.Click += RoundedButton_Click;
        }

        private void RoundedButton_Click(object sender, EventArgs e) //zmacknuti ok buttonu
        {
            Cursor.Current = Cursors.AppStarting;
            progressBar1.Visible = true;
            progressBar1.Value = 20;
            richTextBox1.BringToFront();
            progressBar1.Value = 40;
            richTextBox1.Text = ImageTools.OCRFromImage(image3).Trim();
            progressBar1.Value = 80;
            RoundedButtonOCR.Visible = false;          
            RoundedButtonCopy.Location = new Point((ClientSize.Width / 2) - 56, 400);
            RoundedButtonCopy.Size = new Size(130, 40);
            RoundedButtonCopy.Visible = true;
            RoundedButtonCopy.Font = new Font("Comnic Sans MS", 14, FontStyle.Bold);
            RoundedButtonCopy.ForeColor = Color.Black;
            RoundedButtonCopy.BackColor = Color.FromArgb(0, 165, 224);
            RoundedButtonCopy.BorderColor = Color.FromArgb(0, 165, 224);
            RoundedButtonCopy.Cursor = Cursors.Hand;
            switch (settings.Language)
            {
                case 0:
                    RoundedButtonCopy.Text = "Kopírovat";
                    break;
                case 1:
                    RoundedButtonCopy.Text = "Copy";
                    break;
            }
            RoundedButtonCopy.MouseMove += RoundedButtonCopy_MouseMove;
            RoundedButtonCopy.Click += RoundedButtonCopy_Click;
            progressBar1.Value = 100;
            Cursor.Current = Cursors.Default;
        }

        private void RoundedButtonCopy_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void RoundedButtonCopy_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moved)
            {
                RoundedButtonCopy.BackColor = Color.FromArgb(0, 145, 204);
                RoundedButtonCopy.BorderColor = Color.FromArgb(0, 145, 204);
                moved = true;
            }
        }

        private void RoundedButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moved)
            {
                RoundedButtonOCR.BackColor = Color.FromArgb(100, 200, 100);
                RoundedButtonOCR.BorderColor = Color.FromArgb(100, 200, 100);
                moved = true;
            }
           
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moved)
            {
                RoundedButtonOCR.BackColor = Color.FromArgb(120, 220, 120);
                RoundedButtonOCR.BorderColor = Color.FromArgb(120, 220, 120);

                RoundedButtonCopy.BackColor = Color.FromArgb(0, 165, 224);
                RoundedButtonCopy.BorderColor = Color.FromArgb(0, 165, 224);
                moved = false;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
        private void ExitMenu_Click(object sender, EventArgs e)
        {
            allowClose = true;
            Application.Exit();
        }
        
        private void OpenMenu_Click(object sender, EventArgs e)//file opener
        {
            image3 = ImageTools.LoadImageFromFileDialog();
            PictureBoxToFront();
            ChangeImageBoySize();
            pictureBox1.Image = image3;
        }
        private void CaptureMenu_Click(object sender, EventArgs e) 
        {
            if (!Opened)
            {
                formOverlay2 = new FormOverlay2_Name(ImageTools.CaptureFullScreee());
                Cursor.Current = Cursors.Cross;
                formOverlay2.Show();
                formOverlay2.FormClosed += FormOverlay2_FormClosed;
                Opened = true;
            }
        }
        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (!Opened)
            {
                formOverlay2 = new FormOverlay2_Name(ImageTools.CaptureFullScreee());
                Cursor.Current = Cursors.Cross;
                formOverlay2.Show();
                formOverlay2.FormClosed += FormOverlay2_FormClosed;
                Opened = true;
            }
        }

        private void FormOverlay2_FormClosed(object sender, FormClosedEventArgs e) //otevre form po zavreni capture
        {
            PictureBoxToFront();
            image3 = formOverlay2.ReturnPicture();
            ChangeImageBoySize();
            pictureBox1.Image = image3;
            pictureBox1.BringToFront();
            Opened = false;
        }
        public void ChangeImageBoySize()
        {
            if (image3 != null)
            {
                if ((image3.Height > pictureBox1.Height))
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            
        }
    }
}
