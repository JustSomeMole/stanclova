using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stanclova_vyrazovy_strom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PracujiciStrom pracujici = new PracujiciStrom();
            
            Uzel<string> stromek = pracujici.VytvorStromZeVstupu();

            //Console.WriteLine(stromek.Hodnota);
            if (stromek != null)
            {
                Console.Write("Prefix: ");
                pracujici.VypisPrefix(stromek);
                Console.WriteLine();
                Console.Write("Infix: ");
                pracujici.VypisInfix(stromek);
                Console.WriteLine();
                Console.Write("Postfix: ");
                pracujici.VypisSufix(stromek);
            }

            Console.ReadLine();

            //8 6 2 + / 2 -
            //2 3 1 * - 9 + 
            //65 3 5 * - 2 3 + /
        }
    }


    class Uzel<T>
    {
        public string Hodnota { get; set; }
        public Uzel(string hodnota)
        {
            Hodnota = hodnota;
            Levy = null; Pravy = null;
        }
        public Uzel<T> Levy { get; set; }
        public Uzel<T> Pravy{ get; set; }
    }


    class PracujiciStrom
    {
        private string[] ZiskejVstup()
        {
            Console.Clear();
            Console.WriteLine("Nyní zadejte Váš vstup: ");
            string vstup = Console.ReadLine();
            string[] vstupArray = vstup.Split(' ');

            return vstupArray;
        }

        public Uzel<string> VytvorStromZeVstupu()
        {
            Stack<Uzel<string>> zasobnik = new Stack<Uzel<string>>();
            string znak;
            Uzel<string> uzlik;
            //Strom<T> stromecek;
            Uzel<string> uzlikPravy;
            Uzel<string> uzlikLevy;
            string[] vstupArray = ZiskejVstup();


            for (int i = 0; i < vstupArray.Length; i++)
            {
                znak = vstupArray[i];

                switch (znak)
                {
                    case "+": //je nám jedno, co ot je, takže to přeskočíme
                    case "-":
                    case "*":
                    case "/":
                        if (zasobnik.Count == 0)
                        {
                            break;
                        }
                        uzlikPravy = zasobnik.Pop();
                        if (zasobnik.Count == 0)
                        {
                            break;
                        }
                        uzlikLevy = zasobnik.Pop();

                        uzlik = new Uzel<string>(znak);

                        uzlik.Pravy = uzlikPravy;
                        uzlik.Levy = uzlikLevy;

                        zasobnik.Push(uzlik);
                        break;

                    default:
                        uzlik = new Uzel<string>(znak);
                        //stromecek = new Strom<T>(uzlik); 
                        
                        zasobnik.Push(uzlik);
                        break;
                }
            }

            switch (zasobnik.Count())
            {
                case 1:
                    return zasobnik.Pop();
                case 0:
                    Console.WriteLine("Něco je hodně špatně, chybí číslo.");
                    return null;
                default:
                    Console.WriteLine("Někde jsi udělal chybu, chybí ti znaménko.");
                    return null;
            }

        }

        public void VypisInfix(Uzel<string> uzlik)
        {
            if (uzlik.Levy != null)
            {
                if (uzlik.Hodnota == "+" || uzlik.Hodnota == "-")
                {
                    Console.Write("(");
                }
                VypisInfix(uzlik.Levy);
            }
            Console.Write(uzlik.Hodnota);
            if (uzlik.Pravy != null)
            {
                VypisInfix(uzlik.Pravy);
                if (uzlik.Hodnota == "+" || uzlik.Hodnota == "-")
                {
                    Console.Write(")");
                }
            }
        }

        public void VypisSufix(Uzel<string> uzlik)
        {
            if (uzlik.Levy != null)
            {
                VypisSufix(uzlik.Levy);
            }

            if (uzlik.Pravy != null)
            {
                VypisSufix(uzlik.Pravy);
            }

            Console.Write(" " + uzlik.Hodnota + " ");
        }

        public void VypisPrefix(Uzel<string> uzlik)
        {
            Console.Write(" " + uzlik.Hodnota + " ");
            if (uzlik.Levy != null)
            {
                VypisPrefix(uzlik.Levy);
            }
            
            if (uzlik.Pravy != null)
            {
                VypisPrefix(uzlik.Pravy);
            }
        }
    }
}
