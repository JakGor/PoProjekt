using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RezerwacjaSal;
namespace UnitTestProject1
{
    [TestClass]
    public class TestKlasySala
    {
        [TestMethod]
        public void TestPrzypisywaniaId()
        {
            Sala s1 = new Sala("1", Właściwość.komputerowa, 1);
            Sala s2 = new Sala("2", Właściwość.komputerowa, 1);
            Sala s3 = new Sala("3", Właściwość.komputerowa, 1);
            Sala s4 = new Sala("4", Właściwość.komputerowa, 1);
            Assert.AreEqual(4, s4.Id);
        }
        [TestMethod]
        public void TestKonstruktora()
        {
            Sala s0 = new Sala("1", Właściwość.aula, 10);
            Assert.AreEqual("1", s0.Nazwa);
        }
        [TestMethod]
        public void TestKlonowania()
        {
            Sala s1 = new Sala("1",Właściwość.aula,1);
            Sala s2 =(Sala) s1.Clone();
            Assert.AreEqual(1, s2.Pojemność);
        }
        [TestMethod]
        public void TestSerializacjiXml()
        {
            Sala s1 = new Sala("1",Właściwość.aula,1);
            s1.ZapiszXml("testSerializacji");
            Assert.AreEqual("1",Sala.OdczytajXml("testSerializacji").Nazwa);
        }
        [TestMethod]
        public void TestCompareTo()
        {
            Sala s1 = new Sala("a",Właściwość.aula,1);
            Sala s2 = new Sala("b", Właściwość.aula, 1);
            Assert.AreEqual("a".CompareTo("b"), s1.CompareTo(s2));
        }
        [TestMethod]
        public void TestLiczbyRezerwacji()
        {
            Sala s1 = new Sala("a", Właściwość.aula, 1);
            Najemca n1 = new Najemca("Jakub", "Górowski", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1, "29-01-2019", "09:00", "10:00");
            s1.DodajRezerwację(r1);
            Assert.AreEqual(1, s1.LiczbaRezerwacji);
        }
        [TestMethod]
        public void TestDodawaniaRezerwacji()
        {
            Sala s1 = new Sala("a",Właściwość.aula,1);
            Najemca n1 = new Najemca("Jakub", "Górowski", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1,"29-01-2019","09:00","10:00");
            s1.DodajRezerwację(r1);
            Assert.AreEqual("JAKUB", s1.ListaRezerwacji[0].Najem.Imię);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestKontroliWejściaZnajdźRezerwację()
        {
            Sala s1 = new Sala();
            s1.ZnajdźRezerwację(DateTime.Now, "abc", "abc");
        }
        [TestMethod]
        public void TestZnajdźRezerwację()
        {
            Sala s1 = new Sala();
            Najemca n1 = new Najemca("Hubert", "Jakiś", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1, "29-01-2019", "11:00", "12:00");
            s1.DodajRezerwację(r1);
            Assert.AreEqual("HUBERT", s1.ZnajdźRezerwację(DateTime.Parse("2019-01-29"), "11:00", "12:00")[0].Najem.Imię);
        }
        [TestMethod]
        public void TestSortowaniaListyRezerwacji()
        {
            Sala s1 = new Sala("a", Właściwość.aula, 1);
            Najemca n1 = new Najemca("Jakub", "Górowski", "98052408891", Płcie.M);
            Najemca n2 = new Najemca("Kuba", "Górowski", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1, "29-01-2019", "11:00", "12:00");
            Rezerwacja r2 = new Rezerwacja(n2, "29-01-2019", "09:00", "10:00");
            s1.DodajRezerwację(r1);
            s1.DodajRezerwację(r2);
            Assert.AreEqual("KUBA",s1.ListaRezerwacji[0].Najem.Imię);
        }
        [TestMethod]
        public void TestUsuwaniaRezerwacji()
        {
            Sala s1 = new Sala("a", Właściwość.aula, 1);
            Najemca n1 = new Najemca("Jakub", "Górowski", "98052408891", Płcie.M);
            Rezerwacja r1 = new Rezerwacja(n1, "29-01-2019", "11:00", "12:00");
            s1.DodajRezerwację(r1);
            s1.UsuńRezerwację(r1, "98052408891");
            Assert.AreEqual(0, s1.LiczbaRezerwacji);
        }
    }

    [TestClass]
    public class TestKlasyNajemca
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Najemca n1 = new Najemca("Adrian","Gzyl","98080404212",Płcie.M);
            Assert.AreEqual("ADRIAN",n1.Imię);
        }
        [TestMethod]
        [ExpectedException(typeof(KonstruktorException))]
        public void TestSprawdzaniaPesel()
        {
            Najemca n1 = new Najemca("Adrian", "Gzyl", "98080404999", Płcie.M);
        }
        [TestMethod]
        [ExpectedException(typeof(KonstruktorException))]
        public void TestSprawdzaniaImienia()
        {
            Najemca n1 = new Najemca("A", "Gzyl", "98080404212", Płcie.M);
        }
        [TestMethod]
        [ExpectedException(typeof(KonstruktorException))]
        public void TestSprawdzaniaNazwiska()
        {
            Najemca n1 = new Najemca("Adrian", "12345", "98080404212", Płcie.M);
        }
        [TestMethod]
        public void TestKlonowania()
        {
            Najemca n1 = new Najemca("Adrian", "Gzyl", "98080404212", Płcie.M);
            Najemca n2 =(Najemca) n1.Clone();
            Assert.AreEqual("ADRIAN", n2.Imię);
        }
    }
}
