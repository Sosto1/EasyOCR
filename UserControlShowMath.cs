using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCR
{
    public partial class UserControlShowMath : UserControl
    {
        string pri;
        string[] LangCes = new string[] {"Vypočti", "Kopírovat" };
        string[] LangEng = new string[] {"Ok", "Copy"};
        RoundedButton roundedButton_Pocitej = new RoundedButton();
        Settings1 settings = new Settings1();
        public UserControlShowMath()
        {
            InitializeComponent();
            FillTextBoxes();
            LoadButton();
            Console.WriteLine("Priklad: " + pri);
        }
        private void LoadButton()
        {
            if (settings.Language == 0)
                roundedButton_Pocitej.Text = LangCes[0];
            else
                roundedButton_Pocitej.Text = LangEng[0];

            roundedButton_Pocitej.Location = new Point(ClientSize.Width/2 - 50,305);
            roundedButton_Pocitej.Size = new Size(120,50);
            roundedButton_Pocitej.BorderColor = Color.FromArgb(0, 217, 232);
            roundedButton_Pocitej.BackColor = roundedButton_Pocitej.BorderColor;
            roundedButton_Pocitej.Font = new Font("Comnic Sans MS", 14, FontStyle.Bold);
            this.Controls.Add(roundedButton_Pocitej);
            roundedButton_Pocitej.Click += RoundedButton_Pocitej_Click;
            roundedButton_Pocitej.MouseMove += RoundedButton_Pocitej_MouseMove;
            roundedButton_Pocitej.Cursor = Cursors.Hand;
        }
        bool Moved = false;
        private void RoundedButton_Pocitej_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Moved)
            {
                roundedButton_Pocitej.BackColor = Color.FromArgb(0, 200, 215);
                roundedButton_Pocitej.BorderColor = roundedButton_Pocitej.BackColor;
                Moved = true;
            }
        }
        private void UserControlShowMath_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moved)
            {
                roundedButton_Pocitej.BackColor = Color.FromArgb(0, 217, 232);
                roundedButton_Pocitej.BorderColor = roundedButton_Pocitej.BackColor;
                Moved = false;
            }
        }
        private void RoundedButton_Pocitej_Click(object sender, EventArgs e)
        {
            Vypocti(textBox_Priklad.Text);
        }

        public void FillTextBoxes()
        {
            textBox_Priklad.Clear();
            Console.WriteLine("Pisu: " + pri);
            textBox_Priklad.Text = pri;
        }
        public void Vypocti(string priklad)
        {
            ClassMath ma = new ClassMath();
            if (ma.Matika(priklad) != "")
            {
                textBox_Vysledek.Text = ma.Matika(priklad);
            }
        }
        public string Priklad
        {
            get => pri;
            set => pri = value;
        }

       
    }
}
