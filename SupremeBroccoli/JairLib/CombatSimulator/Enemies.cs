using Microsoft.Xna.Framework;

namespace JairLib.CombatSimulator
{
    public class EnemyBee : CombatActors
    {
        public EnemyBee()
        {
            Name = "bee";
            Moveset = [MoveList.Punch];
            MaximumHealth = 5;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "bee";
            color = Color.White;
        }

    }
}
