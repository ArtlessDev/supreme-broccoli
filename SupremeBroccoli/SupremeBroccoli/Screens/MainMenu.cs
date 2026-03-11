using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using JairLib.Utility;
using Microsoft.Xna.Framework.Graphics;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib;
using Microsoft.Xna.Framework.Input;
using System;

namespace SupremeBroccoli.Screens
{
    public class MainMenu : GameScreen
    {
        CircleF circle;
        private new Game1 Game => (Game1)base.Game;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            circle = new CircleF(new(500,500), 64);
        }
        int slider = 0;
        bool goingUpFlag = false;
        Color color = Color.White;
        int goalToHit = Random.Shared.Next(20, 80);
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            Game._spriteBatch.DrawCircle(circle, (int)circle.Radius, color);
            Game._spriteBatch.DrawString(Globals.font, slider.ToString(), circle.Center, color);
            Game._spriteBatch.DrawString(Globals.font, goalToHit.ToString(), new(0,0), color);

            Game._spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            Globals.Update(gameTime);

            if (slider == 0)
                goingUpFlag = true;
            else if (slider == 100)
                goingUpFlag = false;


            if (goingUpFlag)
                slider++;
            else slider--;
            
            circle.Center.X = (slider * 10) + 500;

            if (Globals.keyb.WasKeyPressed(Keys.Space))
            {
                if(slider == goalToHit)
                {
                    color = Color.Green;
                    return;
                }

                for (int i = 1; i < 5; i++)
                {
                    if (slider == goalToHit+i || slider == goalToHit - i)
                    {
                        color = Color.Yellow;
                        return;
                    }
                }


                for (int i = 5; i < 10; i++)
                {
                    if (slider == goalToHit + i || slider == goalToHit - i)
                    {
                        color = Color.Red;
                        return;
                    }
                }
            }
        }
    }
}
