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
    class ClassMath
    {
        FormOverlay2_Name formOverlay2;
        bool Opened = false;
        readonly string Numbers = "1234567890";
        readonly string Characters = "+-*./:=,";
        String text;
        public void CreatePicture()
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
        Image image;
        private void FormOverlay2_FormClosed(object sender, FormClosedEventArgs e)
        {
            image = formOverlay2.ReturnPicture();
            text = ImageTools.OCRFromImage(image).Trim();
            Opened = false;
        }
        public string ReturnTextFromImage()
        {
            return text;
        }
        public string Matika(String priklad)
        {
            char[] Equation = null;
            if (priklad != "")
            {
                Equation = priklad.ToCharArray();
            }
            if (Kontrola(Equation)) //pokud priklad projde kontrolou
            {
                MathStartingDigit mth = new MathStartingDigit(Equation);
                bool negative = mth.GetNegativity(); //zjisti jestli je prvni cislo + nebo -
                string UpravenyPriklad = mth.GetFinalEquation(); //vrati upraveny priklad
                StringToResult STR = new StringToResult(UpravenyPriklad, negative);
                Console.WriteLine(STR.ReturnResult());
                return STR.ReturnResult();
            }
            else
                return null;
        }
        public bool Kontrola(char[] Equation)
        {
            bool allowed = false;
            if (Equation != null)
            {
                allowed = true;
            }
            if (allowed)
            {
                if (Equation[0] == '-' || Numbers.Contains(Equation[0]))
                {
                    allowed = true;
                }
                else
                {
                    allowed = false;
                    Console.WriteLine("Chyba na zacatku");
                }
            }
            if (allowed)
            {
                foreach (var item in Equation)
                {
                    if (Numbers.Contains(item) || Characters.Contains(item))
                    {
                        allowed = true;
                    }
                    else
                    {
                        allowed = false;
                        Console.WriteLine("Nalezen chybny znak");
                        break;
                    }
                }
            }           
            if (allowed)
            {
                Console.WriteLine("Haloooo");
                for (int i = 0; i < Equation.Length; i++)
                {
                    if (Characters.Contains(Equation[i]) && i < Equation.Length - 1) //kdyz je current index znamenko
                    {
                        if (Characters.Contains(Equation[i+1]))
                        {
                            allowed = false;
                        }
                    }
                }
            }
            return allowed;
        }
    }
}
