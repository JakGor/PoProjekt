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
    /// Logika interakcji dla SzukajWindow.xaml
    /// </summary>
    public partial class SzukajWindow : Window
    {   /** Deklaracja pól: obiekt klasy Sala, zmienne typu string, a takżelista będąca xaml'owym odpowiednikiem kolekcji typu  Lista **/
        Sala sala;
        public string godzinaOD, godzinaDO;
        List<Rezerwacja> listaWyniki;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public SzukajWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor pobierający obiekt klasy Sala, w którym następuje utworzenie pustej listy z wynikami wyszukiwania rezerwacji
        /// </summary>
        /// <param name="sala"></param>
        public SzukajWindow (Sala sala) : this()
        {
            this.sala = sala;
            listaWyniki = new List<Rezerwacja>();
        }
        /// <summary>
        /// Obsługa przycisku button_powrot, który powoduje zamknięcie okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Obsługa przycisku button_zatwierdz, powodującego wyszukanie rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            /// Poniższe instrukcje warunkowe sprawdzają poprawność wpisywanych danych - w razie jej braku użytkownik otrzymuje stosowny komunikat
            /// Data nie może być wcześniejsza niż obecny dzień
            if(kalendarz.SelectedDate < DateTime.Now.Date)
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
            /// Godzina końcowa musi mieć format wymagany przez TryParseExact
            DateTime nowaGodzinaKoń;
            if (!DateTime.TryParseExact(textBox_godzK.Text, "H:mm", null, DateTimeStyles.None, out nowaGodzinaKoń))
            {
                MessageBox.Show("Proszę wprowadzić poprawną godzinę końcową");
                return;
            } 
            /// Godzina początkowa nie może być późniejsza, ani równa końcowej
            if (nowaGodzinaPocz>= nowaGodzinaKoń)
            {
                MessageBox.Show("Proszę sprawdzić godziny");
                return;
            }
            /// Jeżeli dane są poprawne następuje ich przypisanie do odpowiednich zmiennych, będących argumentami funkcji ZnajdźRezerwację
            godzinaOD = textBox_godzP.Text;
            godzinaDO = textBox_godzK.Text;
            DateTime kalendarzdata = (DateTime)kalendarz.SelectedDate;
            DateTime Data = kalendarzdata.Date;
            ///do listy zostaje przypisana lista zwracana przez funkcję ZnajdźRezerwację
            listaWyniki = sala.ZnajdźRezerwację(Data, godzinaOD, godzinaDO);
            ///Gdy program nie znajdzie rezerwacji, użytkownik otrzymuje stosowny komunikat
            if (listaWyniki.Count == 0)
            {
                MessageBox.Show("Brak rezerwacji");
                return;
            }
            /// W przeciwnym wypadku zostaje otwarte okno z listBoxem zawierającym wszystkie znalezione rezerwacje
            else
            {
                WynikiWindow oknoW = new WynikiWindow(listaWyniki);
                oknoW.ShowDialog();
            }
        }
    }
}
