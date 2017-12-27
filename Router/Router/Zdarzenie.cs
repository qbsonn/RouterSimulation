using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Zdarzenie
    {
        int rodzaj_zdarzenia;
        //-1 przyjscie
        //-2 wyjscie
        int nr_kolejki;


       public  Zdarzenie (int r, int n)

        {
            rodzaj_zdarzenia = r;
            nr_kolejki = n;


        }

        public static Zdarzenie Przyjscie(int n) { return new Zdarzenie(-1, n); }
        public static Zdarzenie WyjscieDoLacza(int n) { return new Zdarzenie(-2, n); }
        public int  ZwrocRodzaj() { return rodzaj_zdarzenia; }
        public int ZwrocNr() { return nr_kolejki; }

    }
}
