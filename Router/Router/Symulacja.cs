using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;


namespace Router
{
    class Symulacja
    {
        #region
        double przepustowosc;
        int liczba_buforow;
        int liczbaStrumieni;
        int liczbaRozkladow;
     
       
        double sredni_czas_obslugi;
        double czas_symulacji;
        double minuty;
        double aktCzas;
        double[] srednia_wielkosc_pakietu;
        double[] srednio_pakietow;
        double[] srednia_obsluga;
        double[] wielkosc_pakietow;
        double[] sredni_odstep;
        double[] obsluga;
        double[] wyslane;
        double po_transmisji;
        int[] procent_odrzuconych;
        double srednia_zajetosc_lacza;
        int[] nadanepakiety;
        double[] srednia_zajetosc_kolejek;
        int[] odrzuconepakiety;
        Rozklad[] rozklady;
        Lista<double, double>[] bufor;
        Strumien[] strumienie;
        Lista<double, Zdarzenie> zdarzenia;
      
        string nazwaSystemu;
        string sciezka_wejscia;
        string sciezka_wyjscia;
        int m;
        bool transmisja;
        double bezczynnosc;
        int y;
        double z;
        double zajetosc;
        #endregion

        public Symulacja()
        {
            WczytajDane();
            zdarzenia = new Lista<double, Zdarzenie>();
            PowiazRozklady();
            czas_symulacji = aktCzas = 0.0;
            //  liczbaStrumieni = liczba_s;
            //liczba_buforow = liczba_b;

            
            //czas_symulacji = czas;
            
            sredni_czas_obslugi = 0;

            //zajetosc_lacza = 0;
           m = 0;
            odrzuconepakiety = new int[liczbaStrumieni];
             nadanepakiety = new int[liczbaStrumieni];
            procent_odrzuconych=new int [liczba_buforow];
            wielkosc_pakietow = new double[liczbaStrumieni];
            srednio_pakietow = new double[liczba_buforow];
            obsluga = new double[liczba_buforow];
            wyslane = new double[liczba_buforow];
             srednia_wielkosc_pakietu = new double[liczbaStrumieni];
            srednia_zajetosc_kolejek= new double[liczba_buforow]; ;
            srednia_obsluga = new double[liczba_buforow];
            sredni_odstep= new double[liczbaStrumieni];
            bezczynnosc = 0;
            z = 0;
            po_transmisji = 0;
        }



