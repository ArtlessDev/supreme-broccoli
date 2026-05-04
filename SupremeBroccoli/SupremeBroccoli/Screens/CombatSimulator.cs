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

namespace SupremeBroccoli.Screens
{
    public class CombatSimulator : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

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


            //throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }       
    }

    public static class CombatGUI
    {
        public static Rectangle PrimaryContainer  = new(Globals.fontSize, Globals.TileSize, (int)(Globals.TileSize * 1.5f), Globals.TileSize * 2);

        public static CombatButton fightButton, fleeButton, bagButton;

        public static void Load()
        {
            fightButton = new CombatButton("Fight", 1);
            fleeButton = new CombatButton("Flee", 2);
            bagButton = new CombatButton("Bag", 3);
        }
        //{ get; set; }

    }

    public class CombatButton
    {
        public Color color;
        public Rectangle Container { get; set; }
        public string Text { get; set; }

        public CombatButton()
        {
            color = Color.White;
        }

        public CombatButton(string _text, int position)
        {
            color = Color.White;
            Text = _text;
            Container = new Rectangle(CombatGUI.PrimaryContainer.X + Globals.fontSize, 
                CombatGUI.PrimaryContainer.Y + (Globals.fontSize * position * 2), 
                5 * Globals.fontSize,
                Globals.fontSize * 2);
        }

        public void update()
        {

        }

        public void draw(SpriteBatch sb)
        {
            sb.DrawRectangle(Container, color);
            sb.DrawString(Globals.font, Text, new(Container.X+Globals.fontSize, Container.Y), color);
        }
    }
}
