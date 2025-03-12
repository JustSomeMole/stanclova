using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mergesort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> cisla = new List<int> { 5, 10, 2, 6, 3, 20, 12, 12, 9, 1 };

            foreach (int i in cisla)
            {
                Console.WriteLine(i);
            }

            List<int> setrizeneCisla = MergeSort(cisla);

            Console.WriteLine("---");

            foreach (int i in setrizeneCisla)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }




        static List<int> MergeSort(List<int> cisla)
        {
            if (cisla.Count == 1)
            {
                return cisla;
            }
            else
            {
                int stred = cisla.Count / 2;

                List<int> cislaPoleJedna = new List<int>();
                List<int> cislaPoleDva = new List<int>();

                for (int x = 0; x < cisla.Count; x++)
                {
                    if (x < stred)
                    {
                        cislaPoleJedna.Add(cisla[x]);
                    }
                    else
                    {
                        cislaPoleDva.Add(cisla[x]);
                    }
                }

                
                List<int> sortedJedna = new List<int>(); 
                sortedJedna = MergeSort(cislaPoleJedna);

                List<int> sortedDva = new List<int>();
                sortedDva = MergeSort(cislaPoleDva);

                int i = 0;
                int j = 0;

                List<int> vysledek = new List<int>();

                while (i < sortedJedna.Count || j < sortedDva.Count)
                {
                    if (i == sortedJedna.Count) //jsme na konci, takže přidáváme pole dva
                    {
                        vysledek.Add(sortedDva[j]);
                        j++;
                    }
                    else if (j == sortedDva.Count)
                    {
                        vysledek.Add(sortedJedna[i]);
                        i++;
                    }
                    else if (sortedJedna[i] < sortedDva[j])
                    {
                        vysledek.Add(sortedJedna[i]);
                        i++;
                    }
                    else 
                    {
                        vysledek.Add(sortedDva[j]);
                        j++;
                    }
                }

                return vysledek;
            }
        }
    }
}