        public void symulacja()  // funkcja przeprowadzająca symulację działania centrali telefonicznej 
        {

          
            double[] poprzedniczas = new double[liczbaStrumieni];

            // Random rnd = new Random(DateTime.Now.Millisecond);      
            Random rnd = new Random(DateTime.Now.Millisecond);

            // int []odrzuconepakiety = new int[liczbaStrumieni];
            // int[] nadanepakiety = new int[liczbaStrumieni];


            double czas;

            double rozmiar;
         


            while (czas_symulacji == 0)
            {
               
                try
                {
                    Console.WriteLine("Podaj czas symulacji w minutach (wiecej niz 0): ");
                    minuty = double.Parse(Console.ReadLine()); // wczytywanie czasu końca trwania symulacji
                    if (minuty < 0.0) throw new Exception();
                    czas_symulacji = 60000 * minuty;     // Czas symulacji w milisekundach
                }
                catch (Exception)
                {
                    Console.WriteLine("Podales zla liczbe. Sprobuj jeszcze raz...");
                }
                for (int i = 0; i < liczbaStrumieni; i++) // wygenerowanie pierwszego zdarzenia typu przyjście dla każdego strumienia
                {

                    czas = rozklady[strumienie[i].ZwrocNrRozkladuOdstepu()].podajWartosc(rnd.NextDouble());
                    rozmiar = rozklady[strumienie[i].ZwrocNrRozkladuRoziaru()].podajWartosc(rnd.NextDouble());
                    wielkosc_pakietow[i] = rozmiar;
                    zdarzenia.Dodaj(aktCzas + czas, Zdarzenie.Przyjscie(i));

                    if (bufor[i].zwrocPojemnosc() > 0)
                    {
                        bufor[i].Dodaj(0, rozmiar);
                       
                        odrzuconepakiety[i] = 0;
                    }
                    else
                        odrzuconepakiety[i] = 1;
                    Console.Write(czas + "\n");
                    nadanepakiety[i] = 1;
                    srednia_zajetosc_kolejek[i] = 0;
                    poprzedniczas[i] = 0;
                    wyslane[i] = 0;
                    obsluga[i] = 0;
                    sredni_odstep[i] = czas;
                }
                Console.Write(przepustowosc + "\n");
            }

            // -1 przyjscie
            // -2 wyjscie

            while (aktCzas < czas_symulacji)
            {
                double b;
                
                
           //  Console.Write(transmisja + "\n");
                m++;
             //   Console.Write(m + "\n");
                for (int i = 0; i < liczba_buforow; i++)
                {

                    srednia_zajetosc_kolejek[i] += bufor[i].zwrocLiczbeObiektow();
                }

                //  Console.Write(aktCzas + "\n");

                Element<double, Zdarzenie> nowy = new Element<double, Zdarzenie>();
                if (zdarzenia.zwrocLiczbeObiektow() > 0)
                {
                    nowy = zdarzenia.Usun_Najmniejszy();
                    b = nowy.zwrocKlucz();
                    bezczynnosc = b - aktCzas;
                    aktCzas = nowy.zwrocKlucz();
                   // Console.Write(nowy.zwrocDane().ZwrocRodzaj() + "\n");

                }
                switch (nowy.zwrocDane().ZwrocRodzaj())
                {
                    case -1:
                        y = nowy.zwrocDane().ZwrocNr();
                        nadanepakiety[y]++;
                        rozmiar = rozmiar = rozklady[strumienie[y].ZwrocNrRozkladuRoziaru()].podajWartosc(rnd.NextDouble());
                        wielkosc_pakietow[y] += rozmiar;
                        czas = rozklady[strumienie[y].ZwrocNrRozkladuOdstepu()].podajWartosc(rnd.NextDouble());
                        sredni_odstep[y] += czas;
                        zdarzenia.Dodaj(aktCzas + czas, Zdarzenie.Przyjscie(y));
                        if (bufor[y].zwrocLiczbeObiektow() < bufor[y].zwrocPojemnosc())
                        { bufor[y].Dodaj(aktCzas, rozmiar); }


                        else
                        { odrzuconepakiety[y]++; }

                        if (transmisja == false)
                            for (int i = 0; i < liczba_buforow; i++)
                            {
                                if (bufor[i].zwrocLiczbeObiektow() > 0 && bufor[i].zwrocPojemnosc() != 0 && transmisja == false)
                                {
                                    Element<double, double> tmp = new Element<double, double>();
                                    tmp = bufor[i].Usun_Najmniejszy();
                                    zdarzenia.Dodaj(aktCzas + (tmp.zwrocDane() / przepustowosc)*1000, Zdarzenie.WyjscieDoLacza(i));
                                    wyslane[i]++;
                                    obsluga[i] += (tmp.zwrocDane() / przepustowosc)*1000 + aktCzas - tmp.zwrocKlucz();
                                    transmisja = true;
                                    // Console.Write(aktCzas - tmp.zwrocKlucz()+ "\n");
                                    sredni_czas_obslugi += (tmp.zwrocDane() / przepustowosc)*1000;
                                    z = (tmp.zwrocDane() / przepustowosc) * 1000;
                                    //  Console.Write(sredni_czas_obslugi + "\n");
                                    // Console.Write( (tmp.zwrocDane() / przepustowosc) + "\n");
                                }
                            }


                        break;

                    case -2:
                        transmisja = false;
                        po_transmisji++;
                       
                        if (transmisja == false)
                            for (int i = 0; i < liczba_buforow; i++)
                            {
                                if (bufor[i].zwrocLiczbeObiektow() > 0 && bufor[i].zwrocPojemnosc() != 0 && transmisja == false)
                                {
                                    Element<double, double> tmp = new Element<double, double>();
                                    tmp = bufor[i].Usun_Najmniejszy();
                                    zdarzenia.Dodaj(aktCzas + (tmp.zwrocDane() / przepustowosc)*1000, Zdarzenie.WyjscieDoLacza(i));
                                    wyslane[i]++;
                                    obsluga[i] += (tmp.zwrocDane() / przepustowosc)*1000 + aktCzas - tmp.zwrocKlucz();
                                    transmisja = true;
                                    // Console.Write(aktCzas - tmp.zwrocKlucz()+ "\n");
                                    sredni_czas_obslugi += (tmp.zwrocDane() / przepustowosc)*1000;
                                    z= (tmp.zwrocDane() / przepustowosc) * 1000;
                                    // Console.Write(sredni_czas_obslugi + "\n");
                                }
                            }
                        break;

                }
            }
                //    Console.Write(nadanepakiety[1] + "\n");
                //   Console.Write(odrzuconepakiety[1] + "\n");
                ObliczStatystyki();
                WypiszDane();
                Console.WriteLine("Wyniki symulacji wypisano do pliku");
            

        }


