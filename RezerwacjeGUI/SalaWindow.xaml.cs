using RezerwacjaSal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RezerwacjeGUI
{
    /// <summary>
    /// Logika interakcji dla SalaWindow.xaml
    /// </summary>
    public partial class SalaWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Sala, a także flaga, sprawdzająca czy proces tworzenia sali został w pełni przeprowadzony **/
        Sala sala;
        public bool flaga = false;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public SalaWindow()
        {
            InitializeComponent();
            

        }
        /// <summary>
        /// Konstruktor wymagający podania obiektu klasy Sala
        /// </summary>
        /// <param name="sala"></param>
        public SalaWindow(Sala sala): this()
        {
            this.sala = sala;
            ///Jeżeli wczytana do okna sala nie jest nowa (ma już nazwę), pola zostaną uzupełnione jej właściwościami.
            if (sala.Nazwa != "")
            {
                textBox_nazwaSali.Text = sala.Nazwa;
                slider_pojemnosc.Value = sala.Pojemność;
                if(sala.Typ== Właściwość.aula)
                {
                    comboBox_typSali.Text = "Aula";
                }
                else if (sala.Typ== Właściwość.komputerowa)
                {
                    comboBox_typSali.Text = "Komputerowa";
                }
                else
                {
                    comboBox_typSali.Text = "Cwiczeniowa";
                }
            }
            

        }
        /// <summary>
        /// Obsługa przycisku button_zatwierdz, którego wciśnięcie powoduje zakończenie procesu tworzenia sali
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            /// Jeżeli dane są niepełne, wówczas wyświetlony zostaje stosowny komunikat
            if (textBox_nazwaSali.Text==""|| (int)slider_pojemnosc.Value==0) 
            {
                MessageBox.Show("Proszę uzupełnić dane");
                return;
            }
            ///Następuje przypisanie do właściwości tworzonej sali wartości ustawinonych przez użytkownika
            this.sala.Nazwa = textBox_nazwaSali.Text;
            this.sala.Pojemność = (int)slider_pojemnosc.Value; /// Domyślnie wartość przechowywana przez Slider to double, więc wymagane jest rzutowanie na typ int
            this.sala.Id = ++Sala.Kolejność_sal; // Nie mogę poradzić sobe z ustawieniem dla dodawanej sali odpowiedniego ID. Czy ktoś ma jakiś pomysł? Ewentualnie, może uda się bez niego obejść...

            if(comboBox_typSali.Text=="Aula") { this.sala.Typ = Właściwość.aula; }
            else if (comboBox_typSali.Text == "Komputerowa") { this.sala.Typ = Właściwość.komputerowa; }
            else { this.sala.Typ = Właściwość.ćwiczeniowa; }
            /// Zakończony proces powoduje zmianę wartości logiczej flagi  oraz zamknięcie okna
            flaga = true;
            this.Close();
        }

        
    }
}
