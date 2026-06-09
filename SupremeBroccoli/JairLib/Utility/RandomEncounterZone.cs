using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace JairLib.Utility
{
    public class RandomEncounterZone: AnyObject
    {
        #region variables and constructor
        public bool isPlayerInZone = false;
        public byte areWeEncounteringWithThis;
        public int seconds;
        public bool runEncounterFlag = false;
        public bool generateNewEncounterLock = false;
        public int encounterCooldown = 5;
        public System.Timers.Timer encounterTimer;

        
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
        #endregion

        #region load/draw/update
        public void Load()
        {
            encounterTimer = new System.Timers.Timer(3000f);
            //encounterTimer.Elapsed += DisableTimer;
            encounterTimer.AutoReset = true;
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

            if (!isPlayerInZone)
            {
                encounterTimer.Enabled = false;
                encounterTimer.Stop();
                return;
            }

            if (isPlayerInZone && !encounterTimer.Enabled)
            {
                encounterTimer.Start();
                encounterTimer.Enabled = true;
            }
        }
        #endregion

        #region timer handling
        private void DisableTimer(object? sender, ElapsedEventArgs e)
        {
            if (!isPlayerInZone)
            {
                encounterTimer.Enabled = false;
                return;
            }

            areWeEncounteringWithThis = RollForByte();
            Debug.WriteLine("timer elapsed");
        }

        public int encounterThreshold = 8;

        public bool TryForEncounter(GameTime gameTime)
        {
            //if (seconds + encounterCooldown >= gameTime.TotalGameTime.Seconds
            //    || generateNewEncounterLock
            //    || RpgPlayer.PlayerOverworld.state != PlayerState.Walking)
            //{
            //    return false;
            //}


            //Debug.WriteLine(@"trying for encounter: " + areWeEncounteringWithThis);
            Debug.WriteLine($"threshhold: {encounterThreshold}| rolled: {areWeEncounteringWithThis}");
            if (areWeEncounteringWithThis % encounterThreshold == 0 && generateNewEncounterLock)
            {
                //seconds = gameTime.TotalGameTime.Seconds;
                //runEncounterFlag = true;
                generateNewEncounterLock = false;
                encounterThreshold = 2;
                
                return true;
            }
            else
            {
                encounterThreshold += 2;
            }

            return false;
        }

        public byte RollForByte()
        {
            //if (generateNewEncounterLock) //true = on
            //    return 1;

            byte toReturn = (byte)Random.Shared.Next(255);

            generateNewEncounterLock = true;

            return Byte.Clamp(toReturn, 0, 255);
        }
        #endregion 

        private bool checkPlayerInZone()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(rectangle))
                return true;
            return false; 
        }
    }
}
