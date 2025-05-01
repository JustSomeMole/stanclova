using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stanclova_aritmeticke_vyrazy
{
    internal class Program //FLOAT U DESETINNÝCH ČÍSEL
                           //TEČKY A ČÁRKY JAKO DESETINNÉ ZNAMÉNKO - solution: TryParse(input, NumberStyles Float, CultureInfo.InvariantCulture, out float result)
    {
        static void Main(string[] args)
        {
            PostfixXPrefix postXprefix = new PostfixXPrefix();
            Rozhodnuti rozhodnuti = new Rozhodnuti();

            string rozhodnutiUzivatele = rozhodnuti.ZiskejRozhodnutí();
            string vstup = rozhodnuti.ZiskejVstup();
            string[] vstupArray = rozhodnuti.CastingSpellSplittt(vstup);

            if (rozhodnutiUzivatele == "1")
            {
                string[] otocenePole = postXprefix.CastingSpellOtoceniPoleee(vstupArray);
                Console.WriteLine(postXprefix.PrefixFunkce(otocenePole));
            }
            else 
            {
                Console.WriteLine(postXprefix.PostfixFunkce(vstupArray));
            }

            Console.ReadLine();
        }
    }



    class Rozhodnuti
    {
        public string ZiskejRozhodnutí() //já to chci zase dávat zpátky do mainu :__((
        {
            Console.WriteLine("Zadejte, v čem zadáte Váš vstup - PREFIX (1) nebo POSTFIX (2): ");

            string volba = Console.ReadLine();
            
            while (volba != "1" && volba != "2")
            {
                Console.WriteLine("Neplatná volba! Prosím zadejte 1 (PREFIX) nebo 2 (POSTFIX): ");
                volba = Console.ReadLine();
            }
            return volba;
        }

        public string ZiskejVstup()
        {
            Console.Clear();
            Console.WriteLine("Nyní zadejte Váš vstup: ");
            return Console.ReadLine();
        }

        public string[] CastingSpellSplittt(string vstup)
        {
            Console.Clear();
            return vstup.Split(' ');
        }
    }



    class PostfixXPrefix
    {
        public string[] CastingSpellOtoceniPoleee(string[] originalniVstup) //kvůli prefixu
        {
            string[] otocenyVstup = originalniVstup.Reverse().ToArray();
            return otocenyVstup;
        }

        //float? dělá, že to někdy nemusí vrátit float... třeba null
        public float? PostfixFunkce(string[] vstupArray) //Float.TryParse(cislo, out jmenoDoCehoToDam)
                                                         //Int.TryParse
        {
            Stack<float> zasobnik = new Stack<float>();

            float operand;
            float vyndanyJedna;
            float vyndanyDva;
            float vysledek;

            for (int i = 0; i < vstupArray.Length; i++)
            {
                switch (vstupArray[i])
                {

                    case "+":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva + vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    case "-":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva - vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    case "*":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva * vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    case "/":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        if (vyndanyJedna == 0)
                        {
                            Console.WriteLine("Nuh uh, neděl nulou. Tytyty.");
                            return null;
                        }

                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva / vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    default:
                        if (!float.TryParse(vstupArray[i], NumberStyles.Float, CultureInfo.InvariantCulture, out operand)) //zkusím jestli to jde
                        {
                            Console.WriteLine($"Neplatný operand: {vstupArray[i]}");
                            return null;
                        }
                        zasobnik.Push(operand);
                        break;
                }
            }

            int soucetZbylych = zasobnik.Count();
            switch (soucetZbylych)
            {
                case 1:
                    return zasobnik.Peek();
                case 0:
                    Console.WriteLine("Něco je hodně špatně, prázdný zásobník.");
                    return null;
                default:
                    Console.WriteLine("Někde jsi udělal chybu, chybí ti znaménko.");
                    return null;
            }
        }

        public float? PrefixFunkce(string[] vstupArray)
        {
            Stack<float> zasobnik = new Stack<float>();

            float operand;
            float vyndanyJedna;
            float vyndanyDva;
            float vysledek;

            for (int i = 0; i < vstupArray.Length; i++)
            {
                switch (vstupArray[i])
                {
                    case "+":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva + vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    case "-":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyJedna - vyndanyDva;
                        zasobnik.Push(vysledek);
                        break;

                    case "*":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        vysledek = vyndanyDva * vyndanyJedna;
                        zasobnik.Push(vysledek);
                        break;

                    case "/":
                        if (zasobnik.Count < 2)
                        {
                            Console.WriteLine("Neplatný výraz: chybí operand/y");
                            return null;
                        }
                        vyndanyJedna = zasobnik.Pop();
                        vyndanyDva = zasobnik.Pop();
                        if (vyndanyDva == 0)
                        {
                            Console.WriteLine("Nuh uh, neděl nulou. Tytyty.");
                            return null;
                        }   
                        vysledek = vyndanyJedna / vyndanyDva;
                        zasobnik.Push(vysledek);
                        break;

                    default:
                        if (!float.TryParse(vstupArray[i], NumberStyles.Float, CultureInfo.InvariantCulture, out operand)) //zkusím jestli to jde
                        {
                            Console.WriteLine($"Neplatný operand: {vstupArray[i]}");
                            return null;
                        }
                        zasobnik.Push(operand);
                        break;
                }
            }

            int soucetZbylych = zasobnik.Count();
            switch (soucetZbylych)
            {
                case 1:
                    return zasobnik.Peek();
                case 0:
                    Console.WriteLine("Něco je hodně špatně, prázdný zásobník.");
                    return null;
                default:
                    Console.WriteLine("Někde jsi udělal chybu, chybí ti znaménko.");
                    return null;
            }
        }
    }
}
