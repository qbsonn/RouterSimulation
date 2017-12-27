using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Lista uporzadkowana

namespace Router
{ 
    public class Lista<K, D> where K : IComparable
    {   private int pojemnosc;
        private Element<double,D>[] dane;
        private int dlugosc;
        private int priorytet;
        private string nazwa;

        public  Lista()
        {
            Inicjalizuj();
        }

        public Lista(string nazwa, int i, int p)
        {
            Inicjalizuj(nazwa,i,p);
        }

        public Lista( int i)
        {
            Inicjalizuj("", i, 0);
        }

        public void Inicjalizuj()
        {
            pojemnosc = 1000000;
            dane =new Element<double,D>[pojemnosc];
            dlugosc = 0;         
        }

        public void Inicjalizuj(string n, int i, int p)
        {
            pojemnosc = i;
            nazwa = n;
            priorytet = p;
            dane = new Element<double, D >[pojemnosc];
            dlugosc = 0;
        }

        public void Dodaj(double klucz, D d)
        {
            dlugosc++;

            Element<double,D> nowy = new Element<double, D>(klucz, d);
            if (dlugosc==1&&dlugosc<=pojemnosc)
            {
                dane[0] = nowy;                
            }

            if(dlugosc>pojemnosc)
            {
             //  Console.WriteLine("Pakiet przepadl");
                dlugosc--;
            }

            for (int i=0; i< dlugosc; i++)
            {

                if (i < dlugosc - 1)
                {
                    if (dane[i].zwrocKlucz().CompareTo(nowy.zwrocKlucz()) < 0)
                    {
                        Element<double,D> temp;
                        temp = dane[i];
                        dane[i] = nowy;
                        nowy = temp;
                    }

                }
                else dane[dlugosc - 1] = nowy;
            }       

        }

        public Element<double, D> Usun_Najmniejszy()
        {
            Element <double,D> a = dane[dlugosc - 1];

            dlugosc--;
            return a;
        }
/*
        public D zwrocRozmiarNajmniejszego()
        {
            D a;
            if (dlugosc != 0)
                a = dane[dlugosc - 1].zwrocDane();
            else
                a = 99999999999999999;

            return a;
        }
       */
        public Element<double,D> Usun_Najwiejszy()
        {

            Element <double,D> a = dane[0];
            for (int i = 1; i < dlugosc; i++)
            {
                dane[i-1] = dane[i];
            }

            dlugosc--;
            return a;
        }

        public void Wyswietl()
        {
            for (int i=0; i< dlugosc;i++)
            {
                Console.WriteLine(dane[i].zwrocKlucz());
            }
        }

        public int zwrocLiczbeObiektow() { return dlugosc; }
        public int zwrocPojemnosc() { return pojemnosc; }
        public double zwrocKluczNajmniejszego() { return dane[dlugosc - 1].zwrocKlucz(); }
        public string zwrocNazweKolejki()
        {
            return nazwa;
        }
    }

}
