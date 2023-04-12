using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonsOfDoom
{
    class Program
    {
        List<string> monsters = new List<string>() { "Skeleton", "Zombie" };
        List<Item> items = new List<Item>() { new Bread(), new Sword() };
        Room[,] rooms;
        Player player;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Play();
        }

        public void Play()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreatePlayer();
            CreateRooms();

            do
            {
                Console.Clear();
                DisplayRooms();
                DisplayStats();
                AskForMovement();
                CheckForItems();
            } while (player.IsAlive && Monster.MonsterCounter > 0);
            GameOver();
        }



        void CreatePlayer()
        {
            player = new Player(30, 0, 0);
        }

        Monster GenerateRandomMonster()
        {
            Random rnd = new Random();
            switch (monsters[rnd.Next(monsters.Count)])
            {
                case "Skeleton":
                    return new Skeleton();
                case "Zombie":
                    return new Zombie();
                default:
                    throw new Exception();
            }
        }
        void CreateRooms()
        {
            rooms = new Room[20, 5];
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    rooms[x, y] = new Room();

                    int percentage = Random.Shared.Next(1, 100 + 1);
                    if (percentage < 10)
                    {
                        rooms[x, y].MonsterInRoom = GenerateRandomMonster();
                        Monster.MonsterCounter++;
                    }
                    else if (percentage < 20)
                        rooms[x, y].ItemInRoom = items[Randomizer(items.Count)];
                }
            }
        }

        void DisplayRooms()
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    Room room = rooms[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write("🥷");
                    else if (room.MonsterInRoom?.Name == "Skeleton")
                        Console.Write("💀");
                    else if (room.MonsterInRoom?.Name == "Zombie")
                        Console.Write("🧟");
                    else if (room.ItemInRoom?.Name == "Sword")
                        Console.Write("⚔️");
                    else if (room.ItemInRoom?.Name == "Bread")
                        Console.Write("🍞");
                    else
                        Console.Write("🧱");
                }
                Console.WriteLine();
            }
        }

        void DisplayStats()
        {
            int swordCounter = 0;
            int zombieCorpseCounter = 0;
            int skeletonCorpseCounter = 0;
            Console.ForegroundColor = player.Health > 5 ? ConsoleColor.Green : ConsoleColor.DarkRed;
            Console.WriteLine($"Health: {player.Health}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Strength: {player.Strength}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Monsters: {Monster.MonsterCounter}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Inventory: ");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Name == "Sword")
                    swordCounter++;
                if (player.Inventory[i].Name == "Zombie")
                    zombieCorpseCounter++;
                if (player.Inventory[i].Name == "Skeleton")
                    skeletonCorpseCounter++;
            }
            if (skeletonCorpseCounter > 0)
                Console.WriteLine($" - Skeleton bones ({skeletonCorpseCounter})");
            if (swordCounter > 0)
                Console.WriteLine($" - Sword ({swordCounter})");
            if (zombieCorpseCounter > 0)
                Console.WriteLine($" - Zombie corpse ({zombieCorpseCounter})");

            Console.ForegroundColor = ConsoleColor.White;
        }

        void AskForMovement()
        {
            int newX = player.X;
            int newY = player.Y;
            bool isValidKey = true;
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidKey = false; break;
            }

            if (isValidKey &&
                newX >= 0 && newX < rooms.GetLength(0) &&
                newY >= 0 && newY < rooms.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;
            }
        }

        private void CheckForItems()
        {
            var roomXY = rooms[player.X, player.Y];
            PickUpRoomItem(roomXY);
            PickUpMonsterItemAndMonster(roomXY);
        }
        private void PickUpMonsterItemAndMonster(Room roomXY)
        {
            Character monster = roomXY.MonsterInRoom;
            if (monster != null)
            {
                player.Attack(monster);
                if (monster.Health > 0)
                    monster.Attack(player);
                if (monster.Inventory != null && monster.Health <= 0)
                {
                    for (int i = 0; i < monster.Inventory.Count; i++)
                        player.Inventory.Add(monster.Inventory[i]);
                }
                monster.Inventory = null;
                if (monster.Health <= 0)
                {
                    player.Inventory.Add(monster);
                    roomXY.MonsterInRoom = null;
                    Monster.MonsterCounter--;
                }
            }
        }

        private void PickUpRoomItem(Room roomXY)
        {
            if (roomXY.ItemInRoom != null)
            {
                roomXY.ItemInRoom.PickUpItem(player);
                roomXY.ItemInRoom = null;
            }

        }

        public int Randomizer(int listCount)
        {
            Random rnd = new Random();
            int rndNr = rnd.Next(listCount);
            return rndNr;
        }
        void GameOver()
        {
            Console.Clear();
            string text = "Game over!";
            foreach (var c in text)
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.WriteLine();
            Console.WriteLine("Press [ENTER] to play again: ");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Enter)
            {
            Play();
            }
        }
    }
}
