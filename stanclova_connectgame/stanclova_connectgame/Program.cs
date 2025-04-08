using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Media;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
//using namespace;

namespace ConnectGame
{
    internal class Program  
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Vítejte ve hře Propadávacích piškvorek!");
            Console.WriteLine("Pro spuštění stiskněte libovolnou klávesu.");

            Console.ReadKey(); //počká na stisknutí jakýkoliv klávesnice, pak to vymažu

            Console.Clear();   

            Console.WriteLine("Zvolte si libovolnou šířku Vašeho pole: ");
            int sirkaPole = int.Parse(Console.ReadLine());

            Console.WriteLine("Zvolte si libovolnou výšku Vašeho pole: ");
            int vyskaPole = int.Parse(Console.ReadLine());

            Console.WriteLine("Zvolte si kolik bude hráč muset spojit žetonů, aby vyhrál: ");
            int pocetVyhernichZetonu = int.Parse(Console.ReadLine());

            Console.WriteLine("Zvolte si kolik hráčů bude hrát (možnosti: 2 až 4 hráři):");
            int pocetHracu = int.Parse(Console.ReadLine());



            Hra hra = new Hra(pocetVyhernichZetonu, sirkaPole, vyskaPole, pocetHracu);
            hra.Play();

            Console.WriteLine("Díky za hraní Propadávacích piškvorek, tak příště!"); //dokud poběží Play, tak se to nenapíše, pak jo
            Console.ReadLine();
        }
    }





    //-------PRIVÁTNÍ (kromě Play, .... myslím)-------





    class Hra
    {
        private int[,] hraciPole; //pro tuhle classu inicializuju tuto proměnnou
        private Hrac[] hraci;
        private int hracNaTahuIndex;
        private int pocetVyhernichZetonu;
        private int SoucetTahu;
        private int maxTahu;

        public Hra(int pocetVyhernichZetonu, int sirkaPole, int vyskaPole, int pocetHracu) //konstruktor
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu;
            hraciPole = new int[sirkaPole, vyskaPole];
            hraci = new Hrac[pocetHracu];
            maxTahu = vyskaPole * sirkaPole; //max, pak zaplněno
            SoucetTahu = 0;
            hracNaTahuIndex = 0;

            NainicalizujHrace();
        }
        //nastavím a připravím hráče
        private void NainicalizujHrace()
        {
            string[] symboly = { "X", "O", "#", "@" };
            for (int i = 0; i < hraci.Length; i++)
            {
                hraci[i] = new Hrac($"Hráč {i + 1}", symboly[i]);
            }
        }


        //-------ZAPNUTÍ HRY-------


        public void Play()
        {
            bool konecHry = false;
            Hrac vyhral = null;

            while (konecHry != true)
            {
                ZobrazPole();


                Hrac hracNaTahu = hraci[hracNaTahuIndex]; //změním hráče na hráče na tahu

                Console.WriteLine($"{hracNaTahu.Jmeno}, zadejte sloupec, do kterého chcete dát žeton: ");

                int sloupec = ZadejPlatnySloupec();
                int radek = VhozeniZetonu(sloupec - 1, hracNaTahuIndex + 1); //na jaký řádek žeton spadl

                int[] soucasnaPozice = new int[] { radek, sloupec - 1 };

                if (Check(hraciPole, new int[] { radek, sloupec - 1 }, hracNaTahuIndex + 1, pocetVyhernichZetonu))
                {
                    konecHry = true;
                    vyhral = hracNaTahu;
                }
                else if (++SoucetTahu >= maxTahu)
                {
                    konecHry = true;
                }
                else
                {
                    hracNaTahuIndex = (hracNaTahuIndex + 1) % hraci.Length;
                }

            }
            ZobrazPole();
            if (vyhral != null)
            {
                Console.WriteLine($"Gratulujeme! {vyhral.Jmeno} vyhrál!");
            }
            else
            {
                Console.WriteLine("Remíza! Hrací pole je plné.");
            }

        }


        //-------POMOCNÉ FUNKCE-------

        private int ZadejPlatnySloupec()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int sloupec)) //funkce se pokusí předělat text na celé číslo a když se podaří tak h uloží ho do sloupce
                {
                    if (sloupec >= 1 && sloupec <= hraciPole.GetLength(1)) //jsme v sloupcích od 1 do max poctu/okraj
                    {
                        if (hraciPole[0, sloupec - 1] == 0) //uživatel dává od 1, pole indexováno od 0
                                                            //jestli tam nic není
                        {
                            return sloupec; //vrátím ho protože je ok
                        }
                    }
                    Console.WriteLine("Sloupec už je plný.");
                }
                else
                {
                    Console.WriteLine("Neplatný vstup.");
                }
            }
        }


        private int VhozeniZetonu(int sloupec, int hracID) 
        {                                                                     //getlenght(0) mi vrátí pocet rádku, takže výsku
            for (int radek = hraciPole.GetLength(0) - 1; radek >= 0; radek--) //začnu odspodu - musím odečíst 1, abych měla řádek
                                                                              //jdu nahoru, odspodu - odečítám
            {
                if (hraciPole[radek, sloupec] == 0)
                {
                    hraciPole[radek, sloupec] = hracID;
                    return radek;
                }
            }
            return -1; //mělo by se vždy podařit, protože jsem checkovala jestli tam něco není a jde to vhodit

        }


        private void ZobrazPole()
        {
            Console.Clear();

            //vypíšu si číslíčka nad sloupci
            for (int i = 0; i < hraciPole.GetLength(1); i++) //getlenght funguje ve vícerozměrných polích
                                                             //... třeba to co mám, dá mi zpět délku sloupce
                                                             //... (0) by byly řádky
            {
                Console.Write($" {i + 1} ");
            }
            Console.WriteLine();

            //projdu řádky
            for (int i = 0; i < hraciPole.GetLength(0); i++)                        
            {                                                                       
                Console.Write("|");
                //peojdu sloupce                                                    
                for (int j = 0; j < hraciPole.GetLength(1); j++)                    
                {
                    int pole = hraciPole[i, j];

                    if (pole == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (pole >= 1)
                    {
                        Console.Write($"{hraci[pole - 1].Symbol}|");
                    }
                }
                Console.WriteLine();
            }
            //dolejšek
            for (int i = 0; i < hraciPole.GetLength(1); i++)
            {
                Console.Write("---");
            }
            Console.WriteLine();
        }



        //---------CHECK VÝHRY---------


        public bool Check(int[,] board, int[] soucasnaPozice, int hrac, int pocetKamenuNaVyhru)
        {
            int radek = soucasnaPozice[0];
            int sloupec = soucasnaPozice[1];
            return CheckColumn(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckRow(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckDiag(board, radek, sloupec, hrac, pocetKamenuNaVyhru);
        }

        bool CheckColumn(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 0;
            int pocetRadku = gameField.GetLength(0);

            for (int i = radek; i < pocetRadku; i++)
            {
                if (gameField[i, sloupec] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }
        bool CheckRow(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = sloupec + 1; i < pocetSloupcu; i++)
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }


            for (int i = sloupec - 1; i >= 0; i--)
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }
        bool CheckDiag(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetRadku = gameField.GetLength(0);
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = 1; radek + i < pocetRadku && sloupec + i < pocetSloupcu; i++)
            {
                if (gameField[radek + i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek - i >= 0 && sloupec - i >= 0; i++)
            {
                if (gameField[radek - i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }


            pocet = 1;


            for (int i = 1; radek - i >= 0 && sloupec + i < pocetSloupcu; i++)
            {
                if (gameField[radek - i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek + i < pocetRadku && sloupec - i >= 0; i++)
            {
                if (gameField[radek + i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

    }


    //--------JINÉ TRÍDY--------


    class Hrac
    {
        public string Jmeno { get; } //public abz to bylo vidět
        public string Symbol { get; }

        public Hrac(string jmeno, string symbol)
        {
            Jmeno = jmeno;
            Symbol = symbol;
        }
    }
}