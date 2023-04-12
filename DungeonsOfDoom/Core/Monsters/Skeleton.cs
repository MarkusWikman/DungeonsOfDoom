using DungeonsOfDoom.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core.Monsters
{
    internal class Skeleton : Monster
    {
        public Skeleton() : base("Skeleton", 15, 5, 40)
        {

        }
        public override void Attack(Character opponent)
        {
            if (opponent.Health > Health * 2)
            {
                Strength = 1;
            }
            base.Attack(opponent);
        }
    }
}
