using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
namespace RezerwacjaSal
{
    /// <summary>
    /// Klasa zawiera podstawowe dane o budynku: wchodzące w jego skład sale i informacje o użytecznościach np. windzie
    /// </summary>
    public class Budynek 
    {
        /**********************************************************  Pola  **/
        private string nazwa;
        private List<Sala> listaSal;
        private bool winda;
        private bool gastronomia;
        private bool wifi;
        /**********************************************************  Właściwości  **/
        /// <summary>
        /// Nazwa budynku
        /// </summary>
        [XmlAttribute("Id")]
        public string Nazwa
        {
            get
            {
                return nazwa;
            }

            set
            {
                if (value.Length == 0)
                {
                    throw new KonstruktorException("Brak nazwy budynku");
                }
                nazwa = value;
            }
        }
        /// <summary>
        /// Lista sal w budynku
        /// </summary>
        public List<Sala> ListaSal
        {
            get
            {
                return listaSal;
            }

            set
            {
                listaSal = value;
            }
        }
        /// <summary>
        /// Zmienna logiczna odpowiadająca na pytanie czy w budynku jest winda
        /// </summary>
        public bool Winda
        {
            get
            {
                return winda;
            }

            set
            {
                winda = value;
            }
        }
        /// <summary>
        /// Zmienna logiczna odpowiadająca na pytanie czy w budynku jest zaplecze gastronomiczne
        /// </summary>
        public bool Gastronomia
        {
            get
            {
                return gastronomia;
            }

            set
            {
                gastronomia = value;
            }
        }
        /// <summary>
        /// Zmienna logiczna odpowiadająca na pytanie czy w budynku jest ogólnodostępne Wi-Fi
        /// </summary>
        public bool Wifi
        {
            get
            {
                return wifi;
            }

            set
            {
                wifi = value;
            }
        }



        /**********************************************************  Konstruktory  **/
        /// <summary>
        /// Pusty konstruktor na potrzevy serializacji, nie zaleca się jego używania
        /// </summary>
        public Budynek()
        {
            nazwa = default(string);
            listaSal = new List<Sala>();
            wifi = default(bool);
            gastronomia = default(bool);
            wifi = default(bool);
        }
        /// <summary>
        /// Główny konstruktor, użyteczności wpisywać za pomocą true lub false
        /// </summary>
        /// <param name="nazwa">Nazwa budynku</param>
        /// <param name="winda">Winda</param>
        /// <param name="gastronomia">Gastronomia</param>
        /// <param name="wifi">Internet bezprzewodowy</param>
        public Budynek(string nazwa, bool winda, bool gastronomia, bool wifi)
        {

            Nazwa = nazwa;
            ListaSal = new List<Sala>();
            Winda = winda;
            Gastronomia = gastronomia;
            Wifi = wifi;
        }
        /**********************************************************  Metody  **/
        /// <summary>
        /// Dodaje salę, a jeżeli taka istnieje to wypisuje komunikat a program dalej działa
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Nazwa budynku: " + Nazwa + Environment.NewLine+"Udogodnienia: "+Environment.NewLine);
            sb.Append("Wi-Fi: " + Wifi + Environment.NewLine + "Winda: " + Winda + Environment.NewLine + "Gastronomia: " + Gastronomia + Environment.NewLine);
            sb.Append("Lista sal: " + Environment.NewLine);
            foreach (Sala s  in listaSal)
            {
                sb.Append(s + Environment.NewLine);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Dodaje salę do budynku
        /// </summary>
        /// <param name="s">Obiekt typu sala</param>
        public void DodajSale(Sala s)
        {
            if (ListaSal.Exists(x => x.Nazwa == s.Nazwa))
            {
                Console.WriteLine("Taka sala już istnieje");
            }
            else
            {
                ListaSal.Add(s);
                s.Id = ++Sala.Kolejność_sal;
                ListaSal.Sort();

            }
        }
        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Budynek));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }
        /// <summary>
        /// Deserializacja z pliku do obiektu zwracanego przez metodę 
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        /// <returns>Zwraca obiekt typu Budynek</returns>
        public static Budynek OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Budynek));
            Stream s = File.OpenRead(nazwa + ".xml");
            Budynek tmp = (Budynek)xs.Deserialize(s);
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
            ZapiszXml("Klonowanie_Budynku");
            return OdczytajXml("Klonowanie_Budynku");
        }

    }
}
