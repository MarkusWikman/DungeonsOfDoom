using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Numerics;
using System.Threading;
using System.Xml.Linq;

namespace DungeonsOfDoom.Core
{
    internal class Character : Ipickable
    {
        public Character(string name, int health, int strength, int weight)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Weight = weight;
        }
        public int Weight { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Ipickable> Inventory { get; set; } = new List<Ipickable> { };



        public virtual void Attack(Character opponent)
        {
            opponent.Health -= Strength;
        }

        //public void BreadHealthAdd()
        //{
        //    if (Inventory.Contains(Item item)
        //    Health = Health + 2;
        //}
        //public void SwordAddStrength()
        //{

        //}
    }
}
