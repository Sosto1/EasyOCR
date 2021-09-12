using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR
{
    class MathStartingDigit
    {
        readonly char[] Equation;
        readonly string Numbers = "1234567890";
        readonly string Characters = "+-*./:=,";
        string UpravenyPriklad = "";
        public MathStartingDigit(char[] Equation)
        {
            this.Equation = Equation;
        }
        public bool GetNegativity()
        {
            UpravenyPriklad = "";
            bool Negative;
            if (Characters.Contains(Equation[0]) && Equation[0] == '-')
            {
                Negative = true;
                for (int i = 1; i < Equation.Length; i++)
                {
                    UpravenyPriklad += Equation[i];
                }
            }
            else
            {
                Negative = false;
                for (int i = 0; i < Equation.Length; i++)
                {
                    UpravenyPriklad += Equation[i];
                }
            }
            return Negative;
        }
        public string GetFinalEquation()
        {
            UpravenyPriklad = "";
            GetNegativity(); //vytvori promennou UpravenyPrilad
            if (Characters.Contains(UpravenyPriklad.Substring(UpravenyPriklad.Length -1)))
            {
                UpravenyPriklad = UpravenyPriklad.Remove(UpravenyPriklad.Length - 1);
                UpravenyPriklad += "=";
            }
            else if (Numbers.Contains(UpravenyPriklad.Substring(UpravenyPriklad.Length - 1)))
            {
                UpravenyPriklad += "=";
            }
            return UpravenyPriklad;
        }

    }
}
