using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TimspartaBasic.Week4_TextRPGcs;

namespace TimspartaBasic
{
    internal class Week4_TextRPGcs
    {
        //문자열 가운데 정렬 한글은 제대로 작동하지는 않는것 같다
        public static void StringCenter(string str)
        {
            Console.WriteLine(String.Format($"{str}").PadLeft(50 - (30 - (str.Length / 2))));// 가운데정렬
        }

        //캐릭터 인터페이스
        public interface ICharacter
        {
            string Name { get; set; }
            int Health { get; set; }
            int AttackPower { get; set; }

            int Attack { get; }
            bool IsDead { get; }

            void TakeDamge(int damage);
        }

        //플레이어 클래스
        public class Warrior : ICharacter
        {

            Random random = new Random();

            public string Name { get; set; }

            public int Health { get; set; }

            public int AttackPower { get; set; }

            public int Attack => random.Next(AttackPower - 5, AttackPower + 5);
            public bool IsDead => Health <= 0;

            public Warrior(string name)
            {
                Name = name;
                Health = 100;
                AttackPower = 20;
            }
            public void TakeDamge(int damage)
            {
                Health -= damage;
                StringCenter($"{Name}은(는) {damage}의 피해를 입었다");
            }

        }

        //몬스터 클래스
        public class Monster : ICharacter
        {
            Random random = new Random();
            public string Name { get; set; }

            public int Health { get; set; } = 100;

            public int AttackPower { get; set; }

            public int Attack => random.Next(AttackPower - 10, AttackPower + 3);
            public bool IsDead => Health <= 0;

            public Monster(string name)
            {
                Name = name;
                AttackPower = 10;
            }
            public void TakeDamge(int damage)
            {
                Health -= damage;
                StringCenter($"{Name}은(는) {damage}의 피해를 입었다");
            }
        }

        //아이템 인터페이스
        public interface IItem
        {
            string Name { get; set; }

            int Amount { get; set; }

            //미리 정의된 델리게이트로 값을 반환하지 않는 메서드를 나타내는 델리게이트 
            //아이템 강화에서 사용
            Action Enforce { get; set; }

            void Use(Warrior warrior);

            void AmountUp(); 
        }

        //체력포션 클래스
        public class HealthPotion : IItem
        {
            public string Name { get; set; } = "체력포션";

            public int Amount { get; set; } = 30;
            public void Use(Warrior warrior)
            {
                warrior.Health += Amount;
                StringCenter($"{Name}을 마셨다");
            }

            public Action Enforce { get; set; }

            public HealthPotion()
            {
                //델리게이트에 람다식으로 등록
                Enforce = AmountUp; 
            }

            //아이템 강화 함수
            public void AmountUp()
            {
                if (Name != "강화체력포션")
                {
                    Amount += 30;
                    StringCenter($"{Name} 아이템이 강화되었습니다.");
                    Name = "강화체력포션";
                }
                else
                {
                    StringCenter($"강화{Name}은 존재하지 않네요");
                }
            }
        }

        //공격력 포션 클래스
        public class StrengthPotion : IItem
        {
            public string Name { get; set; } = "공격력포션";
            public int Amount { get; set; } = 30; 

            public Action Enforce { get; set; }

            public StrengthPotion()
            {
                Enforce = AmountUp;
            }

            public void Use(Warrior warrior)
            {
                warrior.AttackPower += Amount;
                StringCenter($"{Name}을 마셨다");
            }
            public void AmountUp()
            {
                if (Name != "강화공격력포션")
                {
                    Amount += 30;
                    StringCenter($"{Name} 아이템이 강화되었습니다.");
                    Name = "강화공격력포션";
                }
                else
                {
                    StringCenter($"강화{Name}은 존재하지 않네요");
                }
            }
        }
        
        //스테이지 클래스
        public class Stage
        {
            //델리게이트 선언 
            public delegate void GameEvent(ICharacter character);
            //델리게이트 이벤트 등록
            public event GameEvent OnCharacterDeath;

            ICharacter player;
            ICharacter monster;
            List<IItem> rewardsItem;

            //스테이지 레벨을 나타내는 변수
            int level;

            public Stage(ICharacter player, ICharacter monster, List<IItem> rewardsItem, int level)
            {
                this.player = player;
                this.monster = monster;
                this.rewardsItem = rewardsItem;
                this.level = level;
                OnCharacterDeath += StageClear; //캐릭터 죽으면 StageClear 실행
            }

