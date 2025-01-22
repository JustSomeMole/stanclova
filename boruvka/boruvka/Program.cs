using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boruvka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] maticeHran = {
            {0, 1, int.MaxValue, 3, int.MaxValue, int.MaxValue},
            {1, 0, 3, 2, int.MaxValue, 4},
            {int.MaxValue, 3, 0, int.MaxValue, 4, 4},
            {3, 2, int.MaxValue, 0, 3, int.MaxValue},
            {int.MaxValue, int.MaxValue, 4, 3, 0, 2},
            {int.MaxValue, 4, 4, int.MaxValue, 2, 0}     };

            int pocetMest = maticeHran.GetLength(0); //pocet mest (vrcholů)

            int[] spojeni = new int[pocetMest]; // sleduji spojení ... (0,1,2,3,4) -> spojení 0,2 -> (0,1,0,3,4) ... až vše 0, tak vše v jedné skupině/komponentě
            for (int i = 0; i < pocetMest; i++) spojeni[i] = i;

            int[,] pouziteCesty = new int[pocetMest, pocetMest]; //jaké hrnay jsem použila

            //-------------------------------BORŮVKA-------------------------------\\
            while (spojeni.Distinct().Count() > 1) //prave dokud nemam same 0 -> Distinct odstrani duplikaty -> zbyde jen jedna 0
            {
                NajdiNejlevnejsiCesty(maticeHran, pouziteCesty, spojeni); //nejlevnjší cesta pro každou skupinu

                SpojMesta(pouziteCesty, spojeni); //spojení měst podle nalezených nejlevnějších cest
            }
        }

        static void NajdiNejlevnejsiCesty(int[,] maticeHran, int[,] pouziteCesty, int[] spojeni)
        {
            int pocetMest = maticeHran.GetLength(0);

            int[] nejlevnejsiCesty = new int[pocetMest]; //udělám array 
            for (int i = 0; i < pocetMest; i++) nejlevnejsiCesty[i] = -1; //přidám samé -1

            for (int i = 0; i < pocetMest; i++)
            {
                for (int j = 0; j < pocetMest; j++)
                {
                    if (spojeni[i] != spojeni[j] && maticeHran[i, j] < int.MaxValue) //pokud neni to same a hodnota je menší než nejmenší dosud nalezena = našla jsem menší
                    {
                        if (nejlevnejsiCesty[spojeni[i]] == -1 || maticeHran[i, j] < maticeHran[i, nejlevnejsiCesty[spojeni[i]]] ) //nejlevnejsiCesty[spojeni[i]] == -1 ... pokud jsme pro skupinu/komponentu nenašli zatim nejlevnejsi cestu
                                                                                                                                   //to druhe = pokud nejlevnějši hrana existuje -> porovnam ji s aktualni ... 
                        {
                            nejlevnejsiCesty[spojeni[i]] = j; //prepisu pro dnaou skupinu nejkratsi cestu
                        }
                    }
                }
            }

            for (int i = 0; i < pocetMest; i++) //zaznamenej vybrané nejlevnější cesty
            {
                if (nejlevnejsiCesty[i] != -1) //pokud jse už našla nejmenši cestu
                {
                    int j = nejlevnejsiCesty[i]; //zaznamenam
                    pouziteCesty[i, j] = 1; //zaznamenam do použitých cest - je to osove soumerne, proto na obou
                    pouziteCesty[j, i] = 1;
                }
            }
        }

        static void SpojMesta(int[,] pouziteCesty, int[] spojeni)
        {
            int pocetMest = spojeni.Length; //pokud spojime, tak se snizi pocet, protoze se spoji do skupiny a ta je jako jedna

            for (int i = 0; i < pocetMest; i++)
            {
                for (int j = 0; j < pocetMest; j++)
                {
                    if (pouziteCesty[i, j] == 1 && spojeni[i] != spojeni[j]) //v matici hrana a ve spojeni ne to same cislo -> (0,1,2) -> [0][1] -> 0 a 1
                    {
                        int staraSkupina = spojeni[j]; //1
                        int novaSkupina = spojeni[i]; //0

                        for (int k = 0; k < pocetMest; k++) //prepisu to, aby byly skupiny ... mám (0,1,2,3) ... sS=1; nS=0
                        {
                            if (spojeni[k] == staraSkupina) //až k=1 ... k=sS -> (0,0,2,3)
                                spojeni[k] = novaSkupina;
                        }
                    }
                }
            }
        }
    }
}