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
    /// Logika interakcji dla BudynekWindow.xaml
    /// </summary>
    public partial class BudynekWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Zbior, lista będąca xaml'owym odpowiednikiem kolekcji typu  Lista, a także licznik i **/
        Zbior zbior = new Zbior();
        
        ObservableCollection<Sala> lista;
        private int i;

        /// <summary>
        /// Pusty konstruktor 
        /// </summary>
        /// <param name="zbior"></param>
        public BudynekWindow() 
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Główny konstruktor, w którym zostają przypisane wartości dla wyświatlanych kontrolek WPF (Labeli, TextBoxów,ListBoxa)
        /// </summary>
        public BudynekWindow( Zbior zbior) : this()
        {
            this.zbior = zbior;
                  
            int i = 0;
            lista = new ObservableCollection<Sala>(zbior.SpisBudynkow[i].ListaSal); /// Do listy zostaje przypisana kolekcja "ListaSal" (typ Lista)
            listBox_sale.ItemsSource = lista; /// obiekt ListBox będzie pobierał dane z listy        
            label_nazwaBudynku.Content = zbior.SpisBudynkow[i].Nazwa;
            
            
            ///Poniższe konstrukcje warunkowe służą do opisania budynku za pomocą słów Tak lub Nie
            
            if (zbior.SpisBudynkow[i].Winda == true)
            {
                labelWinda.Content = "Tak";
            }
            else
            {
                labelWinda.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Wifi == true)
            {
                labelWifi.Content = "Tak";
            }
            else
            {
                labelWifi.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Gastronomia == true)
            {
                labelGastronomia.Content = "Tak";
            }
            else
            {
                labelGastronomia.Content = "Nie";
            }         
        }
        
        
        /// <summary>
        /// Obsługa przycisku "Następny" pozwalającego na przeglądanie budynków
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nastepny_Click(object sender, RoutedEventArgs e)
        {
            ///Poniższa instrukcja warunkowa odpowiada za zapętlenie zbioru budynków, tak aby niemożliwe było wyjście poza rozmiar kolekcji
            if(i ==zbior.SpisBudynkow.Count()-1)
            {
                i = 0;
            }
            else
            {
                i++;
            }

            ///Do kontrolek WPF zostają przypisane cechy następnego budynku
            lista = new ObservableCollection<Sala>(zbior.SpisBudynkow[i].ListaSal); /// Do listy zostaje przypisana kolekcja "ListaSal" (typ Lista)
            listBox_sale.ItemsSource = lista; /// obiekt ListBox będzie pobierał dane z listy        
            label_nazwaBudynku.Content = zbior.SpisBudynkow[i].Nazwa;


            ///Poniższe konstrukcje warunkowe służą do opisania budynku za pomocą słów Tak lub Nie

            if (zbior.SpisBudynkow[i].Winda == true)
            {
                labelWinda.Content = "Tak";
            }
            else
            {
                labelWinda.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Wifi == true)
            {
                labelWifi.Content = "Tak";
            }
            else
            {
                labelWifi.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Gastronomia == true)
            {
                labelGastronomia.Content = "Tak";
            }
            else
            {
                labelGastronomia.Content = "Nie";
            }
        }
        /// <summary>
        /// Obsługa przycisku button_dodajBudynek, pozwalającego na dodawanie nowego budynku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_dodajBudynek_Click(object sender, RoutedEventArgs e)
        {
            ///Zostanie wyświetlone okno klasy EdycjaBudynkuWindow, w którym zostanie stworzony nowy obiekt b klasy Budynek
            Budynek b = new Budynek();
            EdycjaBudynkuWindow okno = new EdycjaBudynkuWindow(b);
            okno.ShowDialog();  
            
            /// Instrukcja warunkowa odpowiada sprawdzenie czy proces tworzenia został w pełni ukończony (czy nie naciśnięto przycisku Anuluj lub zamknięto okno krzyżykiem)
            if (okno.flaga == true)
            {
                /// Nestępuje dodanie budynku do kolekcji SpisBudynkow       
                zbior.DodajBudynek(b);
                
            }
            
        }
        /// <summary>
        /// Obsługa przycisku button_edytuj, pozwalającego na edycję aktualnie wyświetlanego budynku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_edytuj_Click(object sender, RoutedEventArgs e)
        {
            ///Zostanie wyświetlone okno klasy EdycjaBudynkuWindow, w którym zostanie otwarty obiekt klasy Budynek, będący aktualnie wyświetlanym budynkiem
            EdycjaBudynkuWindow okno = new EdycjaBudynkuWindow(zbior.SpisBudynkow[i]);
            okno.ShowDialog();

            /// Poniższe instrukcje odpowiadają za ponowne wyświetlenie budynku, któego edycja została zakończona
            lista = new ObservableCollection<Sala>(zbior.SpisBudynkow[i].ListaSal); /// Do listy zostaje przypisana kolekcja "ListaSal" (typ Lista)
            listBox_sale.ItemsSource = lista; /// obiekt ListBox będzie pobierał dane z listy        
            label_nazwaBudynku.Content = zbior.SpisBudynkow[i].Nazwa;


            ///Poniższe konstrukcje warunkowe służą do opisania budynku za pomocą słów Tak lub Nie

            if (zbior.SpisBudynkow[i].Winda == true)
            {
                labelWinda.Content = "Tak";
            }
            else
            {
                labelWinda.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Wifi == true)
            {
                labelWifi.Content = "Tak";
            }
            else
            {
                labelWifi.Content = "Nie";
            }

            if (zbior.SpisBudynkow[i].Gastronomia == true)
            {
                labelGastronomia.Content = "Tak";
            }
            else
            {
                labelGastronomia.Content = "Nie";
            }
        }
        /// <summary>
        /// Obsługa przycisku button_dodajSale, pozwalajacego na dodanie sali do aktualnie wyświetlanego budynku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_dodajSale_Click(object sender, RoutedEventArgs e)
        {
            ///Nastąpi otwarcie nowego okna klasy SalaWindow, zawierającego nowo stworzony obiekt klasy Sala
            Sala sala = new Sala();
            SalaWindow oknoS = new SalaWindow(sala);
            oknoS.ShowDialog();
            /// Jeżeli proces tworzenia sali zostanie pomyślnie zakończony, zmiany zostaną zapisane w pliku XML, a także nowy obiekt zostanie dodany do listy sal
            if (oknoS.flaga == true)
            {
                lista.Add(sala);            
                zbior.SpisBudynkow[i].DodajSale(sala);
                          
            }
        }
        /// <summary>
        /// Obsługa przycisku button_usunSale, który pozwala na usunięcie zaznaczonej sali
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_usunSale_Click(object sender, RoutedEventArgs e)
        {
            ///"zaznaczony" to wartość aktualnie wybranego indeksu z ListBoxa (jeżeli nie wbrano nic, posada wartość -1)
            int zaznaczony = -1;
            zaznaczony = listBox_sale.SelectedIndex;
            /// Jeżeli wybrano salę z listy, następuje jej usunięcie z wyświetlanego ListBoxa (lista), a także z listy sal znajdującej sięw danym budynku. Zmiany zostają zapsiane w pliku XML
            if (zaznaczony > -1)
            {
                lista.RemoveAt(zaznaczony);
                zbior.SpisBudynkow[i].ListaSal.RemoveAt(zaznaczony);
               
            }
        }
    }
}
