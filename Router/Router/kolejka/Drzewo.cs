using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace ConsoleApplication1
{
    public class Drzewo<K, D>  where K : IComparable
    {
        public Drzewo() // konstruktor
        {
            
            arr = null;
            size = 0;
            real_size = 0;
            dane = new Element <K, D>[100];
        }
        public K getMin()
        {
            if (size < 1) throw new ArgumentException("Drzewo jest puste!");
            return dane[arr[0]].zwrocKlucz();
        }
        public int getSize()
        {
            return real_size;
        }
        
        void PushUp(int start_real_id)
        {
            //Console.WriteLine ("PushUp({0})", start_real_id);
            if (start_real_id != 1)
            {
                if (start_real_id == size)
                {//jeśli to ostatni element
                    if (start_real_id % 2 == 0)
                    {//jeśli to id parzyste to znaczy że nie ma brata
                     //Console.WriteLine ("31 {0} {1}", getId (getParentId (start_real_id)), getId (start_real_id));
                     //Console.WriteLine ("{0} {1}", tablica [getId (getParentId (start_real_id))], tablica [getId (start_real_id)]);

                        arr[getId(getParentId(start_real_id))] = arr[getId(start_real_id)];
                        //Console.WriteLine ("35");
                        size--;
                        Array.Resize(ref arr, size);
                        //Console.WriteLine ("35");
                        PushUp(getParentId(start_real_id));
                        //Console.WriteLine ("37");
                    }
                    else
                    {
                        //throw new ArgumentException ("Chyba nie powinno to nastąpić ;p");
                    }
                }
                else
                {//nieparzyste => (element ma brata po lewej stronie)
                 //Console.WriteLine ("#43 {0}", getId (idPrzeciwnik (start_real_id)));
                    if (dane[arr[getId(start_real_id)]].zwrocKlucz().CompareTo(dane[arr[getId(idPrzeciwnik(start_real_id))]].klucz) < 0)
                    {//kiedy wygram z przeciwnikiem
                     //Console.WriteLine ("#45");
                        arr[getId(getParentId(start_real_id))] = arr[getId(start_real_id)];
                        PushUp(getParentId(start_real_id));
                    }
                    else
                    {
                        //Console.WriteLine("#47");
                        arr[getId(getParentId(start_real_id))] = arr[getId(idPrzeciwnik(start_real_id))];
                        PushUp(getParentId(start_real_id));
                        /*if(tablica [getId (getParentId (start_real_id))].CompareTo(tablica [getId(idPrzeciwnik(start_real_id))])!=0){
                            tablica [getId (getParentId (start_real_id))] = tablica [getId(idPrzeciwnik(start_real_id))];
                            PushUp(idPrzeciwnik(start_real_id)); */
                        //}
                  }
                }
            }
        }

       public void DeleteMin()
        {
            Element<K, D> a = dane[arr[0]];
            int c = arr[0];
            if (real_size == 1)
            {
                size--;
                arr = null;
                dane[0] = null;
            }
            else if (real_size > 1)
            {
                

                int id_to_remove = FindMin(); //realne id (tablicowe) elementu który ma być usunięty

                
                           
                int ostatni_element = arr[getId(size)];
                Array.Resize(ref arr, getId(size));
                size--;
                // Console.WriteLine("1 {0} {1}", id_to_remove, size);
                if (getId(id_to_remove) != size)
                {
                    arr[getId(id_to_remove)]= ostatni_element;
                    
                    PushUp(id_to_remove);
                }


               /* 
                */
                /*
                for (int i = 0; i<arr.Length; i++)
                {
                    if (arr[i] > c)
                        arr[i] = arr[i] - 1;
                }

             */

                PushUp(size);
            }
            Console.Write("    " + arr.Length + "\n");
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > c)
                    arr[i] = arr[i] - 1;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > c)
                    arr[i] = arr[i] - 1;
            }

            real_size--;
        }
    
        int FindMin()//get id of the minimal element in the table. Zwraca id idealne tzn. przy odniesieniu do tablicy należy pobrać id elementu przez getId()
        {
            int min_id = 1;//id of min object (not real table id, but imagine tablie_id + 1
            K id_to_find = dane[arr[0]].zwrocKlucz();//the minimal object has own label in 1st object in table
            for (int pointer_id = 1; pointer_id * 2 < size;)
            {//follow the labels id_to_find

                pointer_id *= 2;//ids of object, which we follow by (we're getting child)
                                //Console.WriteLine("{0}", pointer_id);
                if (dane[arr[getId(pointer_id)]].zwrocKlucz().CompareTo(id_to_find) == 0)
                {
                    min_id = pointer_id;
                }
                else if (dane[arr[getId(pointer_id + 1)]].zwrocKlucz().CompareTo(id_to_find) == 0)
                {//and the next element (brother)
                    min_id = pointer_id + 1;
                }
                else
                {

                    //Console.WriteLine("");
                }
                pointer_id = min_id;
            }
            //Console.WriteLine("ZNALEZIONE ID {0}", min_id);
           
            return  min_id;
        }
        int getId(int i)//przekazuje id elementu w tablicy.
        {
            return i - 1;
        }
        int getParentId(double i)
        {
            double t = Math.Floor(i / 2);
            return (int)t;
        }
        int idPrzeciwnik(int i)// Pobranie id przciwnika (działa na liczbach idealnych)
        {
            int p = -1;
            if (i % 2 == 0)
                p = i + 1;
            else
                p = i - 1;

            return p;
        }
        public void Show()//Wyświetlanie drzewa
        {
            double h = 0;
            int element_in_row = 0;
            /*for (int i = 0; i < size; i++)
            {
                if (Math.Ceiling(Math.Log(i + 2, 2)) != h)
                {
                    Console.Write("\n");
                    h = Math.Ceiling(Math.Log(i + 2, 2));
                    element_in_row = 0;
                }
                element_in_row++;

                int space_size = size;

                if (element_in_row % 2 == 0) space_size *= 2;

                
            */
                for (int a=0; a<size;a++)
                {
                    Console.Write("indeks" + arr[a] + " wartosc    " + dane[arr[a]].klucz + "\n");

                }
                /*
                for (int space = 0; space < space_size / (Math.Pow(2, h - 1)); space++)
                {
                    Console.Write(" ");
                }

                Console.Write("{0}" ,dane[arr[i]].klucz.ToString()); */
            
            

            Console.WriteLine();
        }
     
        public void Dodaj(K klucz, D d) // dodawanie elementu do drzewa
        {
            Element<K, D> nowy = new Element<K, D>(klucz, d);
            //if(!quiet)
            //	Console.WriteLine("Dodano {0}", o);
            dane [real_size] = nowy;
            Array.Resize(ref arr, size + 1);
            arr[size] = real_size;
            size++;
            if (size > 1) Pojedynek(size); // funkcja porównująca z rywalem, gdy mamy 1 element nie występuje pojedynek
            real_size++;
            
            
        }
        void insertInside(int o) // dodawanie elementu do drzewa pomijając sortowanie
        {

            ;
            Array.Resize(ref arr, size + 1);

           arr[size] = o;
            size++;
        }
        void insertInsideFast(K o) // dodawanie elementu do drzewa nie musząc troszczyć sie o rozmiar
        {
          //  arr[size] = o;
            size++;
        }

        void copy(ref int a, ref int b) // funkcja zamieniająca ze sobą 2 obiekty
        {
            a = b;
        }
        void Pojedynek(int i) // i = rozmiar tablicy = indeks elementu +1
        {
            int przeciwnik = i / 2;

            insertInside(arr[getId(przeciwnik)]);
            while (dane[arr[getId(przeciwnik)]].zwrocKlucz().CompareTo(dane[arr[getId(i)]].zwrocKlucz()) > 0)
            {
                copy(ref arr[getId(przeciwnik)], ref arr[getId(i)]);
                if (przeciwnik == 1) break;

                i = przeciwnik;
                przeciwnik = i / 2;
            }
        }
        void PojedynekSzybki(int i) // pojedynak podczas tworzenia elementów po DeleteMin
        {
            int przeciwnik = i / 2;

            insertInsideFast(dane[arr[getId(przeciwnik)]].zwrocKlucz());

            while (dane[arr[getId(przeciwnik)]].zwrocKlucz().CompareTo(dane[arr[getId(i)]].zwrocKlucz()) > 0)
            {
                copy(ref arr[getId(przeciwnik)], ref arr[getId(i)]);
                if (przeciwnik == 1) break;

                i = przeciwnik;
                przeciwnik = i / 2;
            }
        }

       public int[] arr; // tablicowa struktura drzewa
        int size; // rozmiar drzewa
       public int real_size;//ilość prawdziwych elementów
       public Element<K, D>[] dane;
    }
}