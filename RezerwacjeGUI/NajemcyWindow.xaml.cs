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
    /// Logika interakcji dla NajemcyWindow.xaml
    /// </summary>
    /// 

    public partial class NajemcyWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Zbior, lista będąca xaml'owym odpowiednikiem kolekcji typu  Lista **/
        Zbior zbior = new Zbior();
        private ObservableCollection<Najemca> lista;
        /// <summary>
        /// Pusty konstruktor, w którym zostają przypisane wartości dla wyświatlanych kontrolek WPF (Labeli, TextBoxów,ListBoxa)
        /// </summary>
        public NajemcyWindow()
        {
            InitializeComponent(); 
        }
        /// <summary>
        /// Główny Konstruktor wymagający podania zbioru z okna głównego
        /// </summary>
        /// <param name="zbior"></param>
        public NajemcyWindow(Zbior zbior):this()
        {
            this.zbior = zbior;
            lista = new ObservableCollection<Najemca>(zbior.ListaNajemcow); /// Do listy zostaje przypisana kolekcja "ListaNajemcow" (typ Lista)
            listBox_Najemcy.ItemsSource = lista; /// obiekt ListBox będzie pobierał dane z listy
        }
        /// <summary>
        /// Obsługa przycisku button_dodaj, pozwalającego na dodanie najemcy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            ///Otwarte zostaje nowe okno klasy NowyNajemcaWindow, zawierające nowy, pusty obiekt klasy Najemca
            Najemca n = new Najemca();
            NowyNajemcaWindow okno = new NowyNajemcaWindow(n);
            okno.ShowDialog();
                         
            /// Instrukcja warunkowa odpowiada sprawdzenie czy proces tworzenia został w pełni ukończony (czy nie naciśnięto przycisku Anuluj lub zamknięto okno krzyżykiem)
            if (okno.flaga == true)
            {
                zbior.DodajNajemce(n); ///Nestępuje dodanie najemcy do kolekcji ListaNajemcow, uzupełnienie danych w ListBoxie (lista),
                lista.Add(n);

            }
           
        }
        /// <summary>
        /// Obsługa przycisku button_usun, pozwalającego na usunięcie zaznaczonego najemcy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            ///"zaznaczony" to wartość aktualnie wybranego indeksu z ListBoxa (jeżeli nie wbrano nic, posada wartość -1)
            int zaznaczony = -1;
            zaznaczony = listBox_Najemcy.SelectedIndex;
            /// Jeżeli wybrano najemcę z listy, następuje jego usunięcie z wyświetlanego ListBoxa (lista), a także z listy najemców w obiekcie zbior. 
            if (zaznaczony > -1)
            {
                lista.RemoveAt(zaznaczony);
                zbior.ListaNajemcow.RemoveAt(zaznaczony);
                
            }
            
        }
    }
}