        public void ObliczStatystyki()
        {
            double wszystkie=0;
            
          
            for (int i=0; i<liczba_buforow;i++)
            {
                sredni_odstep[i] = sredni_odstep[i] / nadanepakiety[i];
                procent_odrzuconych[i] = odrzuconepakiety[i]*100 / nadanepakiety[i];
                  Console.Write(odrzuconepakiety[i] +  " "+nadanepakiety[i] + "%" + "\n");
                srednio_pakietow[i] = srednia_zajetosc_kolejek[i] / m;
                srednia_zajetosc_kolejek[i] = (srednia_zajetosc_kolejek[i] / m )/bufor[i].zwrocPojemnosc();
                //  Console.Write("nr bufora" + i + " "+srednia_zajetosc_kolejek[i] + "%" + "\n");
                srednia_wielkosc_pakietu[i]=wielkosc_pakietow[i]/nadanepakiety[i];
                wszystkie += wyslane[i];
                if (obsluga[i] == 0 || wyslane[i] == 0)
                { srednia_obsluga[i] = 0; }

                else
                { srednia_obsluga[i] = obsluga[i] / wyslane[i]; }
             
            
                
                //Console.Write("Liczba pakietow ze strumienia"  + i +"  "+ nadanepakiety[i]  + "\n");
              //  Console.Write("Liczba pakietow odrzucnych z" + i + "  " + odrzuconepakiety[i] + "\n");
               Console.Write(obsluga[i] + "\n");
                              
            }
            sredni_czas_obslugi = sredni_czas_obslugi - z;
            zajetosc = sredni_czas_obslugi / czas_symulacji;
            sredni_czas_obslugi = sredni_czas_obslugi / po_transmisji;
            Console.Write(po_transmisji);
            // Console.Write("Sr zajetosc lacza" + " " + srednia_zajetosc_lacza + " bajtow" + "\n");

        }

        public void WczytajDane()
        {
            bool ok = false;
            while (!ok)
            {
                StreamReader sr;
                string[] wyrazy;
                ok = true;
                try
                {
                    Console.WriteLine("Przeciagnij tu plik wejsciowy i wcisnij ENTER...");
                    sciezka_wejscia = Console.ReadLine();
                    if (sciezka_wejscia[0] == '\"') sciezka_wejscia = sciezka_wejscia.Substring(1, sciezka_wejscia.Length - 2);
                    Console.WriteLine(" ");
                    sr = new StreamReader(sciezka_wejscia);
                    String linia = "";
                  # region nazwa systemu
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "SYSTEM" && wyrazy[2] != "") nazwaSystemu = wyrazy[2];
                    else throw (new Exception("Zla nazwa systemu"));
                    #endregion

                    #region przepustosc
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "PRZEPLYWNOSC" && wyrazy[2] != "") przepustowosc = (double.Parse(wyrazy[2]))/8;
                    
                    else throw (new Exception("Zla przepustowosc"));
                    #endregion
                    
                    #region kolejki
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "KOLEJKI" && wyrazy[2] != "") liczba_buforow = int.Parse(wyrazy[2]);
                    else throw (new Exception("Zla liczba kolejek"));
                
                    #endregion                 
                    
