using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.QuestCore
{
    public interface IStats
    {

        public int HealthPoints { get; set; }
        public int BaseAttack {  get; set; }
        public int BaseDefense { get; set; }
        public int BaseSpeed { get; set; }
        public int BaseMagic { get; set; }
        public Element BaseElement { get; set; }
        public int AttackPips { get; set; }
        public int DefensePips { get; set; }
        public int SpeedPips { get; set; }
        public int MagicPips { get; set; }
    }
}
