using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    class Populacja
    {
        Osobnik[] pop;
        public  Osobnik[] Pop
        {
            get { return this.pop; }
            set 
            {
                if (pop == null)
                    this.pop = value;
                else
                {
                    this.pop = value;
                    RatePop();
                }
            }
        }

        Random rnd;

        public Populacja(Random rnd)
        {
            this.rnd = rnd;
            Pop = new Osobnik[Parm.m];
            GeneratePopulation();
        }

        void GeneratePopulation()
        {
            for (int i = 0; i < Pop.Length; i++)
            {
                Pop[i] = new Osobnik(rnd);
            }
        }

        public static Osobnik[] DefineOsobniks(int count)
        {
            Osobnik[] pop = new Osobnik[count];
            for (int i = 0; i < count; i++)
            {
                pop[i] = new Osobnik();
                for (int j = 0; j < pop[i].Geny.Length; j++)
                {
                    pop[i].Geny[j] = -1;
                }
            }
            return pop;
        }

        void RatePop()
        {
            foreach (var item in Pop)
            {
                item.Rating();
            }
        }

        public Osobnik GameWinner()
        {
            Osobnik nvp = new Osobnik();
            int lastBest = 999999999;
            for (int i = 0; i < Pop.Length; i++)
            {
                if(Pop[i].Ocena < lastBest)
                {
                    lastBest = Pop[i].Ocena;
                    nvp = Pop[i];
                }
            }
            return nvp;
        }

        public static Osobnik Min(Osobnik[] PopZ)
        {
            Osobnik nvp = new Osobnik();
            int lastBest = 99999999;
            foreach (var item in PopZ)
            {
                if(item.Ocena < lastBest)
                {
                    lastBest = item.Ocena;
                    nvp = item;
                }
            }
            return nvp;
        }
    }
}
