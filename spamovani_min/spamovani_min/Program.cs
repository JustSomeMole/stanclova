using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spamovani_min
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadejte první řádek matice a čísla oddělte mezerou (pokud student nemá na jiného kontakt, napište -1):");
            string prvniRadek = Console.ReadLine();

            string[] prvniRadekCisla = prvniRadek.Split(' '); //string[] ... prvniRadekCisla = {1, 2, 3} ... split pomocí mezer 

            int velikost = prvniRadekCisla.Length; //jak je velká matice - jak se má vytvořit velká

            int[,] matice = new int[velikost, velikost]; //nemám ráda matice ... int[,]
            int[,] maticeCesty = new int[velikost, velikost]; //ještě další amtice pro cestu

            for (int i = 0; i < velikost; i++) //i zvětšuji index
            {
                matice[0, i] = int.Parse(prvniRadekCisla[i]); //parse převede věci na int
                maticeCesty[0, i] = 0;
            }

            for (int i = 1; i < velikost; i++) //daaalši radky here we go
            {
                Console.WriteLine("Zadejte další řádek matice a čísla oddělte mezerou (pokud student nemá na jiného kontakt, napište -1):");
                string radek = Console.ReadLine();

                string[] cislicka = radek.Split(' ');

                for (int j = 0; j < velikost; j++)
                {
                    matice[i, j] = int.Parse(cislicka[j]);
                    maticeCesty[i, j] = 0;
                }
            }

            Console.WriteLine("Nyní napište jména (oddělte je ';')");
            string jmenaNeco = Console.ReadLine();

            string[] jmena = jmenaNeco.Split(';');

            Console.WriteLine("Jako poslední napište, od kterého študáka začínáme:");
            string pocatecniJmeno = Console.ReadLine();

            int pocatecniPrvek = 0;

            for (int i = 0; i < jmena.Length; i++)
            {
                if (jmena[i] == pocatecniJmeno)
                {
                    pocatecniPrvek = i;
                }
            }

            
            Console.WriteLine("Načtená matice:");
            for (int i = 0; i < velikost; i++)
            {
                for (int j = 0; j < velikost; j++)
                {
                    Console.Write(matice[i, j] + " ");
                }
                Console.WriteLine();
            }
            
            
            int pocetPrvku = velikost;

            Console.WriteLine("Vypsaná matice:");
            int[] vysledek = DijsktruvAlgoritmus(pocetPrvku, pocatecniPrvek, matice, jmena, maticeCesty);
            /*
            foreach (var item in vysledek)
            {
                Console.WriteLine(item.ToString());
            }
            */
            for (int i = 0; i < vysledek.Length; i++)
            {
                if (vysledek[i] > -1)
                {
                    maticeCesty[vysledek[i], i] = 1;                    
                }
            }

            for (int i = 0; i < pocetPrvku; i++)
            {
                for (int j = 0; j < pocetPrvku; j++)
                {
                    Console.Write(maticeCesty[i, j] + " ");
                }
                Console.WriteLine();
            }
            

            Console.WriteLine("\nStiskněte libovolnou klávesu pro ukončení programu...");
            Console.ReadKey();
        }
        
        static int[] DijsktruvAlgoritmus(int pocetPrvku, int pocatecniPrvek, int[,] maticeDelek, string[] jmena, int[,] maticeCesta) //bude vracet tu matici cesty
        {
            int[] cestaDelka = new int[pocetPrvku]; //jaká je vzdálenost nejkratší od počátku
            for (int i = 0; i < pocetPrvku; i++)
            {
                cestaDelka[i] = int.MaxValue;
            }
            cestaDelka[pocatecniPrvek] = 0;

            int[] cestaPrvky = new int[pocetPrvku]; //předchudce prvku - z toho cesta
            for (int i = 0; i < pocetPrvku; i++)
            {
                cestaPrvky[i] = -1;
            }

            int[] zpracovanePrvky = new int[pocetPrvku]; //pole, kde bude bud 1 = zpracované nebo 0 = nezpracované
            for (int i = 0; i < pocetPrvku; i++)
            {
                zpracovanePrvky[i] = 0;
            }
            //zpracovanePrvky[pocatecniPrvek] = 1; //počateční prvek je zpracovaný

            //hlavní část
            int pocetnezpracovanychprvku = KolikNezpracovanychPrvku(zpracovanePrvky);

            while (pocetnezpracovanychprvku > 0) //dokud mám co zpracovávat
            {
                int indexPrvek = NajdiMin(zpracovanePrvky, cestaDelka); //začneme zpracovavta
                zpracovanePrvky[indexPrvek] = 1; //je zpracovan

                for (int i = 0; i < pocetPrvku; i++)
                {
                    if (zpracovanePrvky[i] == 0 && maticeDelek[indexPrvek, i] > -1) //je nezpracovany prvek a vede hrana - z indexprvek do i
                    {
                        if (cestaDelka[indexPrvek] + maticeDelek[indexPrvek, i] < cestaDelka[i])
                        {
                            cestaDelka[i] = cestaDelka[indexPrvek] + maticeDelek[indexPrvek, i];
                            cestaPrvky[i] = indexPrvek;
                        }
                    }
                }
                pocetnezpracovanychprvku = KolikNezpracovanychPrvku(zpracovanePrvky);

            }


            return cestaPrvky;
        }

        static int KolikNezpracovanychPrvku(int[] zpracovanePrvky)
        {
            int pocet = 0;
            for (int i = 0; i < zpracovanePrvky.Length; i++)
            {
                if (zpracovanePrvky[i] == 0)
                {
                    pocet++;
                }
            }
            return pocet;
        }

        static int NajdiMin(int[] zpracovanePrvky, int[] cestaDelka)
        {
            int min = int.MaxValue;
            int indexMin = -1;

            for (int i = 0; i < cestaDelka.Length; i++)
            {
                if (zpracovanePrvky[i] == 0 && cestaDelka[i] < min) //je nezpracovan
                {
                    min = cestaDelka[i];
                    indexMin = i;
                }
            }

            if (indexMin == -1)
            {
                for (int i = 0; i < cestaDelka.Length; i++)
                {
                    if (zpracovanePrvky[i] == 0 && cestaDelka[i] <= min) //je nezpracovan
                    {
                        min = cestaDelka[i];
                        indexMin = i;
                    }
                }
            }
            return indexMin;
        }

        
    }
}
