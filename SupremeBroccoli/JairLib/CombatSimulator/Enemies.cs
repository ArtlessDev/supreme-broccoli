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
            texture2D = Globals.GlobalContent.Load<Texture2D>("CombatSprites/BEES");
            rectangle = new(512, 512, 512, 512);

        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            CircleF useAsBoundsRect = new(new((512 * DrawOrderCounter), 0), 256);
            Rectangle BoundingRect = new((int)useAsBoundsRect.Center.X, (int)useAsBoundsRect.Center.Y, (int)useAsBoundsRect.BoundingRectangle.Height, (int)useAsBoundsRect.BoundingRectangle.Width);
            Vector2 circleCenter = useAsBoundsRect.Center;
            
            //Game._spriteBatch.Draw(ca.texture2D, circleCenter, Color.White);

            this.rectangle = BoundingRect;
            _spriteBatch.Draw(this.texture2D, this.rectangle, Color.White);

            _spriteBatch.DrawString(Globals.stabilloFont, hpText);
        }
    }
}
