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
    /// Logika interakcji dla EdycjaBudynkuWindow.xaml
    /// </summary>
    public partial class EdycjaBudynkuWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Budynek, a także flaga, sprawdzająca czy proces tworzenia/edycji budynku został w pełni przeprowadzony **/
        public bool flaga = false;
        Budynek budynek;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public EdycjaBudynkuWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor wymagajacy podania obiektu klasy Budynek
        /// </summary>
        /// <param name="budynek"></param>
        public EdycjaBudynkuWindow(Budynek budynek) : this()
        {
            this.budynek = budynek;
            ///Jeżeli budynek posiada już swoją nazwę, zostaje ona wyświetlona w TextBoxie. W przeciwnym razie jest on pusty.
            textBox_nazwaBudynku.Text = budynek.Nazwa;
            ///Poniższe instrukcje pokazują pozostałe właściwości budynku za pomocą pól ComboBoxa
            if(budynek.Winda==false)
            {
                comboBox_winda.Text = "Nie";
            }
            else
            {
                comboBox_winda.Text = "Tak";
            }
            if (budynek.Wifi == false)
            {
                comboBox_wifi.Text = "Nie";
            }
            else { comboBox_wifi.Text = "Tak"; }
            if (budynek.Gastronomia == false)
            {
                comboBox_gastronomia.Text = "Nie";
            }
            else { comboBox_gastronomia.Text = "Tak"; }

        }
        /// <summary>
        /// Obsługa przycisku button_zatwierdz, którego wciśnięcie powoduje zakończenie procesu tworzenia budynku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            /// Jeżeli dane są niepełne, wówczas wyświetlony zostaje stosowny komunikat
            if (textBox_nazwaBudynku.Text == "")
            {
                MessageBox.Show("Proszę podać nazwę budynku");
                return;
            }
            ///Następuje przypisanie do właściwości tworzonego budynku wartości ustawinonych przez użytkownika
            this.budynek.Nazwa = textBox_nazwaBudynku.Text;
            if (comboBox_winda.Text == "Tak") { this.budynek.Winda = true; }
            else { this.budynek.Winda = false; }
            if (comboBox_wifi.Text == "Tak") { this.budynek.Wifi = true; }
            else { this.budynek.Wifi = false; }
            if (comboBox_gastronomia.Text == "Tak") { this.budynek.Gastronomia = true; }
            else { this.budynek.Gastronomia = false; }
            /// Zakończony proces powoduje zmianę wartości logiczej flagi  oraz zamknięcie okna
            flaga = true;
            this.Close();
        }
    }
}
