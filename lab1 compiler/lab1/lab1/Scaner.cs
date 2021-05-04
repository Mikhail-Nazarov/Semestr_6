using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    class Scaner
    {
        string word;
        string[] identificators = new string[]{ "int", "double", "byte", "long", "float"};
        char[] delimiterChars = { ' ', '.', ':', '\t', '\n', ';' };
        char[] termSymbols = {'+', '-', '*', '/'};
        public string result { get; set; } = string.Empty;
        private string text;
        public Scaner(string textBox)
        {
            text = textBox;
            analys();
        }
        bool isIdent()
        {
            foreach(var ident in identificators)
            {
                if (word == ident)
                    return true;
            }
            return false;
        }
        bool isTerm()
        {
            foreach (var term in termSymbols)
            {
                if (word == term.ToString())
                    return true;
            }
            return false;
        }
        bool isOperand()
        {
            try
            {
                double i = Convert.ToDouble(word);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //bool isAlpha()
        //{

        //}
        private void analys()
        {
            char[] combinedDelimit = new char[delimiterChars.Length + termSymbols.Length];
            delimiterChars.CopyTo(combinedDelimit, 0);
            termSymbols.CopyTo(combinedDelimit, delimiterChars.Length);
            string[] str = text.Split(delimiterChars);
            string[] str1 = new string[0];
            int i = 0;
            foreach (var w in str)
            {
                word = w;
                if (isOperand())
                {
                    Array.Resize(ref str1, str1.Length + 1);
                    str1[i] = word;
                    result+= word + " ";
                    i++;
                }
                else
                {
                    var tmp = w.Split(',');
                    for (int j=0; j < tmp.Length; j++)
                    {
                        Array.Resize(ref str1, str1.Length + 1);
                        str1[i] = tmp[j];
                        word = str1[i];
                        if (isIdent())
                            result += word + " ";
                        if (isTerm())
                            result += word + " ";
                        i++;
                    }
                }
            }
            combinedDelimit = new char[delimiterChars.Length + 1];
            //delimiterChars.CopyTo(combinedDelimit, 0);
            //combinedDelimit[combinedDelimit.Length - 1] = ',';
            //str = text.Split(combinedDelimit);
            //foreach (var w in str)
            //{
            //    word = w;
            //    if (isIdent())
            //        result += word + " ";
            //}
        }
    }
}
