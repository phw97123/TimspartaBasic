using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TimspartaBasic
{
    internal class Week2_2
    {
        //게임 과제 
        //2-5 숫자 맞추기 

        //static void Main(string[] args)
        //{
        //    Random random = new Random();
        //    int targetNum = random.Next(1, 101);

        //    Console.WriteLine("숫자 맞추기 게임을 시작합니다. 1에서 100까지의 숫자 중 하나를 맞춰보세요.");

        //    int choice;
        //    int count = 0;
        //    while (true)
        //    {
        //        Console.Write("숫자를 입력하세요 : ");
        //        bool bValid = int.TryParse(Console.ReadLine(), out choice);

        //        if (bValid)
        //        {
        //            count++;
        //            if (choice > targetNum)
        //                Console.WriteLine("너무 큽니다!");
        //            else if (choice < targetNum)
        //                Console.WriteLine("너무 작습니다!");
        //            else
        //            {
        //                Console.WriteLine($"축하합니다! {count}번 만에 숫자를 맞추었습니다.");
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("잘못 입력하셨습니다");
        //        }
        //    }
        //}


        //2-6 틱택토
        static void DrawBoard() // 보드판 
        {
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {boardNum[0]}  |  {boardNum[1]}  |  {boardNum[2]}   ");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"-----+-----+-----");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {boardNum[3]}  |  {boardNum[4]}  |  {boardNum[5]}   ");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"-----+-----+-----");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {boardNum[6]}  |  {boardNum[7]}  |  {boardNum[8]}   ");
            Console.WriteLine($"     |     |     ");
        }

        static bool CheckWin(char symbol) // 이기는 조건 
        {
            return (boardNum[0] == symbol && boardNum[1] == symbol && boardNum[2] == symbol) ||
                   (boardNum[3] == symbol && boardNum[4] == symbol && boardNum[5] == symbol) ||
                   (boardNum[6] == symbol && boardNum[7] == symbol && boardNum[8] == symbol) ||
                   (boardNum[0] == symbol && boardNum[3] == symbol && boardNum[1] == symbol) ||
                   (boardNum[1] == symbol && boardNum[4] == symbol && boardNum[7] == symbol) ||
                   (boardNum[2] == symbol && boardNum[5] == symbol && boardNum[8] == symbol) ||
                   (boardNum[0] == symbol && boardNum[4] == symbol && boardNum[8] == symbol) ||
                   (boardNum[2] == symbol && boardNum[4] == symbol && boardNum[6] == symbol);
        }

        static bool isBoardFull() //보드가 꽉 찼다면 
        {
            foreach (char board in boardNum)
            {
                if (board != 'X' && board != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        static char[] boardNum = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int playerTurn = 1;
        static void Main(string[] args)
        {
            bool gameWon;

            do  
            {
                Console.Clear();
                Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
                Console.WriteLine();
                Console.WriteLine($"플레이어 {playerTurn}의 차례");
                Console.WriteLine();
                DrawBoard();

                int choice;
                bool bValid; // 잘못 입력 했을 때 

                do // 예외처리
                {
                    Console.Write("입력: ");
                    bValid = int.TryParse(Console.ReadLine(), out choice);

                    if (bValid == false || choice < 0 || choice > 9 || boardNum[choice - 1] == 'X' || boardNum[choice - 1] == 'O')
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        bValid = false;
                    }
                }
                while (!bValid);

                char symbole = playerTurn == 1 ? 'X' : 'O';
                boardNum[choice - 1] = symbole;

                gameWon = CheckWin(symbole); // 이겼는지 

                if (gameWon) // 현재 player가 이겼다면 
                {
                    Console.Clear();
                    Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
                    Console.WriteLine();
                    DrawBoard();
                    Console.WriteLine();
                    Console.WriteLine($"플레이어 {playerTurn}이 이겼습니다!");
                }
                else if (isBoardFull()) // 보드판이 꽉 찼다면 
                {
                    Console.Clear();
                    Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
                    Console.WriteLine();
                    DrawBoard();
                    Console.WriteLine();
                    Console.WriteLine("무승부 !");
                    break;
                }

                playerTurn = playerTurn == 1 ? 2 : 1; // 플레이어 변경 
            }
            while (!gameWon); // 누가 이기지 않았을 때 까지 

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

