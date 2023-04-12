using DungeonsOfDoom.Core.Monsters;

namespace DungeonsOfDoom.Core
{
    class Player : Character
    {
        public Player(int health, int x, int y) : base("Player", 30, 10, 80)
        {
            Health = health;
            X = x;
            Y = y;
        }
    }
}
