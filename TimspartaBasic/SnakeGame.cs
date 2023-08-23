using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static TimspartaBasic.SnakeGame;

namespace TimspartaBasic
{
    internal class SnakeGame
    {
        public class Position // x,y 좌표를 표현하는 클래스
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Snake
        {
            List<Position> body;
            public Position direction { get; set; }

            public Snake(int x, int y)
            {
                body = new List<Position>
                {
                    new Position(x, y),
                    new Position(x-1,y),
                    new Position(x-2, y)
                };
                direction = new Position(1, 0);
            }

            public void Draw() //처음 뱀 그리기
            {
                for (int i = 0; i < body.Count; i++)
                {
                    Console.SetCursorPosition(body[i].X, body[i].Y);
                    if (i == 0) //머리
                        Console.Write("@");
                    else //꼬리
                        Console.Write("*");
                }
            }

            public void Move() //이동
            {
                Position newHead = new Position(body[0].X + direction.X, body[0].Y + direction.Y); // 이동할 위치 계산
                body.Insert(0, newHead); // 위치를 머리로 넣어주기 
                body.RemoveAt(body.Count - 1); // 꼬리 하나 줄이기
            }

            public void DrawTail() //꼬리추가
            {
                Position tail = body[body.Count - 1];
                body.Add(tail);
            }

            public bool IsGameOver(int maxWidth, int maxHeight) //게임 종료 조건
            {
                Position head = body[0];

                //벽 충돌
                if (head.X <= 0 || head.X >= maxWidth - 1 || head.Y <= 0 || head.Y >= maxHeight - 1)
                {
                    return true;
                }

                //자기 몸통 충돌
                for (int i = 1; i < body.Count; i++)
                {
                    if (body[i].X == head.X && body[i].Y == head.Y)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool EatFood(FoodCreator food) // 음식 먹기
            {
                foreach(var segment in body)
                {
                    if (segment.X == food.position.X && segment.Y == food.position.Y)
                    {
                        return true; 
                    }
                }
                return false;
            }
        }

        public class FoodCreator 
        {
            public Position position { get; set; }

            Random random;
            int maxWidth;
            int maxHeight;
            public FoodCreator(int maxWidth, int maxHeight)
            {
                this.maxWidth = maxWidth;
                this.maxHeight = maxHeight;
                random = new Random();
                Respawn();
            }

            public void Respawn() //재생성
            {
                position = new Position(random.Next(1, maxWidth - 1), random.Next(1, maxHeight - 1));
            }

            public void Draw() //그리기
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("$");
            }
        }
      

        static void Main(string[] args)
        {
            Console.CursorVisible = false; //커서 숨김

            int width = 70;
            int height = 20;

            Snake snake = new Snake(width / 2, height / 2);
            FoodCreator food = new FoodCreator(width, height);

            while (true)
            {
                if (Console.KeyAvailable) //방향전환
                {
                    var Key = Console.ReadKey(true).Key;

                    switch (Key)
                    {
                        case ConsoleKey.UpArrow:
                            snake.direction = new Position(0, -1);
                            break;
                        case ConsoleKey.DownArrow:
                            snake.direction = new Position(0, 1);
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.direction = new Position(-1, 0);
                            break;
                        case ConsoleKey.RightArrow:
                            snake.direction = new Position(1, 0);
                            break;
                    }
                }

                if (snake.EatFood(food)) //음식을 먹었다면 꼬리 생성후 음식 재생성
                {
                    snake.DrawTail();
                    food.Respawn();
                }
                else
                {
                    snake.Move(); 
                }

                if (snake.IsGameOver(width, height)) //게임 종료
                {
                    Console.Clear();
                    Console.SetCursorPosition(width / 2 - 5, height / 2);
                    Console.WriteLine("Game Over");
                    break;
                }

                DrawWall(width + 1, height + 1);
                snake.Draw();
                food.Draw();
                Thread.Sleep(100);
            } 
        }
        static void DrawWall(int width, int height) // 벽그리기
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