                    #region wczytywanie kolejek
                    bufor = new Lista<double, double> [liczba_buforow];
                    int priorytet = 0;
                    for (int i = 0; i < liczba_buforow; i++)
                    {
                        string nazwa;
                        int rozmiar;
                        
                        linia = "";
                        while (linia.Length < 2 || linia[0] == '#')
                        {
                            linia = sr.ReadLine();
                        }
                        wyrazy = linia.Split(' ');
                        if (wyrazy[0] == "NAZWA" && wyrazy[2] != "" && wyrazy[3] == "BUFOR" && wyrazy[5] != "")
                        {
                            nazwa = wyrazy[2];
                           
                            rozmiar = int.Parse(wyrazy[5]);
                          
                        }
                        else throw (new Exception("Zla nazwa rozkladu"));
                        linia = "";
                       

                       bufor[i] = new Lista <double, double> (nazwa,  rozmiar, priorytet);
                        priorytet++;
                    }
                    #endregion
                    
                    #region liczba rozkladow
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    
                    if (wyrazy[0] == "ROZKLADY" && wyrazy[2] != "")
                    { liczbaRozkladow = int.Parse(wyrazy[2]);
                       // Console.Write(liczbaRozkladow);
                    }
                    else throw (new Exception("Zla liczba rozkładów"));
                    #endregion     
                    
                    #region wczytywanie rozkladow
                    rozklady = new Rozklad[liczbaRozkladow];
                    for (int i = 0; i < liczbaRozkladow; i++)
                    {
                        string nazwa;
                        
                        double lambda;
                        linia = "";
                        while (linia.Length < 2 || linia[0] == '#')
                        {
                            linia = sr.ReadLine();
                        }
                        wyrazy = linia.Split(' ');
                       
                        if (wyrazy[0] == "NAZWA" && wyrazy[2] != "")
                        {
                            nazwa = wyrazy[2];
                           // Console.Write(wyrazy[2]);
                            //Console.Write(nazwa);
                        }
                        else throw (new Exception("Zla nazwa rozkladu"));
                        linia = "";
                        while (linia.Length < 2 || linia[0] == '#')
                        {
                            linia = sr.ReadLine();
                        }
                        wyrazy = linia.Split(' ');
                       
                        if (wyrazy[0] == "LAMBDA" && wyrazy[2] != "")
                        {// Console.Write("\n");
                            lambda = double.Parse(wyrazy[2]);
                          //  Console.Write(wyrazy[2]);
                           // Console.Write(lambda);
                        }
                        else throw (new Exception("Zla lambda rozkladu"));

                        rozklady[i] = new Rozklad(nazwa, lambda);
                       // Console.WriteLine(rozklady[i].ZwrocLambde()-1);
                       
                       //Console.Write( rozklady[i].ZwrocNazwe());
                    }
                    #endregion     
                                 
                    #region liczba strumieni
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "STRUMIENIE" && wyrazy[2] != "")
                        liczbaStrumieni = int.Parse(wyrazy[2]);
                    else throw (new Exception("Zla liczba strumieni"));
                    #endregion
                    #region wczytywanie strumieni
                    strumienie = new Strumien[liczbaStrumieni];
                    int pr=liczba_buforow;

                    pr = 1;
                    for (int i = 0; i < liczbaStrumieni; i++)
                    {
                        string nazwa;
                        string kolejka;
                        int nr;
                        string czas;
                        string wielkosc;
                        linia = "";
                        while (linia.Length < 2 || linia[0] == '#')
                        {
                            linia = sr.ReadLine();
                        }
                        wyrazy = linia.Split(' ');
                        if (wyrazy[0] == "NAZWA" && wyrazy[2] != "" && wyrazy[3] == "KOLEJKA" && wyrazy[5] != "" &&
                            wyrazy[6] == "CZAS" && wyrazy[8] != "" && wyrazy[9] == "WIELKOSC" &&
                            wyrazy[11] != "")
                        {
                            nazwa = wyrazy[2];
                        //    Console.Write(nazwa);
                            kolejka = wyrazy[5];
                          // Console.Write(kolejka );
                            czas = wyrazy[8];
                          //  Console.Write(czas );
                            wielkosc = wyrazy[11];
                           // Console.Write(wielkosc );
                            nr = i;
                        }
                        else throw (new Exception("Zle dane strumienia"));
                        //te i trzeba jeszcze psoprawdzac bo na bank jest troche inczej
                        strumienie[i] = new Strumien(nazwa, wielkosc, czas, pr, kolejka);
                        pr++;
                    }
                    #endregion                    
                    
