using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zasobnik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Napište Vaše závorky:");
            string zavorkyPomocny = Console.ReadLine(); //už rovnou to je jako list polí... každý znak je jedno pole, můžu k nim přistupovat po indexech
            Console.WriteLine(SpravneZavorkyOtaznik(zavorkyPomocny));

            Console.WriteLine("Napište celé kladné číslo:");
            int promenna = int.Parse(Console.ReadLine());
            RozlozNaSoucty(promenna);

            Console.ReadLine();

        }
        static bool SpravneZavorkyOtaznik(string seznamZavorek)
        {
            Stack<char> stackZavorek = new Stack<char>(); //char je jeden znak v string

            stackZavorek.Push(seznamZavorek[0]);

            for (int i = 1; i < seznamZavorek.Length; i++)
            {
                if (stackZavorek.Count != 0)
                {
                    char prvniVstack = stackZavorek.Peek(); //nutný zkontrlovat, že tam něco je, jinak nejde peek!
                    if (prvniVstack == '(' && seznamZavorek[i] == ')' || prvniVstack == '[' && seznamZavorek[i] == ']' || prvniVstack == '{' && seznamZavorek[i] == '}')
                    {
                        stackZavorek.Pop();
                    }
                }

                else
                {
                    stackZavorek.Push(seznamZavorek[i]);
                }
            }
            if (stackZavorek.Count > 0)
            {
                return false;
            }
            return true;     //gitkraken mi právě smazal půlku mého kódu... 
        }

        static Stack<int> VytvorZasobnik(int pridavamCislo, int kdyKoncit)
        {
            Stack<int> zasobnik = new Stack<int>();
            int suma = 0;

            while (suma + pridavamCislo <= kdyKoncit) //snažím se přidat co nejvíce čísel
            {
                zasobnik.Push(pridavamCislo);
                suma += pridavamCislo;             //suma 0, přidám 2 -> suma 2, zásobník 2, přidám 2 -> suma 4, zásobník 2,2   ... pokud bych chtěla přidat 2, bude to větší než 5 -> konec
            }

            int zbyva = kdyKoncit - suma;   //pokud to nemám celé, takže mi zbývá, doplním to 1
            while (zbyva > 0)
            {
                zasobnik.Push(1);
                zbyva--;
            }

            return zasobnik;
        }

        static void RozlozNaSoucty(int zadaneCislo)
        {
            int k = 1;
            List<string> jedinecneSoucty = new List<string>(); //abych mohla kontrolovat, že nejsou duplikaty

            while (k < zadaneCislo)  //k zvětšuji a "odečítám"
            {
                Stack<int> stackCisla = VytvorZasobnik(k, zadaneCislo); //udělám zásobník, nejprve plný 1, pak třeba 2 a doplněn o 1, ...

                while (stackCisla.Count > 1) //dokud v zásobníku něco bude
                {
                    // Normalizace kombinace
                    var serazeneCisla = stackCisla.OrderBy(x => x).ToList();   //abych vše mohla kontrolovat spolu a neměla otočené duplikáty
                    string Edvard = string.Join(" + ", serazeneCisla);          //Edvard. On ví. Jím budu kontrolovat

                    if (!jedinecneSoucty.Contains(Edvard))  //fuč fuč duplikáty
                    {
                        jedinecneSoucty.Add(Edvard);
                        Console.WriteLine(Edvard);
                    }

                    int a = stackCisla.Pop();   //vždy sečtu první dvě čísla, vrátím a vypíšu a znovu
                    int b = stackCisla.Pop();   //např.: 1, 1, 1, 1, 1 -> 1, 1, 1, 2      //např.: 1, 1, 1, 2 -> 1, 1, 3       //např.: 1, 1, 3 -> 1, 4
                    int vysledek = a + b;

                    stackCisla.Push(vysledek);
                }
                k++;
            }
            Console.WriteLine(zadaneCislo);
        }
    }
}
