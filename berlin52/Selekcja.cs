using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    static class Selekcja
    {
        public static void Tournament(Osobnik[] popBefore, out Osobnik[] popAfterS, Random rnd)
        {
            // game Winner
            Osobnik dominant = new Osobnik();
            //uczestnicy turnieju
            Osobnik[] turniej = new Osobnik[Parm.s];
            //populacja po turnieju
            popAfterS = Populacja.DefineOsobniks(Parm.m);
            //
            int randomRate = 0;
            int[] indexMember;
            for (int i = 0; i < popBefore.Length; i++)
            {
                indexMember = new int[Parm.s];
                for (int j = 0; j < Parm.s; j++)
                {
                    randomRate = rnd.Next(Parm.m);
                    while(indexMember.Contains(randomRate))
                        randomRate = rnd.Next(Parm.m);
                    indexMember[j] = randomRate;
                }
                for (int j = 0; j < Parm.s; j++)
                {
                    turniej[j] = popBefore[indexMember[j]];
                }
                dominant = Populacja.Min(turniej);
                for (int j = 0; j < Parm.n; j++)
                {
                    popAfterS[i].Geny[j] = dominant.Geny[j];
                }
            }
        }

        public static void Tournament2 (Osobnik[] popBefore, out Osobnik[] popAfterS, Random rnd)
        {
            // game Winner
            Osobnik dominant = new Osobnik();
            //uczestnicy turnieju
            Osobnik[] turniej = new Osobnik[Parm.s];
            //populacja po turnieju
            popAfterS = new Osobnik[Parm.m];
            //
            int bestOcena;
            for (int i = 0; i < popBefore.Length; i++)
            {
                bestOcena = 999999;
                for (int j = 0; j < Parm.s; j++)
                {
                    turniej[j] = popBefore[rnd.Next(Parm.m)];
                    if (bestOcena > turniej[j].Ocena)
                    {
                        bestOcena = turniej[j].Ocena;
                        dominant = turniej[j];
                    }
                }
                popAfterS[i] = dominant;
            }
        }
    }
}
