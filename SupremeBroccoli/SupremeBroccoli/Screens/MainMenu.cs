using Gum;
using Gum.Forms.Controls;
using JairLib.CombatSimulator;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System;

namespace SupremeBroccoli.Screens
{

    /// <summary>
    /// we can clear this all out. CombatSimulator should have all the working changes
    /// </summary>
    public class MainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            Game._spriteBatch.End();

        }
        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            
        }
    }

}

