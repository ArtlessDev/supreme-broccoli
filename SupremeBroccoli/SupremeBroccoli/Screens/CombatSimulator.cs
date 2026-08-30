using Gum.Forms.Controls;
using JairLib;
using JairLib.CombatSimulator;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SupremeBroccoli.Screens
{
    public class CombatSimulator : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public static List<CombatActors> PlayerParty = new List<CombatActors>();
        public static List<CombatActors> FoeParty = new List<CombatActors>();
        public static Screen returnToThisScreen;
        public CombatStates CurrentState;
        public SpinnerMinigame spinnerMinigame;
        public CombatSimulator(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }

        public override void LoadContent()
        {
            CombatGUI.Load();
            CurrentState = CombatStates.CombatMinigame;
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);

            var f = Globals.MainCamera.Center;
            spinnerMinigame = new SpinnerMinigame();

            if (FoeParty == null || FoeParty.Count == 0)
            {
                FoeParty = new List<CombatActors>();
                FoeParty.Add(new EnemyBee());
            }
            if (PlayerParty == null || PlayerParty.Count == 0) 
                PlayerParty = RpgPlayer.PlayerCurrentParty;
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);


            if (Globals.keyb.WasKeyPressed(Keys.Enter))
                ChangeBackScreen(GraphicsDevice, ScreenManager);

            CurrentState = CombatStateMachine.GetInternalState();

            switch (CurrentState)
            {
                case (CombatStates.VerifyActors):
                case (CombatStates.none):
                    CombatStateMachine.VerifyActors(PlayerParty, FoeParty);
                    break;
                case (CombatStates.CheckActorsHP):
                    CombatStateMachine.CheckActorsHealth(FoeParty);
                    break;
                case (CombatStates.GameOverLost):
                    CombatStateMachine.GameOverLost(FoeParty);
                    break;
                case (CombatStates.ReturnToScreen):
                    ChangeBackScreen(GraphicsDevice, ScreenManager);
                    break;
                case (CombatStates.CombatMinigame):
                    CombatStateMachine.CombatMinigame(spinnerMinigame, gameTime);
                    break;
                case (CombatStates.SortTurnOrder):
                    CombatStateMachine.SortTurnOrder();
                    break;
                case (CombatStates.SelectMove):
                    CombatStateMachine.SelectMove();
                    break;
                case (CombatStates.ResolveActions):
                    CombatStateMachine.ResolveActions();
                    break;
                case (CombatStates.GameOverWon):
                    CombatStateMachine.GameOverWon();
                    break;

            }

            //CombatGUI.Update();
            //CombatGUI.fleeButton.update();
            //CombatGUI.bagButton.update();

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Game._spriteBatch.Begin();


            switch (CurrentState)
            {
                case (CombatStates.SelectMove):
                    CombatStateMachine.SelectMove();
                    break;
                case (CombatStates.CombatMinigame):
                    spinnerMinigame.Draw(gameTime, Game._spriteBatch);
                    break;
                case (CombatStates.ReturnToScreen):
                    ChangeBackScreen(GraphicsDevice, ScreenManager);
                    break;
                case (CombatStates.CheckActorsHP):
                case (CombatStates.GameOverLost):
                case (CombatStates.GameOverWon):
                default:

                    //draw only HPs of actors. we may just want this to be seen always, as a default
                    break;
            }

            int counter = 0;
            foreach (CombatActors ca in FoeParty)
            {
                ca.DrawOrderCounter = counter;
                ca.Draw(Game._spriteBatch);
                counter++;
            }
            ////rough numbers, temporary setup
            Game._spriteBatch.DrawRectangle(CombatGUI.PrimaryContainer.X, CombatGUI.PrimaryContainer.Y, CombatGUI.PrimaryContainer.Width, CombatGUI.PrimaryContainer.Height, Color.White);
            //CombatGUI.fightButton.draw(Game._spriteBatch);
            //CombatGUI.fleeButton.draw(Game._spriteBatch);
            //CombatGUI.bagButton.draw(Game._spriteBatch);

            Game._spriteBatch.End();
        }       


        /// <summary>
        /// this always needs to be run before initiating combat
        /// </summary>
        /// <param name="_enemiesFromEncounter"></param>
        /// <param name="_previousScreen"></param>
        internal void SetCombatActorsAndScreen(List<CombatActors> _enemiesFromEncounter, Screen _previousScreen)
        {
            PlayerParty = RpgPlayer.PlayerCurrentParty;
            PlayerParty.Add(RpgPlayer.PlayerCombatActor);
            FoeParty = _enemiesFromEncounter;
            returnToThisScreen = _previousScreen;
        }

        /// <summary>
        /// this is the primary way to return to prior location
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="_screenManager"></param>
        internal static void ChangeBackScreen(GraphicsDevice graphics, ScreenManager _screenManager)
        {
            _screenManager.CloseScreen();
            _screenManager.ShowScreen(returnToThisScreen, new FadeTransition(graphics, Color.Black, 0.5f));
        }

    }

    public static class GumCombatGui
    {
        public static Panel PrimaryPanel;
        
        public static void Load()
        {
            PrimaryPanel = new Panel();
            PrimaryPanel.AddToRoot();
        }
    }

    #region combat gui
    public static class CombatGUI
    {
        public static Rectangle PrimaryContainer = new(Globals.fontSize, Globals.TileSize, (int)(Globals.TileSize * 1.5f), Globals.TileSize * 2);
        public static CombatButton fightButton, fleeButton, bagButton;
        internal static int indexer = 0;
        public static CombatButton[] buttons;

        public static void Load()
        {
            fightButton = new CombatButton("Fight", 1);
            fleeButton = new CombatButton("Flee", 2);
            bagButton = new CombatButton("Bag", 3);
            buttons = [fightButton, fleeButton, bagButton];
        }

        public static void Update()
        {
            //this should handle the state machine



            //bool flowControl = IndexThruOptions();
            //if (!flowControl)
            //{
            //    return;
            //}
        }

        private static CombatButton IndexThruOptions()
        {
            if (Globals.keyb.WasKeyPressed(Keys.S) || Globals.keyb.WasKeyPressed(Keys.Down))
                indexer++;
            if (Globals.keyb.WasKeyPressed(Keys.W) || Globals.keyb.WasKeyPressed(Keys.Up))
                indexer--;

            foreach (CombatButton button in buttons)
            {
                if (button == null)
                    return null;

                if (indexer == Array.IndexOf(buttons, button))
                    button.color = Color.Red;
                else
                    button.color = Color.White;
            }

            if (Globals.keyb.WasKeyPressed(Keys.E))
            {

            }

            return null;
        }
    }
    #endregion

    #region singular button
    public class CombatButton : AnyObject
    {
        public string Text { get; set; }

        public CombatButton()
        {
            color = Color.White;
        }

        public CombatButton(string _text, int position)
        {
            color = Color.White;
            Text = _text;
            rectangle = new Rectangle
            {
                X = CombatGUI.PrimaryContainer.X + Globals.fontSize,
                Y = CombatGUI.PrimaryContainer.Y + (Globals.fontSize * position * 2),
                Width = 5 * Globals.fontSize,
                Height = Globals.fontSize * 2
            };

            absolutePosition = new((int)rectangle.X, (int)rectangle.Y, 0);
        }

        public void update()
        {

        }

        public void draw(SpriteBatch sb)
        {
            //sb.DrawRectangle()
            sb.DrawRectangle(rectangle, color);
            sb.DrawString(Globals.font, Text, new(rectangle.X+Globals.fontSize, rectangle.Y), color);
        }
    }
    #endregion
}
