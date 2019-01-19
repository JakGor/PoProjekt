using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezerwacjaSal
{
    /// <summary>
    /// Klasa abstrakcyjna zawierająca dodatkowe elementy
    /// </summary>
    abstract public class Dodatki
    {
        static private char[] stringZnaki = new char[] { 'ą', 'Ć', 'ć', 'ę', 'Ł', 'ł', 'Ń',
                'ń', 'Ó', 'ó', 'Ś', 'ś','Ś','Ź', 'ź','Ż','ż' };
        /// <summary>
        /// Zawiera znaki akceptowalne podczas wpisywania stringów 
        /// </summary>
       
        public static char[] StringZnaki
        {
            get
            {
                return stringZnaki;
            }

            set
            {
                stringZnaki = value;
            }
        }

        /// <summary>
        /// Sprawdza czy długość stringa jest większa od 1 i czy zawiera tylko litery łacińskiego alfabetu lub znaki
        /// podane przez nas w tablicy znaków
        /// </summary>
        /// <param name="str"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public virtual bool SprawdźString(string str, char[] arr)// nadaje się jako funkcja do klasy abstrakcyjnej
        {
            if (str.Length <= 1) return false;
            else
            {
                foreach (var item in str)
                {
                    if (arr.Contains(item) || (item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        // nic nie robi
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
