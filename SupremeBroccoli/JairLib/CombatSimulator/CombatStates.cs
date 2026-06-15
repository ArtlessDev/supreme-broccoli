using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CombatSimulator
{
    public enum GameStates
    {
        none,
        VerifyActors,
        selectMove,
        resolveActions,
        CheckActorsHP,
        GameOverLost,
        GameOverWon
    }

    public static class CombatStates
    {
        public static void HandleCombatStates()
        {

        }
        //TODO: ALL OF THE COMBAT STATES

    }
}
