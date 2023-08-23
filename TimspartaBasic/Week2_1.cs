using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace TimspartaBasic
{
    internal class Week2_2
    {
        
        static void Main(string[] args)
        {
            //일반과제
            //2-1 구구단 출력하기 
            //for(int i = 1; i<=9; i++)
            //{
            //    for(int j = 1; j<=9; j++)
            //    {
            //        Console.WriteLine($"{i} X {j} = {i*j}"); 
            //    }
            //    Console.WriteLine();
            //}

            //2-2 별찍기
            //1) 오른쪽으로 기울어진 직각삼각형 출력
            //  0  |  1  |  2  |  3  |  4   <= i
            //  0  | 0~1 | 0~2 | 0~3 | 0~4  <= j

            //for(int i = 0; i<5; i++)
            //{
            //    for(int j = 0; j<=i; j++)
            //    {
            //        Console.Write("*"); 
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            //2) 역직각삼각형 출력하기 
            //  5  |  4  |  3  |  2  |  1   <= i
            //  0  | 0~1 | 0~2 | 0~3 | 0~4  <= j

            //for (int i = 5; i > 0; i--)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //}

            //3) 피라미드 출력하기 
            //  0  |  1  |  2  |  3  |  4   <= i
            // 0~3 | 0~2 | 0~1 |  0  |  x   <= j " " (4,3,2,1,x개 그림)  
            //  0  | 0~2 | 0~4 | 0~6 | 0~8  <= k "*" (1,3,5,7,9개 그림) 

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j <4-i; j++)
            //    {
            //        Console.Write(" ");
            //    }

            //    for (int k = 0; k < 2 * i + 1; k++) // 홀수개 그리기 위해서 
            //    {
            //        Console.Write("*");
            //    }

            //    Console.WriteLine();
            //}

            //2-3 최대값, 최소값 찾기 
            //int[] num = new int[5];
            //int max = 0;
            //int min = 0;
            //for (int i = 0; i < num.Length; i++)
            //{
            //    Console.Write("숫자를 입력하세요: ");
            //    num[i] = int.Parse(Console.ReadLine());
            //    min = num[0];
            //    if (max < num[i])
            //    {
            //        max = num[i];
            //    }

            //    if (min > num[i])
            //    {
            //        min = num[i];
            //    }
            //}
            //Console.WriteLine($"최대값: {max}");
            //Console.WriteLine($"최소값: {min}");

           // 2 - 4 소수 판별하기
            Console.Write("숫자를 입력하세요: ");
            int num = int.Parse(Console.ReadLine());

            if (IsPrime(num))
            {
                Console.WriteLine(num + "은 소수입니다.");
            }
            else
            {
                Console.WriteLine(num + "은 소수가 아닙니다.");
            }
        }

        static bool IsPrime(int num)
        {
            if (num <= 1) return false; // 1 이하는 소수가 아님 
            if (num % 2 == 0) // 2를 제외한 짝수는 소수가 아님 
            {
                return num == 2 ? true : false;
            }
            for (int i = 3; i <= Math.Sqrt(num); i += 2) // 홀수는 제곱근까지만 구하면 됨 그 이후의 숫자들은 이전에 나눠서 나온 숫자들이므로 중복되어 필요없음 
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

    }
}
