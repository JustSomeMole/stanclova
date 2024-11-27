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
        }
        static bool SpravneZavorkyOtaznik(string seznamZavorek)
        {
            Stack<char> stackZavorekOtevrene = new Stack<char>(); //char je jeden znak v string
            Stack<char> stackZavorekZavrene = new Stack<char>();

            while (true)
            {
                if (seznamZavorek[0] == '(' || seznamZavorek[0] == '[' || seznamZavorek[0] == '{')
                {
                    stackZavorekOtevrene.Push(seznamZavorek[0]);
                }
                else if (seznamZavorek[0] == '(' || seznamZavorek[0] == '[' || seznamZavorek[0] == '{')
                {
                    stackZavorekZavrene.Push(seznamZavorek[0]);
                }
                else
                {
                    break;
                }
            }
            while (true)
            { 
            }
            { //porovnat stacky

        }
    }
}
