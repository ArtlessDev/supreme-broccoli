using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CombatSimulator
{

    public static class AddtMinigameFunctions
    {

        public static int GoodHitCounter = 0, BadHitCounter = 0;

        public static void GoodHitIncrement() => GoodHitCounter++;
        public static void BadHitIncrement() => BadHitCounter++;
        public static void ResetGoodHit() => GoodHitCounter = 0;
        public static void ResetBadHit() => BadHitCounter = 0;
    }
}
