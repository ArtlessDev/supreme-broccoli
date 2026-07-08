using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using JairLib.Utility;
using Microsoft.Xna.Framework.Graphics;
using Gum;
using System;
using Gum.Forms.Controls;
using System.ComponentModel;

namespace SupremeBroccoli.Screens
{
    public class MainMenu : GameScreen
    {
        CircleF circle;
        private new Game1 Game => (Game1)base.Game;



        // circle variables
        int slider = 0;
        bool goingUpFlag = false;
        Color color = Color.White;
        int goalToHit = Random.Shared.Next(20, 80);

        double radius = 300;
        double centerX = 500;
        double centerY = 500.0;
        double angle = 0.0; // In radians
        double speed = 0.1; // Speed of rotation



        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            circle = new CircleF(new(500,500), 64);

            mainMenuPanel = new StackPanel();
            mainMenuPanel.Width = 400;
            
            mainMenuPanel.IsVisible = true;
            mainMenuPanel.AddToRoot();
            

            Button startButton = new Button();
            startButton.X = 100;
            startButton.Y = 100;
            startButton.Text = "Start Game";
            TextBox nameInput = new TextBox();
            mainMenuPanel.AddChild(nameInput);
            Label nameLabel = new Label();
            nameLabel.Text = "Enter your name:";
            nameLabel.X = 200;
            nameLabel.Y = 200;
            nameLabel.Width = 200;
            nameLabel.Height = 500;
            
            mainMenuPanel.AddChild(nameLabel);
            mainMenuPanel.AddChild(startButton);
        }
        GumService GumUI => GumService.Default;
        StackPanel mainMenuPanel;
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            Game._spriteBatch.DrawCircle(circle, 12, color);
            Game._spriteBatch.DrawString(Globals.font, slider.ToString(), circle.Center, color);
            Game._spriteBatch.DrawString(Globals.font, goalToHit.ToString(), new(0,0), color);

            GumUI.Draw();

            if (mainMenuPanel.IsVisible)
                {
                    // Begin the sprite batch to prepare for rendering.

                    // The color to use for the drop shadow text.
                    Color dropShadowColor = Color.Black * 0.5f;


                    // Always end the sprite batch when finished.
                    // Game._spriteBatch.End();
                }

            Game._spriteBatch.End();

        }

        internal void UpdatePosition()
        {
            angle += speed;
            if (angle > Math.PI * 2) 
                angle = 0;

            double newX = centerX + radius * Math.Cos(angle);
            double newY = centerY + radius * Math.Sin(angle);

            circle.Center.X = (float)newX;
            circle.Center.Y = (float)newY;
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            GumUI.Update(gameTime);

            // if (slider == 0)
            //     goingUpFlag = true;
            // else if (slider == 200)
            //     goingUpFlag = false;


            // if (goingUpFlag)
            //     slider++;
            // else slider--;

            // UpdatePosition();

            
            //this some bullshit to handle the player inpu for the minigame
            // if (Globals.keyb.WasKeyPressed(Keys.Space))
            // {
            //     if (slider == goalToHit)
            //     {
            //         color = Color.Green;
            //         return;
            //     }

            //     for (int i = 1; i < 5; i++)
            //     {
            //         if (slider == goalToHit + i || slider == goalToHit - i)
            //         {
            //             color = Color.Yellow;
            //             return;
            //         }
            //     }


            //     for (int i = 5; i < 10; i++)
            //     {
            //         if (slider == goalToHit + i || slider == goalToHit - i)
            //         {
            //             color = Color.Red;
            //             return;
            //         }
            //     }
            // }

            

        }
    }
}
