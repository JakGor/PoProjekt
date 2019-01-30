using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace RezerwacjaSal
{
    /// <summary>
    /// Zmienna enumerowana opisująca płeć
    /// 
    /// </summary>
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
        /// Imię jest typu string, musi być ono dłuższe niż 1 i zawierać wyłącznie znaki polksiego alfabetu 
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
        /// <summary>
        /// Pesel musi być stringiem o 11 cyfr i posiadać poprawną cyfrę kontrolną
        /// </summary>
        [XmlAttribute("Id")]
        [Key]
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
        /// <summary>
        /// Enumerator  Plcie zawiera dane nt płci
        /// </summary>
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
        /// <summary>
        /// Nazwsko musi mieć więcej niż 1 znak
        /// </summary>
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
        /// <param name="imię"> Obiekt typu string zawierający imię</param>
        /// <param name="nazwisko"> Obiekt typu string zawierający nazwisko</param>
        /// <param name="pesel">Obiekt typu string zawierający PESEL</param>
        /// <param name="płeć">Obiekt typu Plcie (enum) przechowujący Płeć</param>
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
        /// <returns> Zwracany jest opis konkretnego najemcy</returns>
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
        /// <param name="nazwa"> Nazwa plku XML</param>
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
        /// <param name="nazwa">Nazwa pliku XML</param>
        /// <returns> Zwraca obiekt typu Najemca</returns>
        public static Najemca OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Najemca));
            Stream s = File.OpenRead(nazwa + ".xml");
            Najemca tmp = (Najemca) xs.Deserialize(s);
            s.Close();
            return tmp;
        }
        /// <summary>
        /// Konieczne może być rzutowanie obiektu po klonowaniu!
        /// Klonowanie polega na serializacji obecnej instancji do pliku o nazwie "Klonowanie_najemcy.xml",
        /// a następnie deserializacji z tego pliku do zupełnie nowego obiektu, który jest zwracany przez metodę
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            ZapiszXml("Klonowanie_najemcy");
            return OdczytajXml("Klonowanie_najemcy");
        }

        /// <summary>
        /// Metoda sprawdzająca czy podany PESEL jest poprawny
        /// </summary>
        /// <param name="PESEL">Obiekt typu string zawierający PESEL</param>
        /// <returns> Wartość true oznacza poprawność nr PESEL</returns>
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
        /// <summary>
        /// Metoda zapisująca najemców do bazy danych SQL
        /// </summary>
        public void ZapisSQL()
        {
            using (var db = new ModelContext())
            {
                var query = from b in db.Najemcy
                            select b;
                foreach (var item in query)
                {
                    if(item.Pesel == this.Pesel)
                    {
                        Console.WriteLine("Taki rekord już istnieje");
                        return;

                    }
                    
                    
                }
                db.Najemcy.Add(this);
                db.SaveChanges();


            }
        }
        /// <summary>
        /// Przykładowe zapytanie skierowane do bazy danych, wypisywane w konsoli
        /// </summary>
        public static void WypiszQuery()
        {
            using (var db = new ModelContext())
            {
                var query = from b in db.Najemcy
                            orderby b.Nazwisko
                            select b;
                foreach (var item in query)
                {                  
                        Console.WriteLine(item);
                        

                }
                


            }
        }
    }
}
