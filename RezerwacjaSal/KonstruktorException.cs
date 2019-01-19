using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezerwacjaSal
{
    /// <summary>
    /// Klasa to wyjątek wypisujący specjalny komunikat, mająca zastosowanie głównie przy sprawdzaniu argumentów konstruktorów
    /// </summary>
    public class KonstruktorException: Exception
    {
        private string str;
        /// <summary>
        /// Konstruktor domyślny, 
        /// </summary>
        public KonstruktorException()
        {
            Str = "Błędny parametr konstruktora";
        }
        /// <summary>
        /// Konstruktor nadpisuje domyślną wiadomość
        /// </summary>
        /// <param name="str"></param>
        public KonstruktorException(string str)
        {
            Str = str;
        }

        public override string Message => str;
            

        public string Str
        {
            get
            {
                return str;
            }

            set
            {
                str = value;
            }
        }
    }
}
