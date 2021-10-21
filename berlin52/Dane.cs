using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace berlin52
{
    public static class Dane
    {
        static private int[,] berlinTable;

        public static void LoadDane ()
        {
            LoadFile();
        }

        private static void LoadFile()
        {
            try
            {
                string currentLine = String.Empty;
                StreamReader sr = new StreamReader(Parm.plikTSP);
                int lineCounter = 0;
                Parm.n = int.Parse(sr.ReadLine());
                berlinTable = new int[Parm.n, Parm.n];

                while ((currentLine = sr.ReadLine()) != null)
                {
                    var singleLine = currentLine.TrimEnd().Split(' ');
                    for (int i = 0; i < singleLine.Length; i++)
                    {
                        berlinTable[lineCounter, i] = int.Parse(singleLine[i]);
                        berlinTable[i, lineCounter] = int.Parse(singleLine[i]);
                    }
                    lineCounter++;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int[,] Berlin52
        {
            get
            {
                try
                {
                    return berlinTable;
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine("aby pobrać tablice Berlin52 załaduj najpierw plik berlin52 metodą LoadFile");
                    return null;
                }
            }
        }

        static public void print()
        {
            for (int i = 0; i < berlinTable.GetLength(0); i++)
            {
                for (int j = 0; j < berlinTable.GetLength(1); j++)
                {
                    if (berlinTable[i, j] == 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(berlinTable[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
