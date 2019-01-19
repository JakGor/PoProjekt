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
    /// Logika interakcji dla NowyNajemcaWindow.xaml
    /// </summary>
    public partial class NowyNajemcaWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Najemca, a także flaga, sprawdzająca czy proces tworzenia najemcy został w pełni przeprowadzony **/
        public bool flaga = false;
        Najemca najemca;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public NowyNajemcaWindow()
        {
            
            InitializeComponent();

        }
        /// <summary>
        /// Konstruktor wymagający podania obiektu klasy Najemca
        /// </summary>
        /// <param name="najemca"></param>
        public NowyNajemcaWindow(Najemca najemca):this()
        {
            this.najemca = najemca;
            ///Jeżeli wczytany do okna najemca nie jest nowy (ma już PESEL), pola zostaną uzupełnione jego właściwościami.
            if (najemca.Pesel != "00000000000")
            {
                textBox_pesel.Text = najemca.Pesel;
                textBox_imie.Text = najemca.Imię;
                textBox_nazwisko.Text = najemca.Nazwisko;
               
                if ((najemca.Płeć) == Płcie.K)
                    comboBox_plec.Text = "kobieta";
                else
                    comboBox_plec.Text = "mężczyzna";
            }
        }
        /// <summary>
        /// Obsługa przycisku button_zatwierdz, którego wciśnięcie powoduje zakończenie procesu tworzenia najemcy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            /// Jeżeli dane są niepełne, wówczas wyświetlony zostaje stosowny komunikat
            if (textBox_pesel.Text == "" || textBox_imie.Text == "" || textBox_nazwisko.Text == ""|| textBox_pesel.Text == "00000000000")
            {
                MessageBox.Show("Proszę uzupełnić dane");
                return;
            }
            ///Następnie sprawdzona zostaje poprawność numeru PESEL. Jeżeli jest on nieprawidłowy, wówczas wyświetlony zostaje stosowny komunikat
            if (najemca.sprawdzPESEL(textBox_pesel.Text)==false)
            {
                MessageBox.Show("Nieprawidłowy PESEL", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                this.najemca.Pesel = textBox_pesel.Text; ///Jeżeli PESEL jest poprawny, następuje przypisanie go do właściwości nowo utworzonego najemcy
            }
            ///Następuje przypisanie pozostałych danych podanych przez użytkownika do właściwości nowo utworzonego najemcy
            this.najemca.Imię = textBox_imie.Text;
            this.najemca.Nazwisko = textBox_nazwisko.Text;
            
            if (comboBox_plec.Text == "kobieta")
                this.najemca.Płeć= Płcie.K;
            else
                this.najemca.Płeć = Płcie.M;
            /// Zakończony proces powoduje zmianę wartości logiczej flagi  oraz zamknięcie okna
            flaga = true;
            this.Close();
        }
        /// <summary>
        /// Przycisk button_anuluj przerywa tworzenie najemcy i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_anuluj_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }
    }
}
