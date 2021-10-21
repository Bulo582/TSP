using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    static class Krzyzowanie
    {
        


        public static void CrossoverPMX(Osobnik[] popBefore, out Osobnik[] popAfterC, Random rnd)
        {
            popAfterC = Populacja.DefineOsobniks(Parm.m);
            Osobnik Helper1;
            Osobnik Helper2;
            //punkty przeciecia
            int r1 = 0, r2 = 0, randomRate;

            for (int i = 0; i < Parm.m; i+= 2)
            {
                randomRate = rnd.Next(1,100);
                if(randomRate <= Parm.k)
                {
                    Helper1 = new Osobnik();
                    Helper2 = new Osobnik();
                    r1 = rnd.Next(Parm.n-1);
                    r2 = rnd.Next(r1,Parm.n);

                    for (int j = r1; j <= r2; j++)
                    {
                        popAfterC[i].Geny[j] = popBefore[i + 1].Geny[j];
                        popAfterC[i + 1].Geny[j] = popBefore[i].Geny[j];
                    }

                    for (int j = 0; j < Parm.n; j++)
                    {
                        // replace help1 - help 2
                        Helper1.Geny[j] = popAfterC[i].Geny[j];
                        Helper2.Geny[j] = popAfterC[i + 1].Geny[j];
                    }

                    // first
                    for (int k = 0; k < Parm.n; k++)
                    {
                        if (k == r1)
                        {
                            k += r2 - r1;
                        }
                        else
                        {
                            int g = popBefore[i].Geny[k];
                            if (Helper1.Geny.Contains(g))
                            {
                                while (Helper1.Geny.Contains(g))
                                {
                                    int idx = Array.IndexOf(Helper1.Geny, g);
                                    g = Helper2.Geny[idx];
                                }
                                popAfterC[i].Geny[k] = g;
                                Helper1.Geny[k] = g;
                            }
                            else
                                popAfterC[i].Geny[k] = popBefore[i].Geny[k];
                        }
                    }
                    // second
                    for (int k = 0; k < Parm.n; k++)
                    {
                        if (k == r1)
                        {
                            k += r2 - r1;
                        }
                        else
                        {
                            int g = popBefore[i + 1].Geny[k];
                            if (Helper2.Geny.Contains(g))
                            {
                                while (Helper2.Geny.Contains(g))
                                {
                                    int idx = Array.IndexOf(Helper2.Geny, g);
                                    g = Helper1.Geny[idx];
                                }
                                popAfterC[i + 1].Geny[k] = g;
                                Helper2.Geny[k] = g;
                            }
                            else
                                popAfterC[i + 1].Geny[k] = popBefore[i + 1].Geny[k];
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < Parm.n; j++)
                    {
                        if (Parm.kRoll)
                        {
                            popAfterC[i].Geny[j] = popBefore[i + 1].Geny[j];
                            popAfterC[i + 1].Geny[j] = popBefore[i].Geny[j];
                        }
                        else
                        {
                            popAfterC[i].Geny[j] = popBefore[i].Geny[j];
                            popAfterC[i + 1].Geny[j] = popBefore[i + 1].Geny[j];
                        }
                    }
                }
            }
        }
    }
}
