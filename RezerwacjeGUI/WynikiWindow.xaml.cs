using RezerwacjaSal;
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
using System.Windows.Shapes;

namespace RezerwacjeGUI
{
    /// <summary>
    /// Logika interakcji dla WynikiWindow.xaml
    /// </summary>
    public partial class WynikiWindow : Window
    {
        /** Deklaracja pól: lista będąca xaml'owym odpowiednikiem kolekcji typu  Lista **/
        List<Rezerwacja> lista;
        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public WynikiWindow()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// Konstruktor pobierający Listę generyczną z Rezerwacjami, następuje w nim przypisanie do listBoxa danych z otrzymanej listy
        /// </summary>
        /// <param name="lista"></param>
        public WynikiWindow (List<Rezerwacja> lista):this()
        {
            this.lista = lista;
            
            listBox_wyniki.ItemsSource = lista;
        }
        /// <summary>
        /// Obsługa przycisku button_powrot, umożliwiającego zamknięcie okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
