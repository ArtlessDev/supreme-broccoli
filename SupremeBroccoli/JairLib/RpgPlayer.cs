using JairLib.CombatSimulator;
using JairLib.QuestCore;
using Microsoft.Xna.Framework;

namespace JairLib
{
    public static class RpgPlayer
    {
        public static PlayerOverworld PlayerOverworld = new();
        public static PlayerCombatActor PlayerCombatActor = new()
        { // this needs to be in a json and needs to be loaded before the game starts, specifically for like new saves or first time playing or something.
            MaximumHealth = 20,
            Health = 20,
            Name = "default",
            Speed = 10,
            Attack = 30,
            Moveset = [
                MoveList.Punch,
            ],
            Defense = 10,
            SpecialDefense = 10,
            Luck = 10,
            Accuracy = 10,
            Evasiveness = 10,
            identifier = "default",
            color = Color.White,
        };
        public static int PLAYER_TILESIZE_IN_WORLD = 100;
        public static List<Quest> Quests = new();
        public static List<CombatActors> PlayerCurrentParty = new();
        public static List<CombatActors> PlayerReserveParty = new();

    }
}
