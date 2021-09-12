using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCR
{
    class StringToResult
    {
        readonly string UpravenyPriklad;
        readonly bool negative;
        readonly string Numbers = "1234567890";
        readonly string Characters = "+-*./:=,";

        public StringToResult(string Equation, bool negative)
        {
            this.UpravenyPriklad = Equation;
            this.negative = negative;
        }
        public string ReturnResult()
        {
            string[,] StringGrid = GetDataGrid(UpravenyPriklad); //vytvori 2D grid pro znameka  
            Int64[] NUMBER_CHUNKS = ReturnNumberChunks(UpravenyPriklad, StringGrid.Length / 2, negative); //vytvori kusy cisel z prikladu
            return MakeOperation(NUMBER_CHUNKS, StringGrid); //provede matamatickou operaci a vrati vysledek
        }
        private string[,] GetDataGrid(string UpravenyPriklad)
        {
            int POCET_ZNAMENEK = 0;
            foreach (var item in UpravenyPriklad)
            {
                if (Characters.Contains(item))
                {
                    POCET_ZNAMENEK++;
                }
            }

            string[,] StringGrid = new string[POCET_ZNAMENEK, 2]; //2D array nahore indexy znamenek pod nima to znamenko
            int CHAR_COUNTER = 0;
            int S = 0;
            foreach (var item in UpravenyPriklad)
            {
                if (Characters.Contains(item))
                {
                    StringGrid[S, 0] = CHAR_COUNTER.ToString(); // na prvnim radku to da index znamenka
                    StringGrid[S, 1] = item.ToString(); //na druhem radku da znak
                    S++;
                }
                CHAR_COUNTER++;
            }
            return StringGrid;
        }
        private long[] ReturnNumberChunks(string UpravenyPriklad, int NUMBER_OF_CHUNKS, bool FirstNumNegative)
        {
            long[] NUMBER_CHUNKS = new long[NUMBER_OF_CHUNKS];
            int x = 0;
            string Priklad = "";
            foreach (var item in UpravenyPriklad)
            {
                if (Numbers.Contains(item))
                {
                    Priklad += item;
                }
                else if (Characters.Contains(item))
                {
                    try { NUMBER_CHUNKS[x] = long.Parse(Priklad); } catch { MessageBox.Show("Value of numer excedeed number limit!"); }//tohle dej potom do promenne
                    Priklad = "";
                    x++;
                }
            }

            if (FirstNumNegative)
            {
                NUMBER_CHUNKS[0] = NUMBER_CHUNKS[0] * -1;
            }
            return NUMBER_CHUNKS;
        }
        private string MakeOperation(Int64[] NUMBER_CHUNKS, string[,] StringGrid)
        {
            long VYSLEDEK = NUMBER_CHUNKS[0];
            int NUMBER_INDEX = 1;
            for (int i = 0; i < NUMBER_CHUNKS.Length - 1; i++)
            {
                switch (StringGrid[i, 1])
                {
                    case "+":
                        VYSLEDEK = VYSLEDEK + NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                    case "-":
                        VYSLEDEK = VYSLEDEK - NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                    case ":":
                        VYSLEDEK = VYSLEDEK / NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                    case "/":
                        VYSLEDEK = VYSLEDEK / NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                    case ".":
                        VYSLEDEK = VYSLEDEK * NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                    case "x":
                        VYSLEDEK = VYSLEDEK * NUMBER_CHUNKS[NUMBER_INDEX];
                        break;
                }
                NUMBER_INDEX++;
            }
            return VYSLEDEK.ToString();
        }
        
    }
}
