using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace JairLib.CombatSimulator
{
    public class SpinnerMinigame
    {
        public SpinnerMinigameSpace spinnerMinigameSpace;
        public CircleF SpinnerCircle, PlayerInputCircle, CenteredCircle;
        public Texture2D texture2D, pointer2D;
        public Vector2 initPoint = new Vector2();
        public Color color = Color.White;
        public float smartAngle;

        public double radius = 180;
        public double angle = 0.0; // In radians
        public double speed = 0.1; // Speed of rotation

        public SpinnerMinigame()
        {

            initPoint = new Vector2(Globals.MainCamera.Center.X, Globals.MainCamera.Center.Y);

            texture2D = Globals.GlobalContent.Load<Texture2D>("spinner");
            pointer2D = Globals.GlobalContent.Load<Texture2D>("pointer");

            CenteredCircle = new(initPoint, (float)radius);
            smartAngle = Random.Shared.NextAngle();
            spinnerMinigameSpace = new SpinnerMinigameSpace(CenteredCircle);

            SpinnerCircle = new CircleF(CenteredCircle.Center, 180);
            PlayerInputCircle = new CircleF(CenteredCircle.Center, 30);
            
        }
        public void Draw(GameTime gameTime, SpriteBatch _sb)
        {
            //this draws the spinner texture based off of the main circle boundary
            _sb.Draw(texture2D, CenteredCircle.BoundingRectangle.ToRectangle(), color);
            spinnerMinigameSpace.Draw(gameTime, _sb);

            var updatedAngle = (float)angle + (MathF.PI / 2);
            _sb.Draw(pointer2D, PlayerInputCircle.Center, null, Color.Blue, updatedAngle, new Vector2(pointer2D.Width / 2f, pointer2D.Height / 2f), 1f, SpriteEffects.None, 1f);
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
        public void Update(GameTime gameTime)
        {
            UpdatePosition();
            spinnerMinigameSpace.Update(gameTime, PlayerInputCircle);
        }
    }
    public class SpinnerMinigameSpace
    {
        Texture2D texture2D, pointer2D, poison2D;
        Color color;
        float smartAngle;
        CircleF CenteredCircle;
        Vector2 smartPosition;
        public SpinnerMinigameSpace(CircleF _centeredCircle)
        {
            //smartAngle = MathF.PI;
            CenteredCircle = _centeredCircle;
            color = Color.White;
            smartAngle = Random.Shared.NextAngle();
            texture2D = Globals.GlobalContent.Load<Texture2D>("spinner");
            pointer2D = Globals.GlobalContent.Load<Texture2D>("pointer");
            poison2D = Globals.GlobalContent.Load<Texture2D>("poison_space");
        }
        public SpinnerMinigameSpace(CircleF _centeredCircle, float _angle)
        {
            //smartAngle = MathF.PI;
            CenteredCircle = _centeredCircle;
            color = Color.Purple;
            smartAngle = _angle;
            texture2D = Globals.GlobalContent.Load<Texture2D>("spinner");
            pointer2D = Globals.GlobalContent.Load<Texture2D>("pointer");
            poison2D = Globals.GlobalContent.Load<Texture2D>("poison_space");
        }
        public void Update(GameTime gameTime, CircleF _playerInput)
        {

            double newX = CenteredCircle.Radius * Math.Cos(smartAngle);
            double newY = CenteredCircle.Radius * Math.Sin(smartAngle);

            newX += CenteredCircle.Center.X;
            newY += CenteredCircle.Center.Y;

            smartPosition = new((float)newX, (float)newY);

            if (Globals.keyb.WasKeyPressed(Keys.Space) && CircleF.Intersects(_playerInput, new CircleF(smartPosition, texture2D.Width)))
            {
                color = Color.MediumPurple;
            }
            else if (Globals.keyb.WasKeyPressed(Keys.Space) && !_playerInput.Intersects(pointer2D.Bounds))
            {
                color = Color.Purple;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch _sb)
        {
            _sb.Draw(poison2D, smartPosition, null, color, smartAngle + (MathF.PI / 2), new Vector2(poison2D.Width / 2f, poison2D.Height / 2f), 1f, SpriteEffects.None, 1f);
        }
    }
}
