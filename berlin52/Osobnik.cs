using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace berlin52
{
    class Osobnik
    {
        Random rnd;
        int[] geny = new int[Parm.n];
        int ocena;

        public Osobnik() { }

        public Osobnik(Random rnd)
        {
            this.rnd = rnd;
            OsobnikDrawWithoutReturning(geny);
            Rating();
        }

        public int Ocena
        {
            get { return this.ocena; }
        }

        public int[] Geny
        {
            get { return geny; }
            set { geny = value; }
        }

        private void OsobnikDrawWithoutReturning(int[] osobnik)
        {
            // zaślepka
            int plug = 0;
            for (int i = 0; i < Parm.n; i++)
            {
                osobnik[i] = -1;
            }
            for (int i = 0; i < Parm.n; i++)
            {
                plug = rnd.Next(Parm.n);
                while (osobnik.Contains(plug))
                {
                    plug = rnd.Next(Parm.n);
                }
                osobnik[i] = plug;
            }
        }

        public int Rating()
        {
            int remeberFirstOne = 0;
            int first, second;
            int count = 0;
            for (int i = 0; i < Parm.n; i++)
            {
                if (geny[i] == -1)
                    throw new Exception("gen przymuje wartosc -1");
                if (i == 0)
                    remeberFirstOne = geny[i];
                if (i == Parm.n - 1)
                {
                    count += Dane.Berlin52[geny[i], remeberFirstOne];
                    break;
                }

                first = geny[i];
                second = geny[i + 1];
                count += Dane.Berlin52[first, second];
            }
            ocena = count;
            return count;
        }



        #region Prints
        public string print()
        {
            string result = String.Empty;
            Console.ForegroundColor = ConsoleColor.Green;
            List<int> LExist = new List<int>();
            for (int i = 0; i < geny.Length; i++)
            {
                if(LExist.Contains(geny[i]))
                    Console.ForegroundColor = ConsoleColor.Red;
                LExist.Add(geny[i]);
                if(i!=geny.Length-1)
                    result += $"{geny[i].ToString()}-";
                else
                    result += $"{geny[i].ToString()}";
                Console.ForegroundColor = ConsoleColor.Green;
            }
            return result;
        }

        public void print2(string title)
        {
            Console.WriteLine($"\n{title}\n");
            string result = String.Empty;
            Console.ForegroundColor = ConsoleColor.Green;
            List<int> LExist = new List<int>();
            for (int i = 0; i < geny.Length; i++)
            {
                if (LExist.Contains(geny[i]))
                    Console.ForegroundColor = ConsoleColor.Red;
                LExist.Add(geny[i]);
                Console.Write( $"{geny[i].ToString()} ");
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Do pliku
        /// </summary>
        /// <param name="population"></param>
        public static void print3(Osobnik[] population)
        {
            using (StreamWriter sw = new StreamWriter(Parm.plikWyniku))
            {
                foreach (var item in population)
                {
                    sw.WriteLine($"{item.print()} {item.Ocena}");
                }
            }
        }
        /// <summary>
        /// Do pliku
        /// </summary>
        /// <param name="osobnik"></param>
        public static void print3(Osobnik osobnik)
        {
            using (
                StreamWriter sw = new StreamWriter(Parm.plikWyniku))
            {

                sw.WriteLine($"{osobnik.print()} {osobnik.Ocena}");
                
            }
        }
        /// <summary>
        /// w konsoli
        /// </summary>
        /// <param name="population"></param>
        public static void printArray(Osobnik[] population, string title)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n{title}\n");
            int cnt = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var item in population)
            {
                cnt++;
                Console.WriteLine($"({cnt})   {item.print()} {item.Ocena}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void printOsobnik (Osobnik osobnik)
        {
            string result = String.Empty;
            for (int i = 0; i < osobnik.geny.Length; i++)
            {
                if (i != osobnik.geny.Length - 1)
                    result += $"{osobnik.geny[i].ToString()}-";
                else
                    result += $"{osobnik.geny[i].ToString()}";
            }
            Console.WriteLine($"Rate:{osobnik.Ocena}");
            Console.WriteLine($"Way: {result}");
        }

        public override string ToString()
        {
            return string.Format("Ocena: {0}", Ocena);
        }

        #endregion
        /// <summary>
        /// Sprawdza czy geny się nie powtarzając 
        /// </summary>
        /// <returns></returns>
        public bool OnlyOne()
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            foreach (var item in Geny)
            {
                if (!keyValuePairs.ContainsKey(item))
                    keyValuePairs.Add(item, 1);
                else
                    return false;
            }
            return true;
        }
    }
}
