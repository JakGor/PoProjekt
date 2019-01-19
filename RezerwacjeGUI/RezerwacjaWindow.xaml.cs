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
    /// Logika interakcji dla RezerwacjaWindow.xaml
    /// </summary>
    /// 
    public partial class RezerwacjaWindow : Window
    {
        /** Deklaracja pól: obiekt klasy Zbior, listy będące xaml'owym odpowiednikiem kolekcji typu  List, a także liczniki: i, j**/
        Zbior zbior = new Zbior();
        //ObservableCollection<Budynek> listaB;
        ObservableCollection<string> listaBn;
        ObservableCollection<Rezerwacja> listaR;
        ObservableCollection<Rezerwacja> listaS;
        private int i,j;
        /// <summary>
        /// Pusty konstruktor 
        /// </summary>
        public RezerwacjaWindow()
        {
            InitializeComponent();
     
        }
        /// <summary>
        /// Konstruktor wymagający podania konkretnego zbioru z okna głównego
        /// </summary>
        /// <param name="zbior"></param>
        public RezerwacjaWindow(Zbior zbior) : this()
        {
            this.zbior = zbior;
            
            int i = 0;
            int j = 0;
            listaBn = new ObservableCollection<string>();
            listaR = new ObservableCollection<Rezerwacja>();
            listaS = new ObservableCollection<Rezerwacja>();
            /// Poniższa pętla uzupełnia kolekcję o nazwy budynków ze zbioru
            foreach (Budynek b in zbior.SpisBudynkow)
            {
                listaBn.Add(b.Nazwa);
            }

            listBox_Budynki.ItemsSource = listaBn;
        }
        /// <summary>
        /// Obsługa wyboru nazwy budynku wchodzącej w skład listBox_Budynki. W listBox_Sale zostaną wyświetlone sale wchodzące w skład tego budynku 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_Budynki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            j = 0;
            i = listBox_Budynki.SelectedIndex;
            listBox_Sale.ItemsSource = zbior.SpisBudynkow[i].ListaSal;
            listBox_Rezerwacje.ItemsSource = null;           
        }      
        /// <summary>
        /// Obsługa wyboru nazwy sali wchodzącej w sład listBox_Sale. W listBox_Rezerwacje zostaną wyświetlone rezerwacje danej sali
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_Sale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = listBox_Budynki.SelectedIndex;
            j = listBox_Sale.SelectedIndex;
            if (j > -1 && i > -1)
            {
                listaR = new ObservableCollection<Rezerwacja>(zbior.SpisBudynkow[i].ListaSal[j].ListaRezerwacji);
                listBox_Rezerwacje.ItemsSource = listaR;
            }
            
        }
        /// <summary>
        /// Obsługa przycisku button_usun, pozwalającego na usunięcie aktualnie wybranej rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = -1;
            zaznaczony = listBox_Rezerwacje.SelectedIndex;
            /// Jeżeli wybrano rezerwację z listy, następuje wyświetlenie okna usuwania rezerwacji
            if (zaznaczony > -1)
            {
                Rezerwacja r = (Rezerwacja)listBox_Rezerwacje.SelectedItem;
                UsunRezerwacjeWindow oknoU = new UsunRezerwacjeWindow(r);
                oknoU.ShowDialog();
                ///Jeżeli proces usuwania przebiegł bez przeszkód, następuje usunięcie rezerwacji z listBoxa, a także ze zboru.  
                if(oknoU.flaga == true)
                {
                    listaR.RemoveAt(zaznaczony);
                    zbior.SpisBudynkow[i].ListaSal[j].UsuńRezerwację(r, oknoU.haslo);
                    
                }
            }
        }  
        /// <summary>
        /// Obsługa przycisku button_dodajR, pozwalającego na dodanie rezerwacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_dodajR_Click(object sender, RoutedEventArgs e)
        {
            /// Tworzona jest pusta rezerwacja, która następnie razem ze zbiorem jest przekazywana do nowootwartego okna NowaRezerwacjaWindow
            Rezerwacja r = new Rezerwacja();
            NowaRezerwacjaWindow oknoNR = new NowaRezerwacjaWindow(r,zbior);
            oknoNR.ShowDialog();
            /// Jeżeli wybrano konkretnąsalęw konkretnym budynku, a także proces dodawania rezerwacji przeszedł pomyślnie, sprawdzana jest dostępność sali w zaproponowanym przez użytkownika czasie
            if(i>-1 && j > -1 && oknoNR.flaga == true)
            {
                ///Jeżeli sala w danym momencie jest zarezerwowana, użytkownik otrzymuje stosowny komunikat
                if (zbior.SpisBudynkow[i].ListaSal[j].SprawdźRezerwację(r) == false)
                {
                    MessageBox.Show("Sala jest wówczas zajęta. Proszę sprawdzić listę rezerwacji");
                    return;
                }
                /// W przeciwnym razie rezerwacja jest dodawana do listy rezerwacji w zbiorze (oraz w listBoxie), ponadto zmiany są zapisywane w pliku XML
                else
                {
                    zbior.SpisBudynkow[i].ListaSal[j].DodajRezerwację(r);
                    
                    listaR.Add(r);
                }             
            }          
        }
        /// <summary>
        /// Obsługa przycisku button_szukaj, pozwalającego na wyszukiwanie rezerwacji w podanym przez użytkownika przedziale czasowym
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_szukaj_Click(object sender, RoutedEventArgs e)
        {
            ///otarte zostaje nowe SzukajWindow, do którego jest przekazana aktualnie wybrana sala
            SzukajWindow OknoS = new SzukajWindow(zbior.SpisBudynkow[i].ListaSal[j]);
            OknoS.ShowDialog();
        }
    }
}
