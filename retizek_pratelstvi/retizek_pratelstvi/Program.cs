using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace retizek_pratelstvi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kolikpak máme lidí k prohledání? ");
            int PocetLidi = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Nyní je nutné zadat probíhající přátelské vztahy mezi subjekty (příklad zapsání: 1-4 2-3 3-4): ");
            string[] Vztahy = Console.ReadLine().Split();

            Console.WriteLine("Mezi kým hledáš řetízek pln přátelství (příklad zapsání: 1 5)? ");
            string Hledani = Console.ReadLine();
            
        }
        static void HujakasaVytvorList(string[] VlozenaCislaString) //[1-2, 3-4, 1-3] --> {[1, 2], [3, 4], [1, 3]}
        {
            int[] //converttoint32
        }
    }  

    class Kamarad
    {
        public Kamarad(int jmeno) //každý uyel bude mít: název
        {
            Jmeno = jmeno;
        }
        public int Jmeno { get; set; }

    }
}
