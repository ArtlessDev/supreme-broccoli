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

        SpinnerMinigameSpace spinnerMinigameSpace;
        CircleF SpinnerCircle, PlayerInputCircle, CenteredCircle;
        Texture2D texture2D, pointer2D;
        Vector2 initPoint = new Vector2();
        Color color = Color.White;
        Vector2 smartPosition;
        float smartAngle;

        double radius = 180;
        double angle = 0.0; // In radians
        double speed = 0.1; // Speed of rotation

        GumService GumUI => GumService.Default;
        StackPanel mainMenuPanel;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {

            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            initPoint = new Vector2(Globals.MainCamera.Center.X/2, Globals.MainCamera.Center.Y/2);

            texture2D = Globals.GlobalContent.Load<Texture2D>("spinner");
            pointer2D = Globals.GlobalContent.Load<Texture2D>("pointer");

            CenteredCircle = new(initPoint, (float)radius);
            smartAngle = Random.Shared.NextAngle();
            spinnerMinigameSpace = new SpinnerMinigameSpace(CenteredCircle);

            SpinnerCircle = new CircleF(CenteredCircle.Center, 180);
            PlayerInputCircle = new CircleF(CenteredCircle.Center, 30);

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
            Game._spriteBatch.Draw(texture2D, CenteredCircle.BoundingRectangle.ToRectangle(), color);
            spinnerMinigameSpace.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.Draw(pointer2D, PlayerInputCircle.Center, null, Color.Blue, (float)angle + (MathF.PI / 2), new Vector2(pointer2D.Width / 2f, pointer2D.Height / 2f), 1f, SpriteEffects.None, 1f);

            //GumUI.Draw();

            Game._spriteBatch.End();

        }
        internal void UpdatePosition()
        {
            angle += speed;
            if (angle > Math.PI * 2)
                angle = 0;

            double newX = SpinnerCircle.Radius * Math.Cos(angle);
            double newY = SpinnerCircle.Radius * Math.Sin(angle);

            PlayerInputCircle.Center.X = CenteredCircle.Center.X + (float)newX;
            PlayerInputCircle.Center.Y = CenteredCircle.Center.Y + (float)newY;
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            GumUI.Update(gameTime);

            UpdatePosition();

            spinnerMinigameSpace.UpdateTargetPosition(gameTime);
            //spinnerMinigameSpace.UpdateTargetPosition(gameTime);
        }
    }

}

