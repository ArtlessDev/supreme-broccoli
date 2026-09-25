using JairLib.CombatSimulator;

namespace JairLib.QuestCore
{
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

        public MoveDelegate AttackDelegate { get; set; }
        public string MoveName { get; set; }
        public MoveList MoveId { get; set; }
        public bool GetGoingFlag { get; set; }

        public delegate MoveGrouping MoveDelegate(MoveGrouping moveGroup);

        public Attack(MoveList _moveId)
        {
            switch (_moveId)
            {
                default:
                case (MoveList.Punch)://1
                    AttackDelegate = Punch;
                    break;
            }
        }

        public MoveDelegate GetUpgradeMethod(MoveList moveId)
        {

            switch (moveId)
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
        public float AttackBooster;
    }
    public partial class Attack
    {

        private MoveGrouping Punch(MoveGrouping moveGroup)
        {
            Power = 10;
            Accuracy = 95;
            KindOfAttack = KindOfAttack.Physical;
            Type = Element.Physical;

            
            var modBoost = (moveGroup.AttackBooster * 1.25f) * Power;

            int modifiedPower = (moveGroup.moveUser.Attack * (int)modBoost) / 100;

            moveGroup.primaryTarget.CurrentHealth -= modifiedPower;

            return moveGroup;
        }
    }
}
