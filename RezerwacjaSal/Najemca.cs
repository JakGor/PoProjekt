using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
namespace RezerwacjaSal
{
    public enum Płcie { K,M}
    /// <summary>
    /// Klasa zawiera podstawowe dane o najemcy, który może zarezerwować salę.
    /// Wchodzi w skład klasy rezerwacja.
    /// Dziedziczy po klasie dodatki i wykorzystuje interfejs ICloneable.
    /// Podlega serializacji do xml.
    /// </summary>
    public class Najemca: Dodatki, ICloneable
    {
        /**********************************************************  Pola  **/
        private string imię;
        private string nazwisko;
        private string pesel;
        private Płcie płeć;
        /**********************************************************  Właściwości  **/
        /// <summary>
        /// String musi być dłuższy niż 1 i zawierać wyłącznie znaki polksiego alfabetu
        /// </summary>
        
        /// <summary>
        /// String musi być dłuższy niż 1 i zawierać wyłącznie znaki polksiego alfabetu
        /// </summary>
        
        
        public string Imię
        {
            get
            {
                return imię;
            }

            set
            {
                if (base.SprawdźString(value, StringZnaki))
                {
                    imię = value.ToUpper();// Wszystko na duże litery aby nie bawić się w rozróżnienia (można usunąć)
                }
                else
                {
                    throw new KonstruktorException("Błędne imię najemcy");
                }
            }
        }
        [XmlAttribute("Id")]
        public string Pesel
        {
            get
            {
                return pesel;
            }          
            set
            {
                if (sprawdzPESEL(value))
                {
                    pesel = value;

                }
                else
                {
                    throw new KonstruktorException("Wprowadzono niepoprawny PESEL");
                }
            }
        }

        public Płcie Płeć
        {
            get
            {
                return płeć;
            }

            set
            {
                płeć = value;
            }
        }

        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }

            set
            {
                if (base.SprawdźString(value, StringZnaki))
                {
                    nazwisko = value.ToUpper();
                }
                else
                {
                    throw new KonstruktorException("Błędne nazwisko najemcy");
                }
            }
        }

        /**********************************************************  Konstruktory  **/
        /// <summary>
        /// Pusty konstruktor na potrzeby serializacji, ustawia pesel na 11 zer, nie zaleca się jego używania
        /// </summary>
        public Najemca()
        {
            imię = default(string);
            nazwisko = default(string);
            pesel = new string('0', 11);
            płeć = default(Płcie);
        }
        /// <summary>
        /// Główny konstruktor podczas pracy z programem używać tylko jego, używa właściwości sprawdzających poprawność danych
        /// </summary>
        /// <param name="imię"></param>
        /// <param name="nazwisko"></param>
        /// <param name="pesel"></param>
        /// <param name="płeć"></param>
        public Najemca(string imię, string nazwisko, string pesel, Płcie płeć)
        {
            Imię = imię;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Płeć = płeć;
        }
        /**********************************************************  Metody  **/
        /// <summary>
        /// Zwraca sformatowany string zawierający: imie, nazwisko, płeć, 
        /// pesel jest pomijany ponieważ stanowi hasło do usuwania rezerwacji
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Najemca:");
            sb.Append("Imię: ").AppendLine(Imię);
            sb.Append("Nazwisko: ").AppendLine(Nazwisko);
            //sb.Append("PESEL: ").AppendLine(Pesel);
            sb.Append("Płeć: ").AppendLine(Płeć.ToString());
            return sb.ToString();
        }
        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa"></param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Najemca));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }
        /// <summary>
        /// Deserializacja z pliku do obiektu zwracanego przez metodę 
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns></returns>
        public static Najemca OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Najemca));
            Stream s = File.OpenRead(nazwa + ".xml");
            Najemca tmp = (Najemca) xs.Deserialize(s);
            s.Close();
            return tmp;
        }
        /// <summary>
        /// Konieczne może być rzutowanie obiektu po klonowaniu!!!
        /// Klonowanie polega na serializacji obecnej instancji do pliku o nazwie "Klonowanie_najemcy.xml",
        /// a następnie deserializacji z tego pliku do zupełnie nowego obiektu, który jest zwracany przez metodę
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            ZapiszXml("Klonowanie_najemcy");
            return OdczytajXml("Klonowanie_najemcy");
        }


        public bool sprawdzPESEL(string PESEL)
        {

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3};
            int sumaKontrolna = 0;
            ///sprawdzanie czy każdy element z numeru PESEL jest liczbą oraz czy długość wynosi 11 cyfr
            for (int i = 0; i < PESEL.Length - 1; i++)
            {
                if (Char.IsNumber(PESEL[i]) == false || PESEL.Length != 11)
                {
                    return false;
                }
            }
            //Console.WriteLine("Pesel jest z cyfr i ma długość OK");

            ///obliczanie sumy kontrolnej (waga * kolejne cyfry z PESELu)
            sumaKontrolna = Convert.ToInt32(PESEL[0].ToString()) * wagi[0] +
                Convert.ToInt32(PESEL[1].ToString()) * wagi[1] +
                Convert.ToInt32(PESEL[2].ToString()) * wagi[2] +
                Convert.ToInt32(PESEL[3].ToString()) * wagi[3] +
                Convert.ToInt32(PESEL[4].ToString()) * wagi[4] +
                Convert.ToInt32(PESEL[5].ToString()) * wagi[5] +
                Convert.ToInt32(PESEL[6].ToString()) * wagi[6] +
                Convert.ToInt32(PESEL[7].ToString()) * wagi[7] +
                Convert.ToInt32(PESEL[8].ToString()) * wagi[8] +
                Convert.ToInt32(PESEL[9].ToString()) * wagi[9];
            
            sumaKontrolna %= 10;
            sumaKontrolna = 10 - sumaKontrolna;
            sumaKontrolna %= 10;
            
            
            ///sprawdzenie czy suma kontrolna zgadza się z ostatnią cyfrą PESELu
            if (sumaKontrolna == Convert.ToInt32(PESEL[10].ToString()))
            {
                return true;
            }
            else
                return false;


        }
    }
}
