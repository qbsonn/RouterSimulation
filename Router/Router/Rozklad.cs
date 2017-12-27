using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Rozklad    
    {

        private double lambda;
        private string nazwa;

        public Rozklad(string n, double l)
        {
           nazwa = n;
           lambda = l;
        }

        public double podajWartosc(double x)
        {
            return ((-1) * Math.Log(1 - x) / lambda); //przeksztalcony wzor na rozklad wykladniczy
        }

        public string ZwrocNazwe() { return nazwa; }
        public double ZwrocLambde() { return lambda; }

    }
}
