using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RezerwacjaSal
{
    /// <summary>
    /// Klasa zawiera dane o rezerwacji m.in. dane najemcy, datę i czas.
    /// Wchodzi w skład klasy Sala.
    /// Wykorzystuje interfejsy IComparable i ICloneable, podlega serializacji xml.
    /// </summary>
    public class Rezerwacja : IComparable<Rezerwacja>, ICloneable
    {
        /**********************************************************  Pola  **/
        private int numer; // -1 oznacza pusty konstruktor, 0 oznacza że nie ma na liście rezerwacji, liczba naturalna jest na liście
        private Najemca najem;
        private DateTime dzień;
        private TimeSpan godz_pocz;
        private TimeSpan godzina_końcowa;
        private static int kolejność_rezerwacji = 0;
        //public string Pesel { get; set; } // dodane
        /**********************************************************  Właściwości  **/
        /// <summary>
        /// Nr rezerwacji pozwalający na jej identyfikację
        /// </summary>
        [Key]
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
        /// <summary>
        /// Sprawdza czy najemca nie był utworzony pustym konstruktorem
        /// </summary>
        public Najemca Najem // dodane virtual
        {
            get
            {
                return najem;
            }

            set
            {
                if (value.Pesel[0] == '0')
                {
                    throw new KonstruktorException("Rezerwacja utworzona njemcą utworzonym pustym konstruktorem");
                }
                najem = value;
            }
        }
        /// <summary>
        /// Sprawdza czy data nie jest ustawiana na przeszłość
        /// </summary>
        public DateTime Dzień
        {
            get
            {
                return dzień;
            }

            set
            {
                if (value.Date < DateTime.Now.Date)
                {
                    //throw new KonstruktorException("Zła data w rezerwacji");

                }
                else
                {
                    dzień = value;
                }
            }
        }
        /// <summary>
        /// Sprawdza czy godzina nie jest ustawiana na przeszłość, nie podlega serializacji bo to TimeSpan
        /// </summary>
        [XmlIgnore]
        [NotMappedAttribute]
        public TimeSpan Godz_pocz
        {
            get
            {
                return godz_pocz;
            }

            set
            {
                //if (DateTime.Now.Date == Dzień.Date)
                //{
                //    if (DateTime.Now.TimeOfDay > value)
                //    {

                //        throw new KonstruktorException("Nie można cofnąć się w czasie (godzina otwarcia)");
                //    }
                //}

                godz_pocz = value;
            }
        }
        /// <summary>
        /// Sposób na serializacje TimeSpan-użycie metody Ticks(najmniejsz jednostka czasu w .NET), która zwraca wartość w typie long
        /// </summary>
        public long Godz_pocz_serializacja
        {
            get { return Godz_pocz.Ticks; }
            set { Godz_pocz = new TimeSpan(value); }
        }
        /// <summary>
        /// Sprawdza czy godzina nie jest ustawiana na przeszłość, nie podlega serializacji bo to TimeSpan
        /// </summary>
        [XmlIgnore]
        [NotMapped]
        public TimeSpan Godzina_końcowa
        {
            get
            {
                return godzina_końcowa;
            }

            set
            {
                //if (DateTime.Now.Date == Dzień.Date)
                //{
                //    if (DateTime.Now.TimeOfDay > value)
                //    {
                //        throw new KonstruktorException("Nie można cofnąć się w czasie (godzina końcowa)");
                //    }
                //}
                godzina_końcowa = value;
            }
        }
        /// <summary>
        /// Sposób na serializacje TimeSpan-użycie metody Ticks(najmniejsz jednostka czasu w .NET), która zwraca wartość w typie long
        /// </summary>
        public long Godzina_końcowa_serializacja
        {
            get { return Godzina_końcowa.Ticks; }
            set { Godzina_końcowa = new TimeSpan(value); }
        }



        /// <summary>
        /// Służy do automatycznego nadawania numerów dla rezerwacji,ale jeżeli zostaną poprawnie dodane do sali
        /// </summary>
        public static int Kolejność_rezerwacji
        {
            get
            {
                return kolejność_rezerwacji;
            }

            set
            {
                kolejność_rezerwacji = value;
            }
        }






        /**********************************************************  Konstruktory  **/
        /// <summary>
        /// Pusty konstruktor na potrzeby serializacji, ustawia numer na -1 tak można rozpoznać obiekt
        /// </summary>
        public Rezerwacja()
        {
            numer = -1;
            najem = new Najemca();
            dzień = default(DateTime);
            godz_pocz = default(TimeSpan);
            godzina_końcowa = default(TimeSpan);
        }
        /// <summary>
        /// Główny konstruktor podczas pracy z programem używać tylko jego,
        /// Możliwe formaty daty: "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd/MM/yyyy", "dd-MM-yyyy"
        /// Format godziny: "H:mm"
        /// </summary>
        /// <param name="najemca"> Obiekt klasy najemca</param>
        /// <param name="dzień"> Dzień w którym chcemy dokonać rezerwacji</param>
        /// <param name="godzina_początkowa"> Godzina początkowa rezerwacji</param>
        /// <param name="godzina_końcowa">Godzina końcowa rezerwacji</param>
        public Rezerwacja(Najemca najemca, string dzień, string godzina_początkowa, string godzina_końcowa)
        {
            Numer = 0;
            Najem = najemca;
            DateTime newDay;
            DateTime.TryParseExact(dzień, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd/MM/yyyy", "dd-MM-yyyy" }, null, DateTimeStyles.None, out newDay);
            Dzień = newDay;//
            DateTime nowaGodzinaPocz;
            if (!DateTime.TryParseExact(godzina_początkowa, "H:mm", null, DateTimeStyles.None, out nowaGodzinaPocz))
            {
                throw new KonstruktorException("Zły format godziny początkowej");
            }
            Godz_pocz = nowaGodzinaPocz.TimeOfDay;
            DateTime nowaGodzinaKoń;
            if (!DateTime.TryParseExact(godzina_końcowa, "H:mm", null, DateTimeStyles.None, out nowaGodzinaKoń))
            {
                throw new KonstruktorException("Zły format godziny końcowej");
            }
            Godzina_końcowa = nowaGodzinaKoń.TimeOfDay;
            if (Godzina_końcowa <= Godz_pocz)
            {
                throw new KonstruktorException("godzina początkowa nie może być większa od końcowej");
            }
        }
        /**********************************************************  Metody  **/
        /// <summary>
        /// Zwraca string zawierający: numer rezerwacji, dane najemcy, datę, godziny
        /// </summary>
        /// <returns> Zwraca opis rezerwacji</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Numer rezerwacji: ").AppendLine(Numer.ToString());
            sb.Append(Najem.ToString());
            sb.Append("Data: ").AppendLine(Dzień.ToShortDateString());
            sb.Append("Od godziny: ").AppendLine(Godz_pocz.ToString(@"hh\:mm"));
            sb.Append("Do godziny: ").AppendLine(Godzina_końcowa.ToString(@"hh\:mm"));
            return sb.ToString();
        }
        /// <summary>
        /// porównuje Rezerwacje najpierw po dniu potem na podstawie godziny początkowej
        /// </summary>
        /// <param name="r">Obiekt typu rezerwacja</param>
        /// <returns></returns>
        public int CompareTo(Rezerwacja r)
        {
            if (Dzień < r.Dzień)
            {
                return -1;
            }
            else if (Dzień == r.Dzień)
            {
                return Godz_pocz.CompareTo(r.Godz_pocz);
            }
            return 1;
        }
        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Rezerwacja));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }
        /// <summary>
        /// Deserializacja z pliku do obiektu zwracanego przez metodę 
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        /// <returns></returns>
        public static Rezerwacja OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Rezerwacja));
            Stream s = File.OpenRead(nazwa + ".xml");
            Rezerwacja tmp = (Rezerwacja)xs.Deserialize(s);
            s.Close();
            return tmp;
        }
        /// <summary>
        /// Konieczne może być rzutowanie obiektu po klonowaniu!!!
        /// Klonowanie polega na serializacji obecnej instancji do pliku o nazwie "Klonowanie_najemcy.xml",
        /// a następnie deserializacji z tego pliku do zupełnie nowego obiektu, który jest zwracany przez metodę
        /// </summary>
        /// <returns> Zwraca obiekt będący lonem</returns>
        public object Clone()
        {
            ZapiszXml("Klonowanie_Rezerwacja");
            return OdczytajXml("Klonowanie_Rezerwacja");
        }
        //public void ZapisSQL()
        //{
        //    using (var db = new ModelContext())
        //    {
        //        var query = from b in db.Rezerwacje
        //                    select b;
        //        foreach (var item in query)
        //        {
        //            if (item.Numer == this.Numer)
        //            {
        //                Console.WriteLine("Taki rekord już istnieje");
        //                return;

        //            }


        //        }
        //        this.Najem = db.Najemcy.Find(this.Najem.Pesel);
        //        this.numer+=3;
        //        db.Rezerwacje.Add(this);
        //        db.SaveChanges();
        //    }
        //}
    }
}