                    Console.WriteLine("Przeciagnij tu plik wyjsciowy i wcisnij ENTER...");
                    sciezka_wyjscia = Console.ReadLine();
                    Console.WriteLine(" ");
                    if (sciezka_wyjscia[0] == '"') sciezka_wyjscia = sciezka_wyjscia.Substring(1, sciezka_wyjscia.Length - 2);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zla sciezka. Sprobuj jeszcze raz.");
                    Console.WriteLine(e.Message);
                    ok = false;
                }
            }
        }

        public void PowiazRozklady() //znajdujemy indeksy rozkladow i strumieni w tablicach 
        {
           // Console.Write(rozklady[0].ZwrocNazwe());
           // Console.Write(rozklady[1].ZwrocNazwe());
           // Console.Write(rozklady[2].ZwrocNazwe());
           // Console.Write(rozklady[3].ZwrocNazwe());
            try
            {
                for (int i = 0; i < liczbaStrumieni; i++)
                {
                   // Console.Write(strumienie[i].ZwrocNazweRozkladuOdstepu());
                    //Console.Write("\n");
                    
                    int j = 0;
                    
                    while (strumienie[i].ZwrocNazweRozkladuRozmiaru() != rozklady[j].ZwrocNazwe()) j++;
                    strumienie[i].UstawNrRozkladuRozmiaru(j);
                  
                    j = 0;
                    while (strumienie[i].ZwrocNazweRozkladuOdstepu() != rozklady[j].ZwrocNazwe()) j++;
                    strumienie[i].UstawNrRozkladuOdstepu(j);
                    //Console.Write(strumienie[i].ZwrocNrRozkladuOdstepu() + "\n");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nazwa jednego z rozkladow podanych dla zlecenia nie istnieje w podanych rozkladach!");
            }
        }

        public void WypiszDane()
        {
            StreamWriter wyniki = new StreamWriter(sciezka_wyjscia);

            wyniki.WriteLine("Nazwa systemy to: " + nazwaSystemu);
            wyniki.WriteLine("");
            wyniki.WriteLine("Czas trwania symulacji : " + czas_symulacji+" ms");
            wyniki.WriteLine("Przepustowosc w bajtach  : " + przepustowosc);
            wyniki.WriteLine("");
            for (int i = 0; i < liczbaStrumieni; i++)
            {
                wyniki.WriteLine("Prawdopodobieństwo odrzucenia pakietu ze strumienia " + strumienie[i].ZwrocNazwe() + " wynosi: " + procent_odrzuconych[i] + "%");
               
                wyniki.WriteLine("Srednia wielkosc pakietu ze strumienia: "  + strumienie[i].ZwrocNazwe() + " wynosi: " +srednia_wielkosc_pakietu[i]+ "bajt");
                wyniki.WriteLine("Sredni odstep miedzy pakietami ze strumienia " + strumienie[i].ZwrocNazwe() + " wynosi: " + sredni_odstep[i] + "ms");
                wyniki.WriteLine("Nadane pakiety ze strumienia " + strumienie[i].ZwrocNazwe() + " wynosi: " + nadanepakiety[i]);
                wyniki.WriteLine("");
            }

            wyniki.WriteLine("");
             for (int i = 0; i < liczba_buforow; i++)
                {
                wyniki.WriteLine("Sredni czas obslugi w kolejece: " + bufor[i].zwrocNazweKolejki()+" wynosi: "+ srednia_obsluga[i]+ " ms");
                wyniki.WriteLine("Sredni ilosc pakietow w kolejce " + bufor[i].zwrocNazweKolejki() + " wynosi: " + srednio_pakietow[i]);
                wyniki.WriteLine("Sredni zajetosc Kolejki " + bufor[i].zwrocNazweKolejki() + " wynosi: " + srednia_zajetosc_kolejek[i]*100 + " %");
                wyniki.WriteLine("Licza pakietow wyslanych do transmisji z kolejki  " + bufor[i].zwrocNazweKolejki() + " wynosi: " + wyslane[i]);
                wyniki.WriteLine("");
            }

            wyniki.WriteLine("");
            wyniki.WriteLine("Sredni czas wysylania: " + sredni_czas_obslugi+ " ms");
            wyniki.WriteLine("Zajetosc lacza: " + zajetosc*100 + " %");
            wyniki.WriteLine("Pakiety ktorych transmisja zakonczyla sie " + po_transmisji);

            wyniki.Close();

        }
    }
}
