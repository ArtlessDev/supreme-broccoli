using Gum;
using Gum.Forms.Controls;
using JairLib;
using JairLib.CombatSimulator;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using SupremeBroccoli.Screens.Towns;
using System;

namespace SupremeBroccoli.Screens
{

    /// <summary>
    /// we can clear this all out. CombatSimulator should have all the working changes
    /// </summary>
    public class MainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        internal TitleScreen _titleScreen { get; set; }

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            _titleScreen.AddToRoot();

            _titleScreen.ButtonStandardInstance.Click += (s, e) =>
            {
                //Game.screenManager.LoadScreen(new Town_1(Game));
                int x = 20 * Globals.TileSize,
                    y = (24 * Globals.TileSize) - Globals.TileSize;

                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new Towns.Town_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            };

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            Game.GumUI.Draw();
            
            Game._spriteBatch.End();

        }
        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            Game.GumUI.Update(gameTime);
        }
    }

}

