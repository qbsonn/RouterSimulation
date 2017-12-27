using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Lista uporzadkowana

namespace ConsoleApplication1
{
  
    public class Lista<K, D> where K : IComparable
    {       private int pojemnosc;
        private Element<K,D>[] dane;
        private int dlugosc;

        public  Lista()
        {
            Inicjalizuj();
        }

        public void Inicjalizuj()
        {
            pojemnosc = 1000000;
            dane =new Element<K,D>[pojemnosc];
            dlugosc = 0;
           
        }

        public void Dodaj(K klucz, D d)
        {
            dlugosc++;

            Element<K,D> nowy = new Element<K, D>(klucz, d);
            if (dlugosc==1)
            {
                dane[0] = nowy;
                
            }

            if(dlugosc==pojemnosc)
            {
                Console.WriteLine("Pakiet przepadl");
                dlugosc--;
            }

            for (int i=0; i< dlugosc; i++)
            {

                if (i < dlugosc - 1)

                {
                    if (dane[i].zwrocKlucz().CompareTo(nowy.zwrocKlucz()) < 0)
                    {

                        Element<K,D> temp;
                        temp = dane[i];
                        dane[i] = nowy;
                        nowy = temp;
                    }


                }
                else dane[dlugosc - 1] = nowy;
            }

        

        }

       public Element<K, D> Usun_Najmniejszy()
        {
            Element <K, D> a = dane[dlugosc - 1];


            dlugosc--;
            return a;
        }


        public Element<K, D> Usun_Najwiejszy()
        {

            Element <K, D> a = dane[0];
            for (int i = 1; i < dlugosc; i++)
            {  dane[i-1] = dane[i];

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
    }
}
