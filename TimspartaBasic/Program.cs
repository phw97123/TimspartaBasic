using System.Globalization;

namespace TimspartaBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.
            //Console.Write("이름을 입력하세요 : ");
            //string name = Console.ReadLine();
            //Console.Write("나이를 입력하세요 : ");
            //string age = Console.ReadLine();
            //Console.WriteLine("안녕하세요, {0} 당신은 {1} 세 이군요.", name, age);

            //2.
            //Console.Write("첫번째 수를 입력하세요 : ");
            //string firstNum = Console.ReadLine();
            //Console.Write("두번째 수를 입력하세요 : ");
            //string secondnum = Console.ReadLine();

            //int num1 = int.Parse(firstNum);
            //int num2 = int.Parse(secondnum);

            //Console.WriteLine("더하기: {0} ", num1 + num2);
            //Console.WriteLine("빼기: {0}", num1 - num2);
            //Console.WriteLine("곱하기: {0} ", num1 * num2);
            //Console.WriteLine("나누기: {0}", (float)num1 / num2);

            //3.
            //Console.Write("섭씨 온도를 입력하세요 : ");
            //string celsius = Console.ReadLine();

            //int fahrenheit = (int.Parse(celsius) * 9 / 5) + 32;

            //Console.Write("변환된 화씨 온도 : {0}", fahrenheit);

            //4
            //Console.Write("키(m)를 입력하세요 : ");
            //double height = double.Parse(Console.ReadLine());
            //Console.Write("체중(kg)을 입력하세요 : ");
            //double weight = double.Parse( Console.ReadLine());

            //double BMI = weight / (height * height); 

            //Console.WriteLine("BMI 지수는 {0:N2} 입니다", BMI);  

            //1. 사용자로부터 입력 받기 
            //Console.Write("이름을 입력하세요 : ");
            //string name = Console.ReadLine();
            //Console.Write("나이를 입력하세요 : ");
            //int age = int.Parse(Console.ReadLine()); 
            //Console.WriteLine($"안녕하세요, {name}! 당신은 {age} 세 이군요.");

            //2. 간단한 사칙연산 계산기 만들기 
            //Console.Write("첫번째 수를 입력하세요 : "); 
            //int num1 = int.Parse(Console.ReadLine());
            //Console.Write("두번째 수를 입력하세요 : ");
            //int num2 = int.Parse(Console.ReadLine());  

            //Console.WriteLine($"더하기 : {num1+num2}");
            //Console.WriteLine($"빼기 : {num1 - num2}");
            //Console.WriteLine($"곱하기 : {num1 * num2}");
            //Console.WriteLine($"나누기 : {(float)num1 / num2}");

            //3. 온도 변환기 만들기 
            //Console.Write("섭씨 온도를 입력하세요: "); 
            //int celsius = int.Parse(Console.ReadLine());
            //int fahrenheit = (celsius * 9/5) + 32;
            //Console.WriteLine($"변환된 화씨 온도: {fahrenheit}"); 

            //4.BMI 계산기 만들기 
            Console.Write("키(m)를 입력하세요 : ");
            float height = float.Parse(Console.ReadLine());
            Console.Write("체중(kg)을 입력하세요 : ");
            float weight = float.Parse(Console.ReadLine());
            float BMI = weight / (height * height);
            Console.WriteLine($"BMI 지수는 : {BMI} 입니더.");
        }
    }
}