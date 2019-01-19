using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezerwacjaSal
{
    class Program // Opracować wyświetlanie w budynku rezerwacji
    {
        static void Main(string[] args)
        {
            //Stworzyć dokumentację i język angielski
            Najemca n1 = new Najemca("Adrian","Gzyl","98080404212",Płcie.M);
            Console.WriteLine(n1);
            //Console.WriteLine(n1);
            Najemca n2 = new Najemca("Jakub", "Górowski", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1,"2019-01-10","19:00","19:30");
            //Console.WriteLine(r1);
            //Rezerwacja r2 = new Rezerwacja(n1, "2018-12-24", "17:35", "17:45");
            //Rezerwacja r3 = new Rezerwacja(n1, "2018-12-24", "18:00", "18:15");
            //Rezerwacja r4 = new Rezerwacja();
            Sala s1 = new Sala("1.2",Właściwość.aula,50);
            //Console.WriteLine(s1);
            Sala s2 = new Sala("1.3",Właściwość.komputerowa,50);
            s1.DodajRezerwację(r1);

            //s1.DodajRezerwację(r2);
            //s1.DodajRezerwację(r3);
            //Console.WriteLine(s1);
            //s1.UsuńRezerwację(r1, "98080404212");// (s1.ZnajdźRezerwację("2018-12-22", "14:00", "14:30"),"98080404212");
            //Console.WriteLine(s1);
            n1.ZapiszXml("Test_Najemcy_Adrian");
            //Najemca n3 = (Najemca) n1.Clone();
            //Console.WriteLine(n3);
            //Console.WriteLine(Najemca.OdczytajXml("Test_Najemcy_Adrian"));
            //Rezerwacja r1 = new Rezerwacja(n1, "2018-12-28", "8:00", "9:00");
            r1.ZapiszXml("Test_Rezerwacja_1");
            //Console.WriteLine(Rezerwacja.OdczytajXml("Test_Rezerwacja_1"));
            //Sala s1 = Sala.OdczytajXml("Test_Sala_1");
            s1.DodajRezerwację(r1);
            //s1.DodajRezerwację(r2);
            s1.ZapiszXml("Test_Sala_1");
            //Sala s3 = Sala.OdczytajXml("Test_Sala_1");
            Sala s3 = new Sala("102", Właściwość.aula, 150);
            Sala s4 = new Sala("412", Właściwość.ćwiczeniowa, 30);
            Budynek b1 = new Budynek("D-14",true,true,true);
            b1.DodajSale(s1);
            b1.DodajSale(s2);

            Budynek b2 = new Budynek("D-13", true, true, false);
            b2.DodajSale(s3);
            b2.DodajSale(s4);
            Zbior z1 = new Zbior();
            z1.DodajBudynek(b1);
            z1.DodajNajemce(n1);
            z1.DodajNajemce(n2);
            z1.DodajBudynek(b2);
            z1.ZapiszXml("Zbior1");
            Console.WriteLine(z1.WypiszSale(0));         
            

            
            Console.ReadKey();
        }
    }
}
//Test klasy najemca
//1 brak testu pesel
//Najemca n1 = new Najemca("Adrian","Gzyl","",Płcie.M);
//Console.WriteLine(n1);
//Najemca n2 = new Najemca("Adrian", "gzyl", "98080404212", Płcie.M);
//Console.WriteLine(n2);
//Najemca n3 = new Najemca();
//Console.WriteLine(n3);
//Console.WriteLine(n3.Pesel);
