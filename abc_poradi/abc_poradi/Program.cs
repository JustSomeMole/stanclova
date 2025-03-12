using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_poradi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string prijmuteTezsi = "d dd dha dhff dhfj dc c ca cd cc cg cb cbe cbf cee cek cf cfj cfk cfi";
            //string prijmuteLehci = "d<c d<h h<c a<d c<g a<f f<j g<b e<f b<e e<k j<k k<i c<b b<f";

            //List<char> pismenka = Pismenka(prijmuteLehci);
            List<char> pismenka = Pismenka(prijmuteTezsi);

            //List<string> abeceda = AbecedaLehci(prijmuteLehci);
            List<string> abeceda = AbecedaTezsi(prijmuteTezsi);

            Console.WriteLine(vysledek(abeceda, pismenka));

            Console.ReadLine();
        }


        static List<char> Pismenka(string prijmute)
        {
            List<char> pismenka = new List<char>();
            foreach (char znak in prijmute)
            {
                if ((znak != ' ') && (znak != '<'))
                {
                    if (!pismenka.Contains(znak))
                    {
                        pismenka.Add(znak);
                    }
                }
            }

            return pismenka;
        }


        static string vysledek(List<string> abeceda, List<char> pismenka)
        {
            string vysledek = null;
            while (pismenka.Count > 0)
            {

                List<char> pomocnaPismenka = new List<char>(pismenka);


                char hledanyZnak = nejmensiLexikograficky(abeceda, pomocnaPismenka);

                if (hledanyZnak != ' ')
                {
                    if (vysledek == null)
                    {
                        vysledek += hledanyZnak;
                    }
                    else
                    {
                        vysledek += " -> " + hledanyZnak;
                    }

                    pismenka.Remove(hledanyZnak);

                    List<string> abecedaPomocna = new List<string>();

                    foreach (string slovo in abeceda)
                    {
                        if (slovo[0] == hledanyZnak || slovo[1] == hledanyZnak)
                        {
                        }
                        else
                        {
                            abecedaPomocna.Add(slovo);
                        }
                    }
                    abeceda = abecedaPomocna;
                    //Console.WriteLine(pismenka.Count);
                }

                else { 
                    vysledek = string.Empty;
                    break;
                }
            }
            return vysledek;
        }

        static char nejmensiLexikograficky(List<string> abeceda, List<char> pismenka)
        {
            foreach (string dvojice in abeceda)
            {
                char znak = dvojice[1];
                pismenka.Remove(znak);                
            }

            if (pismenka.Count > 1)
            {
                Console.WriteLine("Nejednoznačné");
                return pismenka[0];
            }
            else if (pismenka.Count == 0)
            {
                Console.WriteLine("Obsahuje cyklus => nelze");
                return ' ';
            }
            else {
                
                return pismenka[0];
            }

        }


        static List<string> AbecedaLehci(string prijmute)
        {
            List<string> list = new List<string>();

            string[] zadani = prijmute.Split(' ');
            foreach (string slovo in zadani)
            {
                string slovoJedna = slovo.Replace("<", "");
                list.Add(slovoJedna);
            }

            return list;
        }


        static List<string> AbecedaTezsi(string prijmute)
        {
            string[] zadani = prijmute.Split(' '); //split na jednotlivá slova

            List<string> list = new List<string>();

            int pocetSlov = zadani.Length;

            for (int i = 0; i < pocetSlov - 1; i++)
            {
                string slovoJedna = zadani[i];
                string slovoDva = zadani[i + 1];

                int j = 0;
                while (j < Math.Min(slovoJedna.Length, slovoDva.Length))
                {
                    if (slovoJedna[j] == slovoDva[j])
                    {
                        j++;
                    }
                    else
                    {
                        string dvojice = "";
                        dvojice += slovoJedna[j];
                        dvojice += slovoDva[j];
                        list.Add(dvojice);

                        break;
                    }
                }
            }
            return list;
        }
    }   
}
