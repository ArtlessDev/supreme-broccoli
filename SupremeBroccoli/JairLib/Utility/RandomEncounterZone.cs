using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

namespace JairLib.Utility
{
    public class RandomEncounterZone: AnyObject
    {
        public bool isPlayerInZone = false;
        public RandomEncounterZone()
        {
            rectangle = new();
            texture = Atlases.tilesetAtlas[18];
        }
        
        public RandomEncounterZone(int x, int y, int width, int height)
        {
            texture = Atlases.tilesetAtlas[0];
            rectangle = new(x * Globals.TileSize, 
                y * Globals.TileSize, 
                width * Globals.TileSize, 
                height * Globals.TileSize);
        }

        public void Draw(SpriteBatch sb)
        {
            var rotation = 0f;
            var origin = new Vector2(0, 0);
            var position = new Vector2(rectangle.X, rectangle.Y);
            var scale = new Vector2(1f, 1f);
            //the size of the sprite will always match the size of the tile splitter 
            sb.Draw(texture, position, Color.Aqua, rotation, origin, scale, SpriteEffects.None, 1f);

        }
        public byte areWeEncounteringWithThis;
        public void Update(GameTime gameTime)
        {
            isPlayerInZone = checkPlayerInZone();

            if (isPlayerInZone && gameTime.ElapsedGameTime.Milliseconds == 0)
            {
                
                Debug.WriteLine("player is in the zone");
                areWeEncounteringWithThis = RollForByte();
            }
        }

        private byte RollForByte()
        {
            byte toReturn = 0;
            
            return Byte.Clamp(toReturn, 0, 255);
        }

        private bool checkPlayerInZone()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(rectangle))
                return true;
            return false; 
        }
    }
}
