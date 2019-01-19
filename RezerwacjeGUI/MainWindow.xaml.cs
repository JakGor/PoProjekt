using RezerwacjaSal;
using RezerwacjeGUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RezerwacjeGUI
{
    /// <summary>
    /// Logika intereakcji dla MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /** Deklaracja pól: obiekt klasy Zbior, listatmp będąca xaml'owym odpowiednikiem kolekcji typu  Lista, która weźmie udział w kopiowaniu aktualnych rezerwacji **/
        Zbior zbior = new Zbior();
        ObservableCollection<Rezerwacja> listatemp;
        List<int> listanumerow;
        /// <summary>
        /// Pusty konstruktor w którym program wczyta dane z pliku XML
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            listatemp = new ObservableCollection<Rezerwacja>();
            listanumerow = new List<int>();
            zbior = (Zbior)Zbior.OdczytajXml("Zbior1"); /// Do obiektu zbior zostają wczytane dane z pliku XML
            int i = 0;
            ///Poniższa potrójna pętla sprawdza, czy w każdym budynku, w każdej sali, znajdują się aktualne rezerwacje. Jeżeli tak, to są one kopiowane na listę tymczasową
            foreach (Budynek b in zbior.SpisBudynkow)
            {
                foreach (Sala s in b.ListaSal)
                {
                    foreach (Rezerwacja r in s.ListaRezerwacji)
                    {
                        if (r.Dzień > DateTime.Now.Date)
                        {
                            listatemp.Add(r);
                            listanumerow.Add(r.Numer);

                        }
                        if ( r.Dzień == DateTime.Now.Date && r.Godzina_końcowa > DateTime.Now.TimeOfDay)
                        {
                            listatemp.Add(r);
                            listanumerow.Add(r.Numer);
                        }
                    }
                    s.ListaRezerwacji.Clear();
                    s.LiczbaRezerwacji = 0;
                    /// Wykorzystaje zostana metoda "ręcznego" ustawiania numeru dla przepisywanych rezerwacji
                    foreach (Rezerwacja r in listatemp)
                    {
                        s.DodajRezerwację(r,listanumerow[i]);
                        i++;
                    } 
                    /// następuje przekopiowanie elementów z listy tymczasowej na listę rezerwacji danej sali
                    s.ListaRezerwacji.Sort();
                    listatemp.Clear(); /// Czyszczenie listy tymczasowej, tak aby była ona gotowa do wykorzystania w kolejnej sali
                    listanumerow.Clear();
                    i = 0;
                }
            }
            //zbior.ZapiszXml("Zbior1"); /// Zmiany zostają zapisane w pliku XML



        }
        /// <summary>
        /// Deklaracja tego, co wydarzy się po kliknięciu przycisku button_Budynki 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Budynki_Click(object sender, RoutedEventArgs e)
        {
            BudynekWindow oknoB = new BudynekWindow( zbior); 
            oknoB.ShowDialog();
            /// Nastąpi otwarcie nowego okna klasy BudynekWindow         
        }
        /// <summary>
        /// Deklaracja tego, co wydarzy się po kliknięciu przycisku button_Najemcy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Najemcy_Click(object sender, RoutedEventArgs e)
        {
            NajemcyWindow oknoN = new NajemcyWindow(zbior);
            oknoN.ShowDialog();
            /// Nastąpi otwarcie nowego okna klasy NajemcyWindow
        }

        /// <summary>
        /// Deklaracja tego, co wydarzy się po kliknięciu przycisku button_Rezerwacje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Rezerwacje_Click (object sender, RoutedEventArgs e)
        {
            RezerwacjaWindow oknoR = new RezerwacjaWindow(zbior);
            oknoR.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //zbior.ZapiszXml("Zbior1");
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult rezultat = MessageBox.Show("Czy chcesz zapisać zmiany?", "Zapis jest niepełny", MessageBoxButton.YesNo);

            
            if (rezultat == MessageBoxResult.No)
            {
                
            }
            else if (rezultat == MessageBoxResult.Yes)
            {
                zbior.ZapiszXml("Zbior1");
                
            }
            
        }
    }
}
