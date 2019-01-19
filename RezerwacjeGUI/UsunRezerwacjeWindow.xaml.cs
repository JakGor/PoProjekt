using RezerwacjaSal;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla UsunRezerwacjeWindow.xaml
    /// </summary>
    public partial class UsunRezerwacjeWindow : Window
    {
        /** Deklaracja pól: obiekt klasy rezerwacja, flaga logiczna, zmienna typu string **/
        Rezerwacja rezerwacja;
        public bool flaga = false;
        public string haslo;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public UsunRezerwacjeWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor pobierający obiekt klasy Rezerwacja
        /// </summary>
        /// <param name="rezerwacja"></param>
        public UsunRezerwacjeWindow(Rezerwacja rezerwacja) :this()
        {
            this.rezerwacja = rezerwacja;
        }
        /// <summary>
        /// Obsługa przycisku button_zatwierdz, inicjującego kończenie procesu usuwania rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            ///Poniższa instrukcja warunkowa sprawdza czy wpsiany przez użytkownika PESEL równy jest PESELowi osoby, która dokonała rezerwacji. Jeżeli nie, użytkownik otrzymuje stosowny komunikat
            if(passwordBox.Password != rezerwacja.Najem.Pesel)
            {
                MessageBox.Show("Nieprawidłe hasło", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ///W przeciwnym razie do zmiennej string zostaje przypisane wpisany PESEL,  flaga zmienia wartość na true, co oznacza zakończenie procesu usuwania, a okno jest zamykane 
            else
            {
                haslo = passwordBox.Password;
                flaga = true;
                this.Close();
            }         
        }
        /// <summary>
        /// Obsługa przycisku button_anuluj pozwalającego na wyjście z okna i anulowanie procesu usuwania rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
