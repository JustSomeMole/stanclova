using System;
using System.Collections.Generic;
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

        }

        static bool Check(int[,] gameField, int[] currentPosition, int currentPlayer, int reqForWin)
        {
            return CheckColumn(gameField, currentPosition, currentPlayer, reqForWin) || CheckRow(gameField, currentPosition, currentPlayer, reqForWin) || CheckDiag(gameField, currentPosition, currentPlayer, reqForWin);
        }

        static bool CheckColumn(int[,] gameField, int[] currentPosition, int currentPlayer, int reqForWin)
        {
            
        }

        static bool CheckRow(int[,] gameField, int[] currentPosition, int currentPlayer, int reqForWin)
        {
            int pocet
            while (true)
            {
                if

            }
        }

        static bool CheckDiag(int[,] gameField, int[] currentPosition, int currentPlayer, int reqForWin)
        {

        }
    }
}
