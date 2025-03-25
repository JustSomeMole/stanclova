using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectgame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] board = new int[6, 7]{
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 2, 1, 0 },
                { 2, 2, 2, 2, 2, 1, 0 }
            };
            int[] position = { 5, 0 };
            // pozice řádek s indexem 5
            // pozice sloupec s indexem 0
                
            int hrac = 2;
            int pocetKamenuNaVyhru = 5;
            

            Console.WriteLine(Check(board, position, hrac, pocetKamenuNaVyhru));
            Console.ReadLine();

        }

        static bool Check(int[,] board, int[] soucasnaPozice, int hrac, int pocetKamenuNaVyhru)
        {
           int radek = soucasnaPozice[0];
           int sloupec = soucasnaPozice[1];
           return CheckColumn(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckRow(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckDiag(board, radek, sloupec, hrac, pocetKamenuNaVyhru);
        }

        static bool CheckColumn(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 0;
            int pocetRadku = gameField.GetLength(0);

            for (int i = radek; i < pocetRadku; i++) //protože jsou to padaci piskvorky, tak nepůjdu nahoru proti gravitaci, nelevitují
            {
                if (gameField[i, sloupec] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin) //kdyby náhodou
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

        static bool CheckRow(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1; //začnu od jedné, protože aktuální kámen bude vždy ten toho hráče, komu to kontrluji
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

            // vlevo vbok!
            for (int i = sloupec - 1; i >= 0 ; i--) //odečtu tam 1, protože už jsem ji počítala do součtu v prnvím cyklu
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

        static bool CheckDiag(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetRadku = gameField.GetLength(0);
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = 1; radek + i < pocetRadku && sloupec + i < pocetSloupcu; i++) //diagonála \ dolu; pricitam k radku i sloupci
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

            for (int i = 1; radek - i >= 0 && sloupec - i >= 0; i++) //diagonála \ nahoru; odecitam od ksloupce i radku
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


            pocet = 1; //resetování počtu abych si to neměnila pri pruchodu jednou a pak druhou - jsou separatni 


            for (int i = 1; radek - i >= 0 && sloupec + i < pocetSloupcu; i++) //diagonála / nahoru; odecitam od radku, pricitam k sloupci
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

            for (int i = 1; radek + i < pocetRadku && sloupec - i >= 0; i++) //diagonála / dolu; pricitam k radku, odectiam od sloupce
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
}