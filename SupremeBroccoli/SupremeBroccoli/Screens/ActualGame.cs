using System;
using JairLib;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SupremeBroccoli.Screens
{
    public class ActualGame : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private SpriteFont _font;
        private Vector2 _titlePosition;
        public ActualGame(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Content.Load<SpriteFont>("Fonts/MenuFont");
            _titlePosition = new Vector2(100, 50);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Game._spriteBatch.Begin();
            
            //Game._spriteBatch.DrawString(_font, "Main Menu", _titlePosition, Color.White);
            //Game._spriteBatch.DrawString(_font, "Press Enter To Play", new Vector2(100, 100), Color.White);
            Game._spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(Globals.keyb.WasKeyPressed(Keys.H))
                ScreenManager.ShowScreen(new MainMenu(Game));
            
        }
    }
}
