using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    public static class Parm
    {
        /// <summary>
        /// parametr mutacji (szansa na mutacje)
        /// </summary>
        public static readonly int p = 200; // Mutacja 1 = 0.01%
        /// <summary>
        /// parametr krzyzowania (szansa na mutacje)
        /// </summary>
        public static readonly int k = 3; // Krzyżowanie 1 = 1%
        /// <summary>
        /// nacisk selektywny (osobnicy bioracy udzial w pojedynczym turnieju)
        /// </summary>
        public static readonly int s = 3; // Selekcja
        /// <summary>
        /// zakres populacji
        /// </summary>
        public static readonly int m = 40; // Populacja
        /// <summary>
        /// liczba miast
        /// </summary>
        public static int n = 0;

        /// <summary>
        /// zamiana miejsc po nieudanum krzyzowaniu 
        /// </summary>
        public static readonly bool kRoll = true;

        public static readonly string plikTSP = @"..\..\zawody2020n\kroC100.txt";
        public static readonly string plikWyniku = @"..\..\wyniki\kroC100_wynik.txt";


    }
}
