using System;
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
            Console.WriteLine("Napište Vaše závorky");
            string zavorkyPomocny = Console.ReadLine(); //už rovnou to je jako list polí... každý znak je jedno pole, můžu k nim přistupovat po indexech
            ConsoleWriteLine(SpravneZavorkyOtaznik(zavorkyPomocny));
        }
        static bool SpravneZavorkyOtaznik(string seznamZavorek)
        {
            Stack<char> stackZavorek = new Stack<char>(); //char je jeden znak v string  
            for (int i = 1; i < seznamZavorek.Length; i++)
            {
                
                char prvniVstack = stackZavorek.Peek(); //nutný zkontrlovat, že tam něco je, jinak nejde peek!!!!!
                
                if (prvniVstack == '(' && seznamZavorek[i] == ')' || prvniVstack == '[' && seznamZavorek[i] == ']' || prvniVstack == '{' && seznamZavorek[i] == '}')
                {
                    stackZavorek.Pop();
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
            return true;     
        }
    }
}
