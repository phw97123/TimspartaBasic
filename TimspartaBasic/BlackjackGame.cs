using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static TimspartaBasic.BlackjackGame;

namespace TimspartaBasic
{
    internal class BlackjackGame
    {
        public enum Suit { Hearts, Diamonds, Clubs, Spades} //카드 모양 
        public enum Rank { Two = 2, Three , Four,Five,Six,Seven,Eight, Nine,Ten,Jack,Queen,King,Ace } // 카드 값

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
                else if ((int)Rank <= 13)
                    return 10;
                else
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

                foreach(Suit s in Enum.GetValues(typeof(Suit))) //카드리스트에 카드에 대한 정보 넣기 
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

                while(total>21 && aceCount > 0)
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
            public bool isFirstTurn { get; set; }
            public Dealer()
            {
                isFirstTurn = true;
            }
            public void DrawCard(Deck deck)
            {
                while (Hand.GetTotlaValue() < 17 && Hand.GetTotlaValue()<21)
                {
                    Card drawnCard = deck.DrawCard();
                    Hand.AddCard(drawnCard);
                }
            }

            public override void ShowHand()
            {
                Console.WriteLine("===== 딜러 ====="); 
                
                if(isFirstTurn)
                {
                    Console.WriteLine(Hand.cards[0].ToString());
                    Console.WriteLine("???");
                    Console.WriteLine("점수: ??"); 
                }
                else
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
            public void ScreenChange()
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                }
            }

            public void Run()
            {
                Console.WriteLine("==========블랙잭 게임을 시작합니다.==========");
                Console.WriteLine("처음엔 플레이어 -> 딜러 순으로 카드 두장을 받고 시작합니다 (단, 딜러의 두번째 카드는 보여주지 않습니다. ");
                Console.WriteLine("화면 넘김 : enter.");

                ScreenChange();

                for (int i = 0; i < 2; i++)
                {
                    player.DrawCardFromDeck(deck);
                    dealer.DrawCardFromDeck(deck);
                }

                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("플레이어의 차례");
                    player.ShowHand();
                    dealer.ShowHand();

                    Console.WriteLine("Hit ? Stay ? (Push H or S)");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    
                    if (key.Key == ConsoleKey.H && isPlayerBust == false)
                    {
                        player.DrawCardFromDeck(deck);

                        if (player.Hand.GetTotlaValue() > 21)
                        {
                            Console.WriteLine("Bust!!");
                            isPlayerBust = true;
                        }
                    }
                    else 
                    {
                        Console.Clear();
                        Console.WriteLine("딜러의 차례");
                        dealer.isFirstTurn = false;
                        dealer.DrawCard(deck);
                        player.ShowHand();
                        dealer.ShowHand(); 
                        Console.WriteLine();

                        if(dealer.Hand.GetTotlaValue() >21)
                        {
                            isDealerBust = true;
                        }

                        break;
                    }
                }

                if (!isPlayerBust && isDealerBust)
                {
                    Console.WriteLine("플레이어 승!");
                }
                else if (isPlayerBust && !isDealerBust)
                {
                    Console.WriteLine("딜러 승!");
                }
                else if (player.Hand.GetTotlaValue() > dealer.Hand.GetTotlaValue())
                {
                    Console.WriteLine("플레이어 승!");
                }
                else if(player.Hand.GetTotlaValue() < dealer.Hand.GetTotlaValue())
                {
                    Console.WriteLine("딜러 승!");
                }
                else
                {
                    Console.WriteLine("무승부!");
                }

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
