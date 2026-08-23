using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CombatSimulator
{
    public class EnemyBee : CombatActors
    {
        public EnemyBee()
        {
            Name = "bee";
            Moveset = [MoveList.Punch];
        }

    }
}
