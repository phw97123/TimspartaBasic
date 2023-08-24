using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimspartaBasic
{
    internal class Test
    {
        //int를 반환하고 매개변수 2개를 받는 함수를 저장할 델리게이트 선언 
        //delegate int Calculate(int x, int y);

        ////매개변수 2개를 받아 더한 값을 int로 반환하는 함수
        //static int Add(int x, int y)
        //{
        //    return x + y;
        //}

        //static void Main()
        //{
        //    //델리게이트에 메서드 등록
        //    Calculate calc = Add;

        //    //일반 int 형 변수에 int형을 반환하고 매개변수 2개를 받는 델리게이트 사용 
        //    int result = calc(3, 5);
        //}

        //델리게이트를 그냥 변수에서 사용 

        //반환값이 없고 문자열을 매개변수로 받는 델리게이트 선언
        //delegate void MyDelegate(string message);

        //static void Method1(string message)
        //{
        //    Console.WriteLine("Method1 : " + message);
        //}

        //static void Method2(string message) 
        //{ 
        //    Console.WriteLine("Method2 : "+ message);
        //}

        //static void Main(string[] args)
        //{
        //    MyDelegate myDelegate = Method1;
        //    myDelegate += Method2;

        //    myDelegate("델리게이트 사용");

        //    Console.ReadKey(); 
        //}

        //두개의 결과 그냥 Delegate 는 추가하고 사용한다 

        //델리게이트 이벤트
        //반환값이 없고 float를 매개변수로 받는 델리게이트 선언
        //public delegate void EnemyAttackHandler(float damage);

        //public class Enemy
        //{
        //    //만들었던 델리게이트를 event 키워드로 선언
        //    public event EnemyAttackHandler? OnAttack;

        //    public void Attack(float damage)
        //    {
        //        //이벤트 호출 
        //        Console.WriteLine("enemy가 공격을 했습니다.");
        //        Console.WriteLine("호출 준비");
        //        OnAttack?.Invoke(damage);
        //        Console.WriteLine("호출 끝");
        //    }
        //}

        //public class Player
        //{
        //    public void TakeDamage(float damage)
        //    {
        //        Console.WriteLine("플레이어가 {0}의 데미지를 입었습니다.", damage);
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    Enemy enemy = new Enemy();
        //    Player player = new Player();

        //    //enemy의 이벤트 델리게이트에 플레이어의 함수 추가
        //    enemy.OnAttack += player.TakeDamage;

        //    //공격하면 플레이어의 함수 호출됨 
        //    enemy.Attack(10.0f);
        //}

        ////실행되면 호출해라, 추가로 다른 함수도 호출해라
        ////델리게이트 
        //// 델리게이트 선언
        //public delegate void enemyattackhandler(float damage);

        //// 적 클래스
        //public class enemy
        //{
        //    // 공격 이벤트
        //    public enemyattackhandler onattack;

        //    public enemy()
        //    {
        //        onattack = attack;
        //    }

        //    // 적의 공격 메서드
        //    public void attack(float damage)
        //    {
        //        console.writeline("enemy가 공격을 했습니다.");
        //        console.writeline("호출 준비");
        //        console.writeline("호출 끝");
        //    }
        //}

        //// 플레이어 클래스
        //public class player
        //{
        //    // 플레이어가 받은 데미지 처리 메서드
        //    public void handledamage(float damage)
        //    {
        //        // 플레이어의 체력 감소 등의 처리 로직
        //        console.writeline("플레이어가 {0}의 데미지를 입었습니다.", damage);
        //    }
        //}

        //// 게임 실행
        //static void main()
        //{
        //    // 적 객체 생성
        //    enemy enemy = new enemy();

        //    // 플레이어 객체 생성
        //    player player = new player();

        //    // 플레이어의 데미지 처리 메서드를 적의 공격 이벤트에 추가
        //    enemy.onattack += player.handledamage;

        //    enemy.onattack(10);
        //}
    }
}
