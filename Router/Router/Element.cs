using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
   public  class Element<K, D> where K : IComparable
    {
        public K klucz;
        private D dane;

        public Element()
        { 
            klucz = default(K);
            dane = default(D);
        }

        public Element (K k, D d)
        {
            klucz = k;
            dane = d;
        }
        public K zwrocKlucz ()
        {
            return klucz;
        }

        public D zwrocDane() { return dane; }
        public void ustawDane(D d_dane) { dane = d_dane; }

    }
}

