using JairLib.QuestCore;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace JairLib.CombatSimulator
{
    public enum CombatStates
    {
        none,
        VerifyActors,
        SortTurnOrder,
        SelectMove,
        CombatMinigame,
        ResolveActions,
        CheckActorsHP,
        GameOverLost,
        GameOverWon,
        ReturnToScreen,
        SelectOpponent,
        ResolveSecondaryEffects
    }

    public static partial class CombatStateMachine
    {
        static CombatStates INTERNAL_COMBAT_STATE = CombatStates.none;
        public static Attack? SelectedMove = null;
        public static CombatActors CurrentTargetForAction = null;

        //TODO: ALL OF THE COMBAT STATES
        private static List<CombatActors> PlayerTeamReference, FoeTeamReference;
        public static void AssignActors()
        {

        }

        public static void VerifyActors(List<CombatActors> _playerTeam, List<CombatActors> _foeTeam)
        {
            PlayerTeamReference = _playerTeam;
            // dont need line 38, as the player is added to the party somewhere before this
            //PlayerTeamReference.Add(RpgPlayer.PlayerCombatActor); 
            FoeTeamReference = _foeTeam;

            INTERNAL_COMBAT_STATE = CombatStates.CheckActorsHP;
        }

        /// <summary>
        /// this will check speed of actors and then reorder the actors within the turnorder list
        /// </summary>
        public static void SortTurnOrder()
        {

        }

        public static void SelectMoveDraw(SpriteBatch _sb)
        {
            foreach(MoveList action in RpgPlayer.PlayerCombatActor.Moveset)
            {

                _sb.DrawString(Globals.stabilloFont, action.ToString(), new(128,800), RpgPlayer.PlayerCombatActor.color);
            }
        }

        /// <summary>
        /// this is where the player selects an action. this is where the bulk of the combat will be for the user. 
        /// this section needs to include:
        /// - Moves that the player chooses for themself
        /// - interacting with bag for player to use bagged items
        /// - selecting moves for party members
        /// - using bagged items for the party members
        /// NOTE: BECAUSE OF ALL THE COMPLEXITY, WE SHOULD AVOID THE ENEMY DOING THEIR OWN ACTION IN HERE.
        /// ALTERNATE: we make this reusable. as in, we inject the party member that is going to act and use their resources available 
        /// </summary>
        public static void SelectMove()
        {
            if(Globals.keyb.WasKeyPressed(Keys.D1))
            {
                //move up in the list
                SelectedMove = new Attack(RpgPlayer.PlayerCombatActor.Moveset[0]);
                SelectedMove.GetUpgradeMethod(SelectedMove.MoveId);
                INTERNAL_COMBAT_STATE = CombatStates.SelectOpponent;
            }

            if (Globals.keyb.IsKeyDown(Keys.D1))
            {
                //moveArr[0].ButtonAbility = newAbilities[0];
                //actions[0].ButtonAbility = newAbilities[0];
                //actions[0].ButtonAbility.BaseDamagePower = (float)(WaveNumber * .75f) * 30f;
                //GenerateAbilities = true;
                //newAbilities.Clear();
                //CurrentPhase = GamePhases.PlayerTurn;
                //task = ResetPhaseChangeFlag();
            }

        }

        public static void SelectOpponent()
        {
            if (Globals.keyb.WasKeyPressed(Keys.D1))
            {
                //move up in the list
                //SelectedMove = new Attack(RpgPlayer.PlayerCombatActor.Moveset[0]);
                FoeTeamReference[0].color = Color.Red;
                MoveGrouping.primaryTarget = FoeTeamReference[0];
                INTERNAL_COMBAT_STATE = CombatStates.ResolveActions;
            }
            else if (Globals.keyb.WasKeyPressed(Keys.D2) && FoeTeamReference[1]!=null)
            {
                //move up in the list
                //SelectedMove = new Attack(RpgPlayer.PlayerCombatActor.Moveset[1]);
                MoveGrouping.primaryTarget = FoeTeamReference[1];
                INTERNAL_COMBAT_STATE = CombatStates.ResolveActions;
            }

            MoveGrouping.foeGroup = FoeTeamReference;
        }

        public static MoveGrouping MoveGrouping = new MoveGrouping()
        {
            moveUser = RpgPlayer.PlayerCombatActor,
            primaryTarget = null,
            allyGroup = PlayerTeamReference,
            foeGroup = FoeTeamReference
        };
        /// <summary>
        /// this is where all of the actions, player and foe alike, are resolved
        /// </summary>
        public static void ResolveActions()
        {
            SelectedMove.AttackDelegate(MoveGrouping);
            INTERNAL_COMBAT_STATE = CombatStates.ResolveSecondaryEffects;
        }

        /// <summary>
        /// this is for afflictions and gradual heals like
        /// leftovers, leech seed, poison, burn, etc
        /// </summary>
        public static void ResolveSecondaryActions()
        {


            INTERNAL_COMBAT_STATE = CombatStates.CheckActorsHP;
        }

        public static void CheckActorsHealth(List<CombatActors> foeParty)
        {
            //so long as the player is healthy, they can still fight
            //we dont care about the HP stats for the player's party members.
            bool playerGoodToGo = RpgPlayer.PlayerCombatActor.Health > 0 ? true : false;
            bool foePartyGoodToGo = false;
            foreach (CombatActors actor in foeParty)
            {
                if (actor.Health >= 0)
                {
                    foePartyGoodToGo = true;
                }
                else
                    foePartyGoodToGo = false;
            }

            if (playerGoodToGo && foePartyGoodToGo)
            {
                //keep fighting
                INTERNAL_COMBAT_STATE = CombatStates.SelectMove;
            }
            else if (playerGoodToGo && !foePartyGoodToGo)
            {
                INTERNAL_COMBAT_STATE = CombatStates.GameOverWon;
            }
            else
            {
                INTERNAL_COMBAT_STATE = CombatStates.GameOverLost;
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

            if (Globals.keyb.WasKeyPressed(Keys.E)) INTERNAL_COMBAT_STATE = CombatStates.ReturnToScreen;
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


            if (Globals.keyb.WasKeyPressed(Keys.E)) INTERNAL_COMBAT_STATE = CombatStates.ReturnToScreen;
        }

        public static CombatStates GetInternalState()
        {
            return INTERNAL_COMBAT_STATE;
        }

        public static void CombatMinigame(SpinnerMinigame _spinnerMinigame, GameTime gameTime)
        {
            Globals.MainCamera.LookAt(_spinnerMinigame.CenteredCircle.Center);
            _spinnerMinigame.Update(gameTime);
        }
    }
}
