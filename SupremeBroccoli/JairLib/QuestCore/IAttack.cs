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

        public MoveDelegate UpgradeDelegate { get; set; }
        public string MoveName { get; set; }
        public MoveList MoveId { get; set; }
        public bool GetGoingFlag { get; set; }

        public delegate MoveGrouping MoveDelegate(MoveGrouping moveGroup);


        public MoveDelegate GetUpgradeMethod(int moveId)
        {

            switch ((MoveList)moveId)
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
