using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Xml.Linq;

namespace DungeonsOfDoom.Core.Items
{
    abstract class Item : Ipickable
    {
        public Item(string name)
        {
            Name = name;
        }
        public int Weight { get; set; }
        public string Name { get; set; }

        public abstract void PickUpItem(Character player);
    }
}
