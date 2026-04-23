using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace JairLib.Utility
{
    public class RandomEncounterZone: AnyObject
    {
        public bool isPlayerInZone = false;
        public byte areWeEncounteringWithThis;
        public int seconds;
        public bool runEncounterFlag = true;
        public bool generateNewEncounterLock = false;

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
            sb.Draw(texture.Texture, rectangle, Color.Aqua);//position, Color.Aqua, rotation, origin, scale, SpriteEffects.None, 1f);

        }
        public void Update(GameTime gameTime)
        {
            isPlayerInZone = checkPlayerInZone();

            if (!isPlayerInZone && !hasSecondPassed(gameTime) && !runEncounterFlag )
                return;

            if (generateNewEncounterLock)
                areWeEncounteringWithThis = RollForByte();

        }

        public bool tryForEncounter(GameTime gameTime)
        {
            if (seconds+3 >= gameTime.TotalGameTime.Seconds 
                || generateNewEncounterLock
                || RpgPlayer.PlayerOverworld.state != PlayerState.Walking)
            {
                return false;
            }

            if (areWeEncounteringWithThis % 16 == 0 && hasSecondPassed(gameTime))
            {
                seconds = gameTime.TotalGameTime.Seconds;
                runEncounterFlag = true;
                generateNewEncounterLock = false;

                return true;
            }


            return false;
        }

        public bool hasSecondPassed(GameTime gameTime)
        {
            if (seconds+3 != gameTime.TotalGameTime.Seconds)
                return true;
            return false;
        }

        private byte RollForByte()
        {
            if (generateNewEncounterLock) //true = on
                return 1;

            byte toReturn = (byte)Random.Shared.Next(255);

            generateNewEncounterLock = true;

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
