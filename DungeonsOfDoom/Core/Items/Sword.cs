using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Xml.Linq;

namespace DungeonsOfDoom.Core.Items
{
    internal class Sword : Item
    {
        public Sword() : base("Sword")
        {
            Weight = 5;
        }
        public override void PickUpItem(Character player)
        {
            player.Inventory.Add(new Sword());
            player.Strength += 1;
        }
    }
}
