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
            CircleF useAsBoundsRect = new(new((512 * DrawOrderCounter), 0), 256);

            Vector2 circleCenter = useAsBoundsRect.Center;
            
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);

            //this.rectangle = BoundingRect;
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);

            var hpText = $"{Name}\nHP: {CurrentHealth}/{MaximumHealth}";
            Vector2 hpTextPosition = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            _spriteBatch.DrawString(Globals.stabilloFont, hpText, hpTextPosition, color);
        }
    }
}
