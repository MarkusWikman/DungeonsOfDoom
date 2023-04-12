using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Monsters;

namespace DungeonsOfDoom.Core
{
    class Room
    {
        public Monster MonsterInRoom { get; set; }
        public Item ItemInRoom { get; set; }
    }
}
