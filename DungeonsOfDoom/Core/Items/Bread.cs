using DungeonsOfDoom.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;
using System.IO.Pipes;
using System.Xml.Linq;


namespace DungeonsOfDoom.Core.Items
{
    internal class Bread : Item
    {
        public Bread() : base("Bread")
        {
            Weight = 1;
        }

        public override void PickUpItem(Character player)
        {
            player.Health += 3;
        }
    }
}
