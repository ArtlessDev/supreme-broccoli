using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CombatSimulator
{
    public class EnemyBee : CombatActors
    {
        public EnemyBee()
        {
            Name = "bee";
            Moveset = [MoveList.Punch];
            MaximumHealth = 5;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "bee";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/BEES");
            rectangle = new(256, 128, 256, 256);
            DrawOrderCounter = 0;
        }
        public EnemyBee(int _drawOrderCounter)
        {
            Name = "bee";
            Moveset = [MoveList.Punch];
            MaximumHealth = 5;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "bee";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/BEES");
            rectangle = new(256 * _drawOrderCounter, 128, 256, 256);
            DrawOrderCounter = _drawOrderCounter;
        }
        public static void AdjustRectangle()
        {

        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);

            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X+64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);

            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);

            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
    public class EnemySlime : CombatActors
    {
        public EnemySlime()
        {
            Name = "slime";
            Moveset = [MoveList.Punch];
            MaximumHealth = 10;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "slime";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/squishy");
            rectangle = new(256, 128, 256, 256);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);

            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X + 64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);

            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);

            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
    public class EnemyBigRat : CombatActors
    {
        public EnemyBigRat()
        {
            Name = "big rat";
            Moveset = [MoveList.Punch];
            MaximumHealth = 15;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "big rat";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/bigrat");
            rectangle = new(256, 128, 256, 256);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);
            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X + 64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);
            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);
            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
    public class EnemyMagicMouse : CombatActors
    {
        public EnemyMagicMouse()
        {
            Name = "magic mouse";
            Moveset = [MoveList.Punch];
            MaximumHealth = 20;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "magic mouse";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/magicmouse");
            rectangle = new(256, 128, 256, 256);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);
            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X + 64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);
            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);
            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
    public class EnemySpearRat : CombatActors
    {
        public EnemySpearRat()
        {
            Name = "spear rat";
            Moveset = [MoveList.Punch];
            MaximumHealth = 25;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "spear rat";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/olive_spear_rat");
            rectangle = new(256, 128, 256, 256);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);
            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X + 64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);
            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);
            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
    public class EnemyMouseQueen : CombatActors
    {
        public EnemyMouseQueen()
        {
            Name = "mouse queen";
            Moveset = [MoveList.Punch];
            MaximumHealth = 30;
            CurrentHealth = MaximumHealth;
            Speed = 5;
            Attack = 10;
            Defense = 5;
            SpecialDefense = 5;
            Luck = 10;
            Accuracy = 10;
            Evasiveness = 10;
            identifier = "mouse queen";
            color = Color.White;
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/ratqueen");
            rectangle = new(256, 128, 256, 256);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((256 * DrawOrderCounter), 0), 256);
            Vector2 circleCenter = useAsBoundsRect.Center;
            circleCenter.SetX(circleCenter.X + 64);// screeen margin
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);
            this.rectangle = new Rectangle((int)circleCenter.X, (int)circleCenter.Y, this.rectangle.Width, this.rectangle.Height);
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);
            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
}
