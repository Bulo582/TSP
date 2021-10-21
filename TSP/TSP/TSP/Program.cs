using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TSP
{
    class Program
    {
        public static int cityCount = 0;
        public static string path = @"..\..\Berlin52.txt";
        public static int[,] valuesTable;
        public static int population = 40;
        public static bool keyPressed = false;

        //Wczytanie odleglosci z pliku do tablicy
        public static void LoadValuesFromFile(string path)
        {
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                cityCount = Convert.ToInt32(reader.ReadLine());
                int i = 0;
                valuesTable = new int[cityCount, cityCount];
                string[] tempStringTable;
                while ((line = reader.ReadLine()) != null)
                {
                    tempStringTable = line.Trim().Split(' ');
                    for (int j = 0; j < tempStringTable.Length; j++)
                    {
                        valuesTable[i, j] = Convert.ToInt32(tempStringTable[j]);
                        valuesTable[j, i] = valuesTable[i, j];
                    }
                    i++;
                }
            }
        }

        //Tworzenie losowej populacji
        public static int[,] randomPopulation()
        {
            bool[] boolTable = new bool[cityCount];
            int[,] startPopulation = new int[population, cityCount];
            int randomNumber;
            Random rnd = new Random();
            for (int i = 0; i < cityCount; i++)
            {
                boolTable[i] = false;
            }
            for (int i = 0; i < population; i++)
            {
                for (int j = 0; j < cityCount; j++)
                {
                    do
                    {
                        randomNumber = rnd.Next(cityCount);
                    } while (boolTable[randomNumber]);
                    startPopulation[i, j] = randomNumber;
                    boolTable[randomNumber] = true;
                }
                for (int k = 0; k < cityCount; k++)
                {
                    boolTable[k] = false;
                }
            }

            return startPopulation;
        }

        //Liczenie oceny funckji
        public static int[] calucalateFitness(int[,] populationTab)
        {
            int[] fitnessTable = new int[population];

            for (int i = 0; i < populationTab.GetLength(0); i++)
            {
                for (int j = 0; j < cityCount-1; j++)
                {
                    fitnessTable[i] += valuesTable[populationTab[i, j], populationTab[i, j+1]];
                }
                fitnessTable[i] += valuesTable[populationTab[i, cityCount - 1], populationTab[i, 0]];
            }

            return fitnessTable;
        }

        //Selekcja Turniejowa
        public static int[,] tournamentSelection(int[,] populationTab, int[] fitnessTable, int k)
        {
            int[,] tournamentTab = new int[population, cityCount];
            Random rnd = new Random();
            int[] indexOfMember = new int[k];
            int[] tournamentMember = new int[k];

            for (int i = 0; i < population; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    indexOfMember[j] = rnd.Next(population);
                }

                for (int j = 0; j < k; j++)
                {
                    tournamentMember[j] = fitnessTable[indexOfMember[j]];
                }

                for (int j = 0; j < cityCount; j++)
                {
                    tournamentTab[i, j] = populationTab[Array.IndexOf(fitnessTable, tournamentMember.Min()), j];
                }
            }
            
            return tournamentTab;
        }

        //Krzyzowanie PMX
        private static int[,] crossover(int[,] populationTab, int crossoverRate)
        {
            int[,] crossoverTab = new int[populationTab.GetLength(0), populationTab.GetLength(1)];
            int[] helperTab = new int[populationTab.GetLength(1)];
            int[] helperTab2 = new int[populationTab.GetLength(1)];
            Random random = new Random();
            int limit1 = 0, limit2 = 0, randomRate;

            for (int i = 0; i < populationTab.GetLength(0); i++)
            {
                for (int j = 0; j < populationTab.GetLength(1); j++)
                {
                    crossoverTab[i, j] = -1;
                }
            }
            for (int i = 0; i < populationTab.GetLength(0); i += 2)
            {
                randomRate = random.Next(100);
                if ((randomRate > crossoverRate))
                {
                    limit1 = random.Next(populationTab.GetLength(1));
                    limit2 = random.Next(populationTab.GetLength(1));

                    if (limit1 == limit2)
                    {
                        while (limit1 == limit2)
                        {
                            limit1 = random.Next(populationTab.GetLength(1));
                            limit2 = random.Next(populationTab.GetLength(1));
                        }
                    }

                    if (limit2 < limit1)
                    {
                        int tmp = limit1;
                        limit1 = limit2;
                        limit2 = tmp;
                    }

                    for (int j = limit1; j <= limit2; j++)
                    {
                        crossoverTab[i, j] = populationTab[i + 1, j];
                        crossoverTab[i + 1, j] = populationTab[i, j];
                    }


                    for (int l = 0; l < populationTab.GetLength(1); l++)
                    {
                        helperTab[l] = crossoverTab[i, l];
                    }
                    for (int l = 0; l < populationTab.GetLength(1); l++)
                    {
                        helperTab2[l] = crossoverTab[i + 1, l];
                    }

                    for (int j = 0; j < populationTab.GetLength(1); j++)
                    {
                        if (j == limit1)
                        {
                            j += limit2 - limit1;
                        }
                        else
                        {
                            int a = populationTab[i, j];
                            if (helperTab.Contains(a))
                            {
                                while (helperTab.Contains(a))
                                {
                                    int indeks = Array.IndexOf(helperTab, a);
                                    a = helperTab2[indeks];
                                }
                                crossoverTab[i, j] = a;
                                helperTab[j] = a;
                            }
                            else
                                crossoverTab[i, j] = populationTab[i, j];
                        }
                    }

                    for (int j = 0; j < populationTab.GetLength(1); j++)
                    {
                        if (j == limit1)
                        {
                            j += limit2 - limit1;
                        }
                        else
                        {
                            int b = populationTab[i + 1, j];
                            if (helperTab2.Contains(b))
                            {
                                while (helperTab2.Contains(b))
                                {
                                    int indeks = Array.IndexOf(helperTab2, b);
                                    b = helperTab[indeks];
                                }
                                crossoverTab[i + 1, j] = b;
                                helperTab2[j] = b;
                            }
                            else
                                crossoverTab[i + 1, j] = populationTab[i + 1, j];
                        }
                    }
                }

                else
                {
                    for (int j = 0; j < populationTab.GetLength(1); j++)
                    {
                        crossoverTab[i, j] = populationTab[i, j];
                        crossoverTab[i + 1, j] = populationTab[i + 1, j];
                    }
                }
            }
            return crossoverTab;
        }

        public static int[,] inversionMutate(int[,] populationTab, int mutationRate)
        {
            int[,] mutateTable = new int[population, cityCount];
            int[] chromosoneTab = new int[cityCount];
            Random rnd = new Random();
            int limit1, limit2;
            int randomRate;

            for (int i = 0; i < population; i++)
            {
                randomRate = rnd.Next(1, 100);
                if (randomRate <= mutationRate)
                {
                    limit1 = rnd.Next(cityCount);
                    limit2 = rnd.Next(cityCount);
                    if (limit1 > limit2)
                    {
                        int temp = limit1;
                        limit1 = limit2;
                        limit2 = temp;
                    }

                    for (int j = 0; j < cityCount; j++)
                    {
                        chromosoneTab[j] = populationTab[i, j];
                    }

                    Array.Reverse(chromosoneTab, limit1, limit2 - limit1);

                    for (int j = 0; j < cityCount; j++)
                    {
                        mutateTable[i, j] = chromosoneTab[j];
                    }
                }
                else
                {
                    for (int j = 0; j < cityCount; j++)
                    {
                        mutateTable[i, j] = populationTab[i, j];
                    }
                }
            }

            return mutateTable;
        }

        public static void FirstPopulation(int[,] populationTab)
        {
            for (int i = 0; i < population; i++)
            {
                for (int j = 0; j < cityCount; j++)
                {
                    Console.Write(populationTab[i, j] + "-");
                }
                Console.WriteLine(calucalateFitness(populationTab)[i]);
            }
        }

        public static void ShowResult(int[,] populationTab, int generation, int population, int fitness, int index)
        {
            Console.Clear();
            Console.WriteLine("Generation: {0}", generation);
            Console.WriteLine("Population: {0}", population);
            Console.WriteLine("Fitness: {0}", fitness);
            Console.Write("Best: ");
            for (int i = 0; i < cityCount; i++)
            {
                Console.Write(populationTab[index, i]+"-");
            }
            Console.WriteLine();
        }

        

        static void Main(string[] args)
        {
            void KeyPressed()
            {
                Console.ReadKey();
                    if (!keyPressed)
                        keyPressed = true;
                    else
                        keyPressed = false;
            }
            Thread tr = new Thread(KeyPressed);
            tr.Start();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LoadValuesFromFile(path);
            int[,] tab = randomPopulation();
            int[] fitnessTable = new int[population];
            int[] bestWay = new int[cityCount];
            int min = 0, minglobal = 500000000;
            int index = 0;
            for (int i = 0; i < 1000000; i++)
            {
                tab = tournamentSelection(tab, calucalateFitness(tab), 3);
                tab = crossover(tab, 70);
                tab = inversionMutate(tab, 3);
                fitnessTable = calucalateFitness(tab);
                min = fitnessTable.Min();

                if (min < minglobal)
                {
                    index = Array.IndexOf(fitnessTable, min);
                    minglobal = min;
                    //bestWay = tab[index];
                    for (int k = 0; k < cityCount; k++)
                    {
                        bestWay[k] = tab[index, k];
                    }
                }
                if (i % 1000 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Generation: {0}", i);
                    Console.WriteLine("Population: {0}", population);
                    Console.WriteLine("Fitness: {0}", minglobal);
                    Console.Write("Best: ");
                    for (int j = 0; j < cityCount-1; j++)
                    {
                        Console.Write(bestWay[j] + "-");
                    }
                    Console.WriteLine(bestWay[cityCount-1]);

                    //ShowResult(tab, i, population, minglobal, index);
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    Console.WriteLine("RunTime: " + elapsedTime);
                }
                if (keyPressed)
                {
                    Console.WriteLine("Koniec");
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
