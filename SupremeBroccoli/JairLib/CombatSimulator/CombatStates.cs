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
        SortTurnOrder,
        SelectMove,
        ResolveActions,
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

        public static void VerifyActors()
        {

        }

        /// <summary>
        /// this will check speed of actors and then reorder the actors within the turnorder list
        /// </summary>
        public static void SortTurnOrder()
        {

        }

        public static void SelectMove()
        {

        }

        public static void ResolveActions()
        {

        }

        /// <summary>
        /// this is for afflictions and gradual heals like
        /// leftovers, leech seed, poison, burn, etc
        /// </summary>
        public static void ResolveSecondaryActions()
        {

        }

        public static void CheckActorsHealth()
        {

        }
        public static void GameOverLost()
        {

        }

        public static void GameOverWon()
        {

        }
    }
}
