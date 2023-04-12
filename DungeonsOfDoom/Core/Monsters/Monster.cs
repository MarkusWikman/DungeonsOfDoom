using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Xml.Linq;
namespace DungeonsOfDoom.Core.Monsters
{
    class Monster : Character, Ipickable
    {
        public int Weight { get; set;  }
        static public int MonsterCounter { get; set; }
        public Monster(string name, int health, int strength, int weight) : base(name, health, strength, weight)
        {
            Weight = weight;
            Name = name;
            Health = health;
            RandomItem();
            Strength = strength;
        }
        protected string Color;
        public void RandomItem()
        {
            List<Item> weapons = new List<Item>() { new Bread(), new Sword() };
            Random rnd = new Random();
            int rndWeapon = rnd.Next(weapons.Count);
            Inventory.Add(weapons[rndWeapon]);
        }

    }
}
