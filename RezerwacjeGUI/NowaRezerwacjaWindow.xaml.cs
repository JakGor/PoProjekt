using RezerwacjaSal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Shapes;

namespace RezerwacjeGUI
{
    /// <summary>
    /// Logika interakcji dla NowaRezerwacjaWindow.xaml
    /// </summary>
    public partial class NowaRezerwacjaWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Zbior, Rezerwacja, lista będąca xaml'owym odpowiednikiem kolekcji typu  Lista a także flaga logiczna **/
        Zbior zbior;
        Rezerwacja rezerwacja;
        ObservableCollection<Najemca> lista;
        public bool flaga = false;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public NowaRezerwacjaWindow()
        {          
            InitializeComponent();
            // Czy muszę tu deklarować użycie zbioru, listy itp, skoro i tak do okna zostanie podany taki argument? Wolę to zrobić w tym drugim konstruktorze...           
        }
        /// <summary>
        /// Konstruktor pobierający obiekt klasy Rezerwacja oraz Zbior. Następuje w nim przypisanie listy najemców do listBoxa
        /// </summary>
        /// <param name="rezerwacja"></param>
        /// <param name="zbior"></param>
        public NowaRezerwacjaWindow(Rezerwacja rezerwacja,Zbior zbior): this()
        {
            this.rezerwacja = rezerwacja;
            this.zbior = zbior;
            lista = new ObservableCollection<Najemca>(zbior.ListaNajemcow);
            listBox_Najemcy.ItemsSource = lista;
            rezerwacja.Numer = 0; // to może być powód problemów z numerem. tylko jak inaczej to zrobić?         
        }
        /// <summary>
        /// Obsługa przycisku button_Zatwierdz, pozwalającego na dodanie rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            /// Poniższe instrukcje warunkowe sprawdzają poprawność wpisywanych danych - w razie jej braku użytkownik otrzymuje stosowny komunikat
            ///Z listBoxa musi zostać wybrany najemca
            if (listBox_Najemcy.SelectedIndex == -1 )
            {
                MessageBox.Show("Proszę uzupełnić dane");
                return;
            }
            /// Data nie może być wcześniejsza niż obecny dzień
            if (kalendarz.SelectedDate< DateTime.Now.Date)
            {
                MessageBox.Show("Proszę wprowadzić poprawną datę");
                return;
            }
            /// Godzina początkowa musi mieć format wymagany przez TryParseExact
            DateTime nowaGodzinaPocz;
            if (!DateTime.TryParseExact(textBox_godzP.Text, "H:mm", null, DateTimeStyles.None, out nowaGodzinaPocz))
            {
                MessageBox.Show("Proszę wprowadzić poprawną godzinę początkową");
                return;
            }
            this.rezerwacja.Godz_pocz = nowaGodzinaPocz.TimeOfDay;
            /// Godzina końcowa musi mieć format wymagany przez TryParseExact
            DateTime nowaGodzinaKoń;
            if (!DateTime.TryParseExact(textBox_godzK.Text, "H:mm", null, DateTimeStyles.None, out nowaGodzinaKoń))
            {
                MessageBox.Show("Proszę wprowadzić poprawną godzinę końcową");
                return;
            }
            this.rezerwacja.Godzina_końcowa = nowaGodzinaKoń.TimeOfDay;
            /// Godzina początkowa nie może być późniejsza, ani równa końcowej
            if (rezerwacja.Godz_pocz>=rezerwacja.Godzina_końcowa)
            {
                MessageBox.Show("Proszę sprawdzić godziny");
                return;
            }
            /// Jeżeli dane są poprawne następuje ich przypisanie do odpowiednich zmiennych tworzących rezerwację
            this.rezerwacja.Najem = (Najemca)listBox_Najemcy.SelectedItem;
            this.rezerwacja.Dzień = (DateTime)kalendarz.SelectedDate;
            this.rezerwacja.Numer += 1;
            ///Zmiana flagi na true oznacza pomyślne zakończenie procesu dodawania rezerwacji. Następuje zamknięcie okna
            flaga = true;
            this.Close();          
        }
    }
}
