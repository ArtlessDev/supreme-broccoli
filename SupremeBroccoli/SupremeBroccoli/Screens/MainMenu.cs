using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using JairLib.Utility;
using Microsoft.Xna.Framework.Graphics;
using Gum;
using System;
using Gum.Forms.Controls;
using System.ComponentModel;
using Microsoft.Xna.Framework.Input;

namespace SupremeBroccoli.Screens
{
    public class MainMenu : GameScreen
    {
        CircleF circle, player_input;
        private new Game1 Game => (Game1)base.Game;

        Rectangle CenteredRectangle = new Rectangle();
        Rectangle testSpaceRectangle = new Rectangle();

        GumService GumUI => GumService.Default;
        StackPanel mainMenuPanel;
        Vector2 initPoint = new Vector2();
        Vector2 poisonCenter;

        // circle variables
        int slider = 0;
        bool goingUpFlag = false;
        Color color = Color.White;
        int goalToHit = Random.Shared.Next(20, 80);

        double radius = 500;
        double centerX = 500;
        double centerY = 500.0;
        double angle = 0.0, stillSpaceAngle = 0.0; // In radians
        double speed = 0.1; // Speed of rotation

        Texture2D texture2D, pointer2D, poison2D;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            //circle.Position = new Vector2(500, 500);
            //initPoint = new(circle.Center.X, circle.Center.Y);
            initPoint = new Vector2(Globals.ViewportWidth * .5f, Globals.ViewportHeight * .5f);

            texture2D = Globals.GlobalContent.Load<Texture2D>("spinner");
            pointer2D = Globals.GlobalContent.Load<Texture2D>("pointer");
            poison2D = Globals.GlobalContent.Load<Texture2D>("poison_space");
            
            CenteredRectangle = new((int)Globals.MainCamera.Center.X, (int)Globals.MainCamera.Center.Y, 64, 64);

            stillSpaceAngle = (Math.PI * 45) / 180;

            Vector2 circleCenter = new(CenteredRectangle.Center.X, CenteredRectangle.Center.Y);

            circle = new CircleF(circleCenter, 180);
            player_input = new CircleF(circleCenter, 30);
            //CenteredRectangle.Center = new Point((int)initPoint.X, (int)initPoint.Y);


            #region gui
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
            #endregion gui
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            //this draws the spinner texture based off of the main circle boundary
            Game._spriteBatch.Draw(texture2D, circle.BoundingRectangle.ToRectangle(), color);

            Game._spriteBatch.Draw(poison2D, new(testSpaceRectangle.X, testSpaceRectangle.Y), null, Color.Purple, (float)stillSpaceAngle, new Vector2(poison2D.Width / 2f, poison2D.Height / 2f), 1f, SpriteEffects.None, 1f);
            
            //uses the texture but is offset by some weird amount
            Game._spriteBatch.Draw(pointer2D, player_input.Center, null, Color.Blue, (float)angle, new Vector2(pointer2D.Width / 2f, pointer2D.Height / 2f), 1f, SpriteEffects.None, 1f);
            
            
            //Game._spriteBatch.Draw(texture2D, circle.Center, circle.BoundingRectangle.ToRectangle(), color, 3.14f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //Game._spriteBatch.DrawRectangle(CenteredRectangle, color);
            //Game._spriteBatch.DrawString(Globals.font, slider.ToString(), circle.Center, color);
            //Game._spriteBatch.DrawString(Globals.font, goalToHit.ToString(), new(0,0), color);

            //GumUI.Draw();

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

            //double newX = centerX + radius * Math.Cos(angle);
            //double newY = centerY + radius * Math.Sin(angle);
            double newX = circle.Radius * Math.Cos(angle);
            double newY = circle.Radius * Math.Sin(angle);

            player_input.Center.X = CenteredRectangle.Center.X + (float)newX;
            player_input.Center.Y = CenteredRectangle.Center.Y + (float)newY;
            
            //CenteredRectangle.X = (int)newX;
            //CenteredRectangle.Y = (int)newY;
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

            UpdatePosition();

            double newX = circle.Radius * Math.Cos(3.14d*.25);
            double newY = circle.Radius * Math.Cos(3.14d * .25);

            testSpaceRectangle.X = (int)(CenteredRectangle.Center.X + newX);
            testSpaceRectangle.Y = (int)(CenteredRectangle.Center.Y + newY);
            testSpaceRectangle.Width = 64;
            testSpaceRectangle.Height = 64;
            //this some bullshit to handle the player inpu for the minigame
            if (Globals.keyb.WasKeyPressed(Keys.Space) && player_input.Intersects(testSpaceRectangle))
            {
                color = Color.Green;
            }
            else if (Globals.keyb.WasKeyPressed(Keys.Space) && !player_input.Intersects(testSpaceRectangle))
            {
                color = Color.White;
            }
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
