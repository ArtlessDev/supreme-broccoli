using JairLib.QuestCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib
{
    public static class RpgPlayer
    {
        public static PlayerOverworld PlayerOverworld = new();
        public static PlayerCombatActor PlayerCombatActor = new();
        public static int PLAYER_TILESIZE_IN_WORLD = 100;
        public static List<Quest> Quests = new();
    }
}
