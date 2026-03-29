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
        public static InCombatPlayer InCombatPlayer = new();
    }
    public class InCombatPlayer
    {
        public InCombatPlayer() { }

        List<Type> Affinities { get; set; }
        List<Attack> Attacks { get; set; }
    }
}