            //스테이지 시작 함수
            public void Start()
            {
                Console.Clear();
                string stageStrMessge = $"스테이지{level} 시작!";
                StringCenter(stageStrMessge);
                Console.WriteLine("  ------------------------------------------------");
                Thread.Sleep(1000);
                Console.WriteLine();

                while (!player.IsDead && !monster.IsDead)
                {
                    Console.WriteLine($"{player.Name,8}({player.Health}) -> 공격!");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    monster.TakeDamge(player.Attack);
                    Console.WriteLine();
                    Thread.Sleep(1000);

                    if(monster.IsDead) { break; }

                    Console.WriteLine($"공격! <- {monster.Name}({monster.Health})".PadLeft(46));
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    player.TakeDamge(monster.Attack);
                    Console.WriteLine();
                    Thread.Sleep(1000);
                }

                if (player.IsDead)
                {
                    Console.WriteLine();
                    StringCenter($"{player.Name}이(가) 죽었습니다.");
                    Thread.Sleep(1000);
                    //StageClear(monster);  이렇게 선언하게 되면 의존을 하게 되서 이 다음에 나올 함수들을 계속 선언을 해야한다 하지만 델리게이트를 사용하면 플레이어가 죽었을 때 실행할 함수들을 추가만 해주면 된다 
                    OnCharacterDeath?.Invoke(monster);
                }
                else if (monster.IsDead)
                {
                    Console.WriteLine();
                    StringCenter($"{monster.Name}이(가) 죽었습니다.");
                    Thread.Sleep(1000);
                    //StageClear(player);
                    OnCharacterDeath?.Invoke(player);
                }
            }

            //스테이지 클리어 함수
            void StageClear(ICharacter character)
            {
                if (character == player)
                {
                    Console.WriteLine("  ------------------------------------------------");
                    StringCenter("스테이지 클리어!");
                    Console.WriteLine(); 
                    Console.WriteLine("      보상의 이름을 입력하여 선택하세요");
                    Console.WriteLine(); 
                    foreach (var item in rewardsItem)
                    {
                        StringCenter($"{item.Name}  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("  ------------------------------------------------");
                    Console.Write(">> "); 
                    string? input = Console.ReadLine();


                    IItem? selectedItem = rewardsItem.Find(item => item.Name == input);

                    if (selectedItem != null)
                    {
                        //Warrior는 곧 player니까 형변환 가능
                        Console.WriteLine(); 
                        selectedItem.Use((Warrior)player);
                        Console.WriteLine();
                        //아이템 강화 함수를 델리게이트를 이용하여 호출
                        selectedItem.Enforce();
                        //물약 강화
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        StringCenter("아무것도 선택하지 않으셨습니다.");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine();
                    StringCenter("다음 스테이지로 넘어갑니다.");
                    Console.WriteLine();
                }
                else
                {
                    StringCenter("YOU DIED");
                    Thread.Sleep(1000);
                }
            }
        }

        //게임을 관리하는 클래스
        public class GameManager
        {
            Warrior player;
            Monster Slime;
            Monster goblin;
            Monster dragon;

            List<IItem> stageRewards;

            //스테이지를 리스트로 관리 
            List<Stage> stageMap;

            Stage stage1;
            Stage stage2;
            Stage stage3;

            public GameManager()
            {
                player = new Warrior("Player");
                Slime = new Monster("슬라임");
                goblin = new Monster("고블린");
                dragon = new Monster("드래곤");

                stageRewards = new List<IItem> { new HealthPotion(), new StrengthPotion() };

                stage1 = new Stage(player, Slime, stageRewards, 1);
                stage2 = new Stage(player, goblin, stageRewards, 2);
                stage3 = new Stage(player, dragon, stageRewards, 3);

                stageMap = new List<Stage>();

                stageMap.Add(stage1);
                stageMap.Add(stage2);
                stageMap.Add(stage3);
            }

            //게임 시작 함수
            public void StageStart()
            {
                foreach (var stage in stageMap)
                {
                    string startMessage = "게임을 시작합니다.";
                    StringCenter(startMessage); 
                    Thread.Sleep(1000);

                    stage.Start();

                    if (player.IsDead) return;

                    StringCenter("아무키나 눌러서 다음 스테이지 시작"); 
                    Console.ReadKey(); 
                }
                StringCenter("Victory!!");
            }

        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(53, 40);
            Console.CursorVisible = false; 

            GameManager Game = new GameManager();
            Game.StageStart();

        }
    }
}



