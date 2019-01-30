using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezerwacjaSal
{
    /// <summary>
    /// Enumerator oznaczający typ sali
    /// </summary>
    public enum Właściwość { komputerowa, aula, ćwiczeniowa}
    /// <summary>
    /// Klasa zawiera podstawowe dane o sali oraz wszystkie rezerwacje tej sali.
    /// Wchodzi w skład klasy Budynek.
    /// Wykorzystuje interfejsy IComparable i ICloneable, podlega serializacji xml.
    /// </summary>
    public class Sala :  IRezerwowalny, IComparable<Sala>
    {
        /**********************************************************  Pola  **/
        private string nazwa;
        private int id;
        private Właściwość typ;
        private int pojemność;
        private List<Rezerwacja> listaRezerwacji;
        private int liczbaRezerwacji;
        private static int kolejność_sal = 0;
        /**********************************************************  Właściwości  **/
        
        
        /// <summary>
        /// Służy do automatycznego nadawania id
        /// </summary>
        [Key]
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
                    throw new KonstruktorException("Brak nazwy sali");
                }
                nazwa = value;
            }
        }
        /// <summary>
        /// Id nadaje unikalny nr sali w budynku
        /// </summary>
        [NotMapped]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        /// <summary>
        /// Typ sali (zmienna enumerowana)
        /// </summary>
        public Właściwość Typ
        {
            get
            {
                return typ;
            }

            set
            {
                typ = value;
            }
        }
        /// <summary>
        /// Pojemność oznacza ile miejsc znajduje sięw sali
        /// </summary>
        public int Pojemność
        {
            get
            {
                return pojemność;
            }

            set
            {
                pojemność = value;
            }
        }
        /// <summary>
        /// Lista rezerwacji danej sali
        /// </summary>
        public  List<Rezerwacja> ListaRezerwacji
        {
            get
            {
                return listaRezerwacji;
            }

            set
            {
                listaRezerwacji = value;
            }
        }
        /// <summary>
        /// Liczba rezerwacji danej sali
        /// </summary>
        public int LiczbaRezerwacji
        {
            get
            {
                return liczbaRezerwacji;
            }

            set
            {
                liczbaRezerwacji = value;
            }
        }
        /// <summary>
        /// Zmienna statyczna oznaczająca kolejność sal
        /// </summary>
        public static int Kolejność_sal
        {
            get
            {
                return kolejność_sal;
            }

            set
            {
                kolejność_sal = value;
            }
        }

        /**********************************************************  Konstruktory  **/
        /// <summary>
        /// Pusty konstruktor na potrzeby serializacji, ustawia Id na -1 tak można rozpoznać obiekt
        /// </summary>
        public Sala()
        {
            nazwa = default(string);
            id = -1; // Wartość odróżnia błędną sale
            typ = default(Właściwość);
            pojemność = default(int);
            listaRezerwacji = new List<Rezerwacja>();
            liczbaRezerwacji = default(int);
        }
        /// <summary>
        /// Główny konstruktor do pracy w programie
        /// </summary>
        /// <param name="nazwa">Nazwa sali</param>
        /// <param name="typ">Typ sali</param>
        /// <param name="pojemność">Liczba miejsc</param>
        public Sala(string nazwa, Właściwość typ, int pojemność)
        {
            Nazwa = nazwa;
            Typ = typ;
            Pojemność = pojemność;
            ListaRezerwacji = new List<Rezerwacja>();
            LiczbaRezerwacji = 0;
            Id = ++Kolejność_sal;
        }
        /**********************************************************  Metody  **/
        /// <summary>
        /// Zwraca string zawierający: nazwę, id, typ, pojemność oraz wszystkie rezerwacje
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Nazwa: ").AppendLine(Nazwa);
        //    sb.Append("Id: ").AppendLine(Id.ToString());
        //    sb.Append("Typ: ").AppendLine(Typ.ToString());
        //    sb.Append("Pojemność: ").AppendLine(Pojemność.ToString());
        //    sb.Append("Liczba rezerwacji: ").AppendLine(LiczbaRezerwacji.ToString());
        //    if (LiczbaRezerwacji == 0)
        //    {
        //        sb.AppendLine("Brak rezerwacji!!!");
        //    }
        //    else
        //    {
        //        sb.AppendLine("***************************************************");
        //        foreach (Rezerwacja r in ListaRezerwacji)sb.AppendLine(r.ToString());
        //        sb.AppendLine("***************************************************");
        //    }
        //    return sb.ToString();
        //}
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Nazwa: " + Nazwa + " (Typ: " + Typ + " Pojemność: " + Pojemność+")");
            return sb.ToString();
        }

        /// <summary>
        /// Metoda sprawdza, czy rezerwacja o danej godzinie początkowej i końcowej jest możliwa
        /// </summary>
        /// <param name="r">Obiekt rezerwacja</param>
        /// <returns>Zwraca prawdę, gdy można dodać rezerwację</returns>
        public bool SprawdźRezerwację(Rezerwacja r)
        {
            //if(r.Numer == -1)
            //{
            //    return false; // rezerwacja została stworzona pustym konstruktorem więc nie można jej dodać
            //}
            foreach (var item in ListaRezerwacji)
            {
                if (item.Dzień.Date == r.Dzień.Date)
                {

                    if ((r.Godz_pocz >= item.Godz_pocz && r.Godz_pocz < item.Godzina_końcowa))
                    {
                        
                        return false;
                    } else if (r.Godzina_końcowa > item.Godz_pocz && r.Godzina_końcowa <= item.Godzina_końcowa)
                    {
                        
                        return false;
                    }
                    else if (r.Godz_pocz < item.Godz_pocz && r.Godzina_końcowa > item.Godzina_końcowa)
                    {
                        
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Dodawanie rezerwacji, gdy jest zajęta wyskakuje komunikat ale pracujemy dalej, gdy doda się sale pomyślnie
        /// dostaje ona przydzielony numer, lista sortowana jest po każdym dodaniu
        /// </summary>
        /// <param name="r">Obiekt typu rezerwacja</param>
        public void DodajRezerwację(Rezerwacja r)
        {
            if (SprawdźRezerwację(r))
            {
                ListaRezerwacji.Add(r);
                LiczbaRezerwacji++;
                r.Numer = ++Rezerwacja.Kolejność_rezerwacji;
                Sortuj();
            }
            else
            {
                Console.WriteLine("Sala zajęta");
            }
        }
        /// <summary>
        /// Metoda dodawani sali z ręcznym ustawieniem numeru. Używana w czasie inicjalizacji GUI
        /// </summary>
        /// <param name="r">Obiekt typu rezerwacja</param>
        /// <param name="nr">Ręcznie podany nr rezerwacji</param>
        public void DodajRezerwację(Rezerwacja r, int nr)
        {
            if (SprawdźRezerwację(r))
            {
                ListaRezerwacji.Add(r);
                LiczbaRezerwacji++;
                r.Numer = nr;
                //if(nr!=0)
                if (Rezerwacja.Kolejność_rezerwacji < nr)
                {
                    Rezerwacja.Kolejność_rezerwacji = nr;
                }
                
                Sortuj();
            }
            else
            {
                Console.WriteLine("Sala zajęta");
            }
        }

        /// <summary>
        /// Usuwa rezerwację jeżeli poda się prawidłowy pesel, pierwszym argumentem powinna być funkcja ZnajdźRezerwację
        /// z oodpowiednim parametrem wyszukiwania
        /// </summary>
        /// <param name="r">Obiekt typu rezerwacja</param>
        /// <param name="pesel">Pesel jest jednicześnie hasłem</param>
        public void UsuńRezerwację(Rezerwacja r, string pesel)
        {
            if (r.Najem.Pesel == pesel)
            {
                ListaRezerwacji.Remove(r);
                LiczbaRezerwacji--;
            }
            else
            {
                Console.WriteLine("Błędny PESEL!!!");
            }
        }
        
        /// <summary>
        /// Zwraca obiekt rezerwacja o podanym numerze, gdy takiego nie ma wyrzuca wyjątek
        /// </summary>
        /// <param name="numer">Unikalny dla każdej rezerwacji nr</param>
        /// <returns>Zwaca obiekt typu rezerwacja</returns>
        public Rezerwacja ZnajdźRezerwację(int numer)
        {
            return ListaRezerwacji.Find(x => x.Numer == numer);
        }
        /// <summary>
        /// Zwraca obiekt o podanej dacie i czasie, zwraca wyjątek gdy takiego nie ma lub format argumentów jest zły
        /// </summary>
        /// <param name="dzień">Dzień w którrym szukamy rezerwacji</param>
        /// <param name="godzina_początkowa">Godzina początkowa poszukiwanego zakresu</param>
        /// <param name="godzina_końcowa">Godzina kończąca zakres</param>
        /// <returns></returns>
        public List<Rezerwacja> ZnajdźRezerwację(DateTime dzień,string godzina_początkowa,string godzina_końcowa)
        {
            //DateTime newDay;
            
            //if(!DateTime.TryParseExact(dzień, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd/MM/yyyy", "dd-MM-yyyy" }, null, DateTimeStyles.None, out newDay))
            //{
            //    throw new Exception("Znajdź rezerwacje dostał zły format daty");
            //}
            DateTime nowaGodzinaPoczątkowa;
            if (!DateTime.TryParseExact(godzina_początkowa, "H:mm", null, DateTimeStyles.None, out nowaGodzinaPoczątkowa))
            {
                throw new Exception("Znajdź rezerwacje dostał zły format godzinypoczątkowej");
            }
            DateTime nowaGodzinaKońcowa;
            if (!DateTime.TryParseExact(godzina_końcowa, "H:mm", null, DateTimeStyles.None, out nowaGodzinaKońcowa))
            {
                throw new Exception("Znajdź rezerwacje dostał zły format godzinykońcowej");
            }
            return ListaRezerwacji.FindAll(x=> x.Dzień==dzień && x.Godz_pocz >=nowaGodzinaPoczątkowa.TimeOfDay && x.Godzina_końcowa
            <= nowaGodzinaKońcowa.TimeOfDay);
        }
        /// <summary>
        /// Sortuje listę rezerwacji zgodnie z interfejsem IComparable w Rezerwacji
        /// </summary>
        public void Sortuj()
        {
            ListaRezerwacji.Sort();
        }
        /// <summary>
        /// Za kryterium porównywania obiektów przyjmuje nazwę
        /// </summary>
        /// <param name="s">Obiekt typu sala</param>
        /// <returns></returns>
        public int CompareTo(Sala s)
        {
            return Nazwa.CompareTo(s.Nazwa);// sortuje sale po nazwie alfabetycznie
        }
        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Sala));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }
        /// <summary>
        /// Deserializacja z pliku do obiektu zwracanego przez metodę 
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        /// <returns>Zwraca obiekt typu sala</returns>
        public static Sala OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Sala));
            Stream s = File.OpenRead(nazwa + ".xml");
            Sala tmp = (Sala)xs.Deserialize(s);
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
            ZapiszXml("Klonowanie_Sali");
            return OdczytajXml("Klonowanie_Sali");
        }

        //public void ZapisSQL()
        //{
        //    using (var db = new ModelContext())
        //    {
        //        var query = from b in db.Sale
        //                    select b;
        //        foreach (var item in query)
        //        {
        //            if (item.Nazwa == this.Nazwa)
        //            {
        //                Console.WriteLine("Taki rekord już istnieje");
        //                return;

        //            }


        //        }
                
                
        //        //this.numer += 3;
        //        db.Sale.Add(this);
        //        db.SaveChanges();
        //    }
        }
    }

