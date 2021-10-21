using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    static class Mutacja
    {
        public static void InversionMutate(Osobnik[] popBefore, out Osobnik[] popAfterM, Random rnd)
        {
            popAfterM = Populacja.DefineOsobniks(Parm.m);
            Osobnik Helper;
            int r1, r2, randomRate;

            for (int i = 0; i < Parm.m; i++)
            {
                randomRate = rnd.Next(10000);
                if( randomRate <= Parm.p)
                {
                    Helper = new Osobnik();
                    r1 = rnd.Next(Parm.n-1);
                    r2 = rnd.Next(r1,Parm.n);

                    for (int j = 0; j < Parm.n; j++)
                    {
                        Helper.Geny[j] = popBefore[i].Geny[j];
                    }

                    Array.Reverse(Helper.Geny, r1, r2 - r1);

                    for (int j = 0; j < Parm.n; j++)
                    {
                        popAfterM[i].Geny[j] = Helper.Geny[j];
                    }
                }
                else
                {
                    for (int j = 0; j < Parm.n; j++)
                    {
                        popAfterM[i].Geny[j] = popBefore[i].Geny[j];
                    }
                }
            }
        }
    }
}
