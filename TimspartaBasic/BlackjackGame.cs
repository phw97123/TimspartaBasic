using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static TimspartaBasic.BlackjackGame;

namespace TimspartaBasic
{
    internal class BlackjackGame
    {
        public enum Suit { Hearts, Diamonds, Clubs, Spades} 
        public enum Rank { Two = 2, Three , Four,Five,Six,Seven,Eight, Nine,Ten,Jack,Queen,King,Ace } 

        public class Card //카드 한장에 대한 클래스
        {
            public Suit Suit { get; private set; }
            public Rank Rank { get; private set; }

            public Card(Suit s, Rank r) 
            {
                Suit = s;
                Rank = r; 
            }

            public int GetValue() //카드의 값을 숫자로 변경 
            {
                if ((int)Rank <= 10)
                    return (int)Rank;
                else if ((int)Rank <= 13) // K,Q,J 카드에 대한 처리 10으로 맞춤
                    return 10;
                else //Ace 카드에 대한 처리 (일단 11로 맞춤) 
                    return 11; 
            }

            public override string ToString()
            {
                return $"{Rank} of {Suit}";
            }
        }

        public class Deck //덱을 표현하는 클래스
        {
            private List<Card> cards; //카드 리스트
            public Deck()
            {
                cards = new List<Card>();

                foreach(Suit s in Enum.GetValues(typeof(Suit))) //카드리스트 안 카드에 대한 정보 넣기 
                {
                    foreach(Rank r in Enum.GetValues(typeof(Rank)))
                    {
                        cards.Add(new Card(s, r));
                    }
                }
                Shuffle();
            }

            public void Shuffle() //섞음
            {
                Random rand = new Random();

                for(int i = 0; i<cards.Count; i++)
                {
                    int j = rand.Next(i,cards.Count);
                    Card temp = cards[i];
                    cards[i] = cards[j];
                    cards[j] = temp;
                }
            }

            public Card DrawCard() //카드 뽑기
            {
                Card card = cards[0];
                cards.RemoveAt(0);
                return card;
            }
        }

        public class Hand //손패를 표현하는 클래스 
        {
            public List<Card> cards; //가진 카드 리스트

            public Hand()
            {
                cards = new List<Card>();
            }

            public void AddCard(Card card) //카드 추가
            {
                cards.Add(card); 
            }

            public int GetTotlaValue() //최종 점수
            {
                int total = 0;
                int aceCount = 0; 

                foreach(Card card in cards)
                {
                    if(card.Rank == Rank.Ace)
                    {
                        aceCount++;
                    }
                    total += card.GetValue(); 
                }

                // 점수가 21을 초과하고 Ace 카드가 포함되어있다면 Ace 카드의 점수를 1로 설정
                while (total>21 && aceCount > 0) 
                {
                    total -= 10; 
                    aceCount--;
                }

                return total;
            }

           
        }

        public class Player //플레이어를 표현하는 클래스 
        {
            public Hand Hand { get; private set; } // 플레이어가 가진 손패

            public Player()
            {
                Hand = new Hand();
            }

            public Card DrawCardFromDeck(Deck deck) //덱에서 카드 뽑기 
            {
                Card drawnCard = deck.DrawCard();
                Hand.AddCard(drawnCard);
                return drawnCard;
            }

            public virtual void ShowHand()
            {
                Console.WriteLine($"===== player =====");
                foreach (var card in Hand.cards)
                {
                    Console.WriteLine(card.ToString());
                }
                Console.WriteLine("점수 : " + Hand.GetTotlaValue());
            }
        }

        public class Dealer : Player //딜러를 표현하는 클래스
        {
            public bool isFirstTurn { get; set; } // 딜러가 처음 카드를 받았는지 체크하는 변수
            public Dealer()
            {
                isFirstTurn = true;
            }
            
            public override void ShowHand() 
            {
                Console.WriteLine("===== 딜러 ====="); 
                
                if(isFirstTurn)  // 딜러는 처음 카드를 받을 때 두번째로 받은 카드를 보여주지 않는다
                {
                    Console.WriteLine(Hand.cards[0].ToString());
                    Console.WriteLine("???");
                    Console.WriteLine("점수: ??"); 
                }
                else // 딜러의 차례에서 카드 공개 
                {
                    foreach(var card in Hand.cards)
                    {
                        Console.WriteLine($"{card.ToString()}");
                    }
                    Console.WriteLine("점수: "+ Hand.GetTotlaValue());
                }
            }
        }

