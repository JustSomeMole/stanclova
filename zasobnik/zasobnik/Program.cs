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

        static void RozlozNaSoucty(int zadaneCislo) //budu vracet seznamy v seznamech
        {
            Stack<int> stackCisla = new Stack<int>();

            for (int i = 1; i <= zadaneCislo; i++) //udělám si zásobník plný 1 ... dle zadaného čísla ... např.: mám 5, proto 1, 1, 1, 1, 1
            {
                stackCisla.Push(1);
            }

            Console.WriteLine(string.Join(" + ", stackCisla.Reverse()));  //toto není vůbec hezké a měla bych to spíš přidat do seznamů a ty pak vypsat...

            while (true) //vždy sečtu první dvě čísla, vrátím a vypíšu a znovu
            {
                if (stackCisla.Count != 1)
                {
                    int a = stackCisla.Pop();                                           //např.: 1, 1, 1, 1, 1 -> 1, 1, 1, 2
                    int b = stackCisla.Pop();                                           //např.: 1, 1, 1, 2 -> 1, 1, 3
                    int vysledek = a + b;                                               //např.: 1, 1, 3 -> 1, 4
                    stackCisla.Push(vysledek);                                          //např.: 1, 4 -> 5
                    Console.WriteLine(string.Join(" + ", stackCisla.Reverse())); //reverse otočí zásobník, string join to spojí do 1 řetězce a dá mezi to +
                }

                else
                {
                    break;  
                }
            }
        }
    }
}
