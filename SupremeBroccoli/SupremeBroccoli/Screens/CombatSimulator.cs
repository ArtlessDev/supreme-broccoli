using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended;
using JairLib.Utility;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using JairLib.CombatSimulator;
using Gum.Forms.Controls;

namespace SupremeBroccoli.Screens
{
    public class CombatSimulator : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public static List<CombatActors> PlayerParty = new List<CombatActors>();
        public static List<CombatActors> FoeParty = new List<CombatActors>();

        public CombatSimulator(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }

        public override void LoadContent()
        {
            CombatGUI.Load();
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);


            if (Globals.keyb.WasKeyPressed(Keys.Enter))
                ScreenManager.ShowScreen(new Routes.Route_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));


            CombatGUI.Update();
            //CombatGUI.fleeButton.update();
            //CombatGUI.bagButton.update();

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Game._spriteBatch.Begin();

            //rough numbers, temporary setup
            Game._spriteBatch.DrawRectangle(CombatGUI.PrimaryContainer.X, CombatGUI.PrimaryContainer.Y, CombatGUI.PrimaryContainer.Width, CombatGUI.PrimaryContainer.Height, Color.White);
            CombatGUI.fightButton.draw(Game._spriteBatch);
            CombatGUI.fleeButton.draw(Game._spriteBatch);
            CombatGUI.bagButton.draw(Game._spriteBatch);

            Game._spriteBatch.End();
        }       

        public static void SetCombatants(List<CombatActors> _playerParty, List<CombatActors> _foeParty)
        {
            PlayerParty = _playerParty;
            FoeParty = _foeParty;
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
            if (Globals.keyb.WasKeyPressed(Keys.S) || Globals.keyb.WasKeyPressed(Keys.Down))
                indexer++;
            if (Globals.keyb.WasKeyPressed(Keys.W) || Globals.keyb.WasKeyPressed(Keys.Up))
                indexer--;

                foreach (CombatButton button in buttons)
                {
                    if (button == null)
                        return;

                    if (indexer == Array.IndexOf(buttons, button))
                        button.color = Color.Red;
                    else 
                        button.color = Color.White;
                }
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
