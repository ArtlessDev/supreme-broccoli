using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gum.Forms.Input;
using JairLib.Utility;

namespace JairLib.CombatSimulator
{
    public enum CombatStates
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
    public static bool pressedKey(Keys key)
    {
        return 
    }

    public static class CombatStateMachine
    {
        static CombatStates internalCombatState = CombatStates.none;
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
            if(Globals.keyb.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.D1))
            {
                //move up in the list
            }
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

        public static void CheckActorsHealth(List<CombatActors> foeParty)
        {
            //so long as the player is healthy, they can still fight
            bool playerGoodToGo = RpgPlayer.PlayerCombatActor.Health > 0 ? true : false;
            bool foePartyGoodToGo = false;
            foreach (CombatActors actor in foeParty)
            {
                if (actor.Health >= 0)
                {
                    foePartyGoodToGo = true;
                } 
            }

            if (playerGoodToGo && foePartyGoodToGo)
            {
                //keep fighting
                internalCombatState = CombatStates.SelectMove;
            }
            else if (playerGoodToGo && !foePartyGoodToGo)
            {
                internalCombatState = CombatStates.GameOverWon;
            }
            else
            {
                internalCombatState = CombatStates.GameOverLost;
            }
        }
        public static void GameOverLost(List<CombatActors> playerParty)
        {
            //players health gets reset to max hp along with their mp
            foreach (CombatActors partyMember in playerParty)
            {
                partyMember.Health = partyMember.MaximumHealth;
            }
            //player then gets sent back to the last save spot
        }


        /// <summary>
        /// this will require its own sub-state machine
        /// </summary>
        public static void GameOverWon()
        {
            //player gains exp/levelup

            //player party gains exp/levelup

            //maybe here a player needs to select newly learned move

            //player returns to area where they just were

        }

        public static CombatStates GetInternalState()
        {
            return internalCombatState;
        }
    }
}
