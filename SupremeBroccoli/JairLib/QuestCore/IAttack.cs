using JairLib.CombatSimulator;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.QuestCore
{
    public enum MoveList
    {
        None,
        Punch,
        Fuego_I,
        Hielo_I,
        Viento_I,
        Mag_Up,
        Phys_Up,
        Def_Up,
        Mag_Down,
        Phys_Down,
        Def_Down,
        Speed_Up,
        Speed_Down,
        Fuego_II,
        Hielo_II,
        Viento_II,
        Fuego_III,
        Hielo_III,
        Viento_III,
        SUMMON_GUNDAM_M,
        SUMMON_GUNDAM_P,
        Reflejo_P,
        Reflejo_M,
        Draco_Slash,
        Draco_Breath,
    }
    public partial class Attack
    {
        /// <summary>
        /// TODO: FINISH THIS ACCORDING TO EXCEL
        /// </summary>
        public byte Power;
        public byte Accuracy;
        public byte Effect;
        public KindOfAttack KindOfAttack;
        public Element Type;

        public MoveDelegate UpgradeDelegate { get; set; }
        public string MoveName { get; set; }
        public MoveList MoveId { get; set; }
        public bool GetGoingFlag { get; set; }

        public delegate MoveGrouping MoveDelegate(MoveGrouping moveGroup);


        public MoveDelegate GetUpgradeMethod(int rand)
        {

            switch ((MoveList)rand)
            {
                default:
                case (MoveList.Punch)://1
                    return Punch;
            }
        }

    }
    public struct MoveGrouping
    {
        public CombatActors moveUser;
        public CombatActors primaryTarget;
        public List<CombatActors> allyGroup;
        public List<CombatActors> foeGroup;
    }
    public partial class Attack
    {

        private MoveGrouping Punch(MoveGrouping moveGroup)
        {
            Power = 35;
            Accuracy = 95;
            KindOfAttack = KindOfAttack.Physical;
            Type = Element.Physical;

            var modifiedPower = (moveGroup.moveUser.Attack * Power) / 100;

            moveGroup.primaryTarget.Health -= modifiedPower;

            return moveGroup;
        }
    }
}
