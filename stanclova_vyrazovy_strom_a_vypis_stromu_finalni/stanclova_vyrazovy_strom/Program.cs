using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
                Console.WriteLine("Výpis stromu:");
                Console.WriteLine();
                string[,] matice = VypisStromu.StromNaMatici(stromek);
                VypisStromu.VypisMatice(matice);

                Console.WriteLine();

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


    public class Uzel<T>
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

    //------------------VÝPIS STROMU------------------\\

    public static class VypisStromu
    {
        public static int VyskaStromu(Uzel<string> koren)
        {
            if (koren == null)
            {
                return -1;
            }

            int levaVyska = VyskaStromu(koren.Levy);
            int pravaVyska = VyskaStromu(koren.Pravy);

            return Math.Max(levaVyska, pravaVyska) + 1; //vrátí větší hodnotu z těch dvou
        }

        public static void PruchodInOrder(Uzel<string> koren, int radek, int sloupec, int vyska, string[,] matice)
        {
            if (koren == null)
            {
                return;
            }

            //výpočet offsetu
            int offset = (int)Math.Round(Math.Pow(2, vyska - radek - 1)); //Round to zaokrouhlí, Pow je mocnina ... pomůže vykreslit strom, o kterou má být potomek vykreslen

            for (int i = 0; i < offset; i++)
            {
                if (koren.Levy != null)
                {
                    matice[radek, sloupec - i] = "---";
                }
                if (koren.Pravy != null)
                {
                    matice[radek, sloupec + i] = "---";
                }
            }

            if (koren.Levy != null)
            {
                matice[radek, sloupec - offset] = " ┌-";
            }
            if (koren.Pravy != null)
            {
                matice[radek, sloupec + offset] = "-┐ ";
            }

            //průchod levým podstromem
            if (koren.Levy != null)
            {
                PruchodInOrder(koren.Levy, radek + 1, sloupec - offset, vyska, matice);
            }

            //hodnota ve stromu se uloží na spravné místo do matice
            if(koren.Levy == null || koren.Pravy == null)
            {
                switch((koren.Hodnota).Length)
                {
                    case 1:
                        matice[radek, sloupec] = " " + koren.Hodnota + " ";
                        break;
                    case 2:
                        matice[radek, sloupec] = " " + koren.Hodnota;
                        break;
                    default:
                        matice[radek, sloupec] = koren.Hodnota;
                        break;
                }
            }
            else
            {
                matice[radek, sloupec] = "[" + koren.Hodnota + "]";
            }

            //průchod pravým podstromem
            if (koren.Pravy != null)
            {
                PruchodInOrder(koren.Pravy, radek + 1, sloupec + offset, vyska, matice);
            }
        }

        public static string[,] StromNaMatici(Uzel<string> koren)
        {
            int vyska = VyskaStromu(koren);
            int radek = vyska + 1;
            int sloupec = (int)Math.Pow(2, vyska + 1) - 1;

            var matice = new string[radek, sloupec]; //nová matice s rozměry radek a sloupec, var odvodí co to bude za proměnnou

            //inicializace matice
            for (int i = 0; i < radek; i++)
            {
                for (int j = 0; j < sloupec; j++)
                {
                    matice[i, j] = "   ";
                }
            }

            //průchod stromu a naplnění matice
            PruchodInOrder(koren, 0, (int)Math.Floor((double)(sloupec - 1) / 2), vyska, matice);

            return matice;
        }

        public static void VypisMatice(string[,] matice)
        {
            int radek = matice.GetLength(0);
            int sloupec = matice.GetLength(1); //1 zjistí sloupce v matici

            for (int i = 0; i < radek; i++)
            {
                for (int j = 0; j < sloupec; j++)
                {
                    if (matice[i, j] == null)
                    {
                        Console.WriteLine("   ");
                    }
                    else
                    {
                        Console.Write(matice[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }

    //------------------VÝPIS STROMU------------------\\


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
