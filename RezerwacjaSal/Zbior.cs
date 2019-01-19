using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RezerwacjaSal
{

    /// <summary>
    /// Klasa zawiera listę wszystkich klientów (najemców) oraz budynków. Powstała w celu ułatwinia współpracy z GUI
    /// </summary>
     public class Zbior
    {
        /**********************************************************  Pola  **/
        private List<Budynek> spisBudynkow;
        private List<Najemca> listaNajemcow;
        private int rezerwacjeRazem;
        private int numer;
        /**********************************************************  Właściwości  **/
        public List<Budynek> SpisBudynkow
        {
            get
            {
                return spisBudynkow;
            }

            set
            {
                spisBudynkow = value;
            }
        }

        public List<Najemca> ListaNajemcow
        {
            get
            {
                return listaNajemcow;
            }

            set
            {
                listaNajemcow = value;
            }
        }

        public int RezerwacjeRazem
        {
            get
            {
                return rezerwacjeRazem;
            }

            set
            {
                rezerwacjeRazem = value;
            }
        }

        public int Numer
        {
            get
            {
                return numer;
            }

            set
            {
                numer = value;
            }
        }


        /**********************************************************  Konstruktory  **/

        public Zbior()
        {
            listaNajemcow = new List<Najemca>();
            spisBudynkow = new List<Budynek>();
            numer = 0;
            rezerwacjeRazem = 0;
        }

        /**********************************************************  Metody  **/

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("LISTA budynków: "+Environment.NewLine);
            foreach (Budynek b in spisBudynkow)
            {
                
                sb.Append("Nazwa budynku: " + b.Nazwa + Environment.NewLine + "Udogodnienia: " + Environment.NewLine);
                sb.Append("Wi-Fi: " + b.Wifi + Environment.NewLine + "Winda: " + b.Winda + Environment.NewLine + "Gastronomia: " + b.Gastronomia + Environment.NewLine);
                sb.Append("Lista sal: " + Environment.NewLine);
                foreach (Sala s in b.ListaSal)
                {
                    sb.Append(s.Nazwa + Environment.NewLine);
                    sb.Append(s.Typ + Environment.NewLine);
                    sb.Append(s.Pojemność + Environment.NewLine);
                }

            }
            sb.Append("Spis Najemców: "+Environment.NewLine);
            foreach( Najemca n in listaNajemcow)
            {
                sb.Append(n + Environment.NewLine);
            }
            return sb.ToString();
        }

        public void DodajBudynek(Budynek b)
        {
            SpisBudynkow.Add(b);
        }

        public void UsunBudynek(Budynek b)
        {
            SpisBudynkow.Remove(b);
        }
        public void DodajNajemce(Najemca n)
        {
            ListaNajemcow.Add(n);
        }
        public void UsunNajemce(Najemca n)
        {
            ListaNajemcow.Remove(n);
        }
        public string WypiszSale(int i)
        {
            Budynek b = spisBudynkow[i];
            StringBuilder sb = new StringBuilder();
            
            foreach (Sala s in b.ListaSal)
            {
                sb.Append(s.Nazwa +" ( Typ: "+s.Typ+", Pojemność: "+s.Pojemność+" )"+ Environment.NewLine);
                
            }
            return sb.ToString();
        }

        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa"></param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Zbior));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }
        /// <summary>
        /// Deserializacja z pliku do obiektu zwracanego przez metodę 
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns></returns>
        public static Zbior OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Zbior));
            Stream s = File.OpenRead(nazwa + ".xml");
            Zbior tmp = (Zbior)xs.Deserialize(s);
            s.Close();
            return tmp;
        }
    }
}
