using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace berlin52
{
    class Program
    {
        public static Random rnd;
        static void Main(string[] args)
        {
            rnd = new Random();
            Dane.LoadDane();
            Populacja p1 = new Populacja(rnd);
            //PopulacjaPoIteracji(p1,rnd);
            Zawody(p1,rnd);
            Console.ReadKey();
        }

        static void Zawody(Populacja p1, Random rnd)
        {
            ConsoleKeyInfo cki;
            int i = 0;
            while (!Console.KeyAvailable)
            {
                i++;
                Selekcja.Tournament(p1.Pop, out Osobnik[] popAfterS, rnd);
                Krzyzowanie.CrossoverPMX(popAfterS, out Osobnik[] popAfterC, rnd);
                Mutacja.InversionMutate(popAfterC, out Osobnik[] popAfterM, rnd);
                p1.Pop = popAfterM;
                if (i % 1000 == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Generation: {i}");
                    Osobnik.printOsobnik(p1.GameWinner());
                }
            }
            cki = Console.ReadKey();
            Console.WriteLine("Koniec");
            Osobnik.print3(p1.GameWinner());
        }

        static void PopulacjaPoIteracji(Populacja p1, Random rnd)
        {
            Osobnik.printArray(p1.Pop, "Przed selekcja");
            Selekcja.Tournament(p1.Pop, out Osobnik[] popAfterS, rnd);
            p1.Pop = popAfterS;
            Osobnik.printArray(popAfterS, "Po Selekcji");
            Krzyzowanie.CrossoverPMX(popAfterS, out Osobnik[] popAfterC,rnd);
            p1.Pop = popAfterC;
            Osobnik.printArray(popAfterC, "Po Krzyzowaniu");
            Mutacja.InversionMutate(popAfterC, out Osobnik[] popAfterM, rnd);
            p1.Pop = popAfterM;
            Osobnik.printArray(popAfterM, "Po Mutacji");
        }
    }
}
