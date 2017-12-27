using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Strumien     //////////// public ustawiłem tymczasowo do testów wczytywania
    {
        private string nazwa;
        private string rozkladRozmiaru; // nazwa rozkładu z którego losowana jest wartość trwania połączenia
        private string rozkladOdstepu; //nazwa rozkładu z którego losowana jest wartość odstepu miedzy polaczeniami
        private int nrRozkladRozmiaru;
        private int nrRozkladOdstepu;
        int priorytet;
        string kolejka;

        public Strumien(string m_nazwa,  string m_rozkladRozmiaru,
            string m_rozkladOdstepu, int p, string k)
        {//nr- nr rozkladu ostepu, nr-nr rozkladu rozmiaru
            nazwa = m_nazwa;
            p = priorytet;
            rozkladRozmiaru = m_rozkladRozmiaru;
            rozkladOdstepu = m_rozkladOdstepu;
            kolejka = k;
        }
        public string ZwrocNazwe() { return nazwa; }
        public int ZwrocNrRozkladuRoziaru() { return nrRozkladRozmiaru; }
        public int ZwrocNrRozkladuOdstepu() { return nrRozkladOdstepu; }
     
        public string ZwrocNazweRozkladuRozmiaru() { return rozkladRozmiaru; }
        public string ZwrocNazweRozkladuOdstepu() { return rozkladOdstepu; }

        public void UstawNrRozkladuRozmiaru(int m_nrRozkladRozmiaru) { nrRozkladRozmiaru = m_nrRozkladRozmiaru; }
        public void UstawNrRozkladuOdstepu(int m_nrRozkladOdstepu) { nrRozkladOdstepu = m_nrRozkladOdstepu; }
    }

}

    