        public class Blackjack //블랙잭 게임로직 구현 
        {
            Deck deck;
            Player player;
            Dealer dealer;

            bool isPlayerBust = false; 
            bool isDealerBust = false;

            public Blackjack()
            {
                deck = new Deck();
                player = new Player();
                dealer = new Dealer();
            }
            public void ScreenChange() // 메인화면 전환 
            {
                while(true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        Thread.Sleep(1000); 
                        Console.SetCursorPosition(0, Console.CursorTop - 1); // 이전에 출력한 라인의 처음으로 이동
                        Console.Write(new string(' ', Console.WindowWidth)); // 해당 라인을 공백으로 채움
                        Console.SetCursorPosition(0, Console.CursorTop); // 커서를 지금 있는 행의 맨 앞으로 이동 ( 라인을 공백으로 채워서 커서는 맨 뒤로 가 있다) 
                    }
                }
                
            }

            public void Run()
            {
                Console.WriteLine("==========블랙잭 게임을 시작합니다.==========");
                Console.WriteLine("처음엔 플레이어 -> 딜러 순으로 카드 두장을 받고 시작합니다 (단, 딜러의 두번째 카드는 보여주지 않습니다. ");
                Console.WriteLine("화면 넘김 : enter.");

                ScreenChange();

                for (int i = 0; i < 2; i++) // 카드를 2장씩 받는다 
                {
                    player.DrawCardFromDeck(deck);
                    dealer.DrawCardFromDeck(deck);
                }

                while(player.Hand.GetTotlaValue() <= 21)
                {
                    Console.Clear();

                    Console.WriteLine("플레이어의 차례");
                    player.ShowHand();
                    dealer.ShowHand();
                    Console.WriteLine("Hit ? Stay ? (Push H or S)");
                    char playerChoice = Console.ReadKey().KeyChar;
                    
                    if ((playerChoice == 'H' || playerChoice == 'h') && !isPlayerBust )
                    {
                        player.DrawCardFromDeck(deck);
                        Console.Clear();
                        player.ShowHand();
                        dealer.ShowHand();

                        if (player.Hand.GetTotlaValue() > 21)
                        {
                            Console.WriteLine("Bust!!");
                            isPlayerBust = true;
                        }
                    }
                    else if (playerChoice == 'S' || playerChoice == 's' )
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(); 
                        Console.WriteLine("잘못된 입력입니다");
                        Thread.Sleep(1000); 
                        continue; 

                    }
                }

                
                while ((dealer.Hand.GetTotlaValue() < 17 && dealer.Hand.GetTotlaValue() < 21) || dealer.isFirstTurn ==true)
                {
                    Console.Clear();
                    Console.WriteLine("딜러의 차례");
                    dealer.isFirstTurn = false;
                    dealer.DrawCardFromDeck(deck);
                    player.ShowHand();
                    dealer.ShowHand();
                    Thread.Sleep(1000);
                    if (dealer.Hand.GetTotlaValue() > 21)
                    {
                        isDealerBust = true;
                        Console.WriteLine("딜러 Bust!!");
                    }
                }

                if(isDealerBust) // 게임 종료 로직
                {
                    if (isPlayerBust)
                        Console.WriteLine("무승부!");
                    else
                        Console.WriteLine("플레이어 승!"); 
                }
                else 
                {
                    if(isPlayerBust)
                        Console.WriteLine("딜러 승!");
                    else
                    {
                        if (player.Hand.GetTotlaValue() > dealer.Hand.GetTotlaValue())
                            Console.WriteLine("플레이어 승!");
                        else if (player.Hand.GetTotlaValue() < dealer.Hand.GetTotlaValue())
                            Console.WriteLine("딜러 승!");
                        else
                            Console.WriteLine("무승부!"); 
                    }
                }
                
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            //게임 실행 
            Blackjack blackjack = new Blackjack();
            blackjack.Run();
        }
    }
}
