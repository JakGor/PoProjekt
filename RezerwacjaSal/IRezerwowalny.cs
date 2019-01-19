using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezerwacjaSal
{
    /// <summary>
    /// Interfejs zawiera metody: DodajRezerwację,UsuńRezerwacje,ZnajdźRezerwację
    /// </summary>
    interface IRezerwowalny
    {
        void DodajRezerwację(Rezerwacja r);
        void UsuńRezerwację(Rezerwacja r, string pesel);
        List<Rezerwacja> ZnajdźRezerwację(DateTime dzień1,string godzina_początkowa,string godzina_końcowa);
        Rezerwacja ZnajdźRezerwację(int numer);
    }
}
