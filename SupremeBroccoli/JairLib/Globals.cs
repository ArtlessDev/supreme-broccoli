using JairLib.FootballBoilerPlate;
using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;

namespace JairLib
{
    public static class Globals
    {
        public static ContentManager GlobalContent;

        public static int mapWidth = 20;
        public static int mapHeight = 40;
        public static int TileSize = 128;

        public static Texture2D gameTilePrototypeSet, playerPrototypeSet;
        public static Texture2DAtlas gameTilePrototypeAtlas, playerPrototypeAtlas;
        public static int PUZZLE_SIZE = 25;
        public static int PUZZLE_SIZE_ADJUSTED = (int)(2 + Math.Sqrt(PUZZLE_SIZE)) * (int)(2 + Math.Sqrt(PUZZLE_SIZE));
        public static SpriteSheet spriteSheet, gameObjectSheet;
        public static List<TileSpace> tileSpaces;


        public static KeyboardStateExtended keyb;
        public static Rectangle mouseRect;
        public static MouseStateExtended mouseState;

        public static string seed;
        public static string[] gridSeed;
        public static SpriteFont font;
        public static int currentLevel = 1;
        public static int CountOfTiles = 8;

        public static int ViewportHeight = 720;//480;
        public static int ViewportWidth = 1280;//800;
        public static OrthographicCamera MainCamera;

        public static Vector2 STARTING_POSITION = new Vector2(128,128);

        public static void Load()
        {
            gameTilePrototypeSet = GlobalContent.Load<Texture2D>("game_tileset_prototype");
            gameTilePrototypeAtlas = Texture2DAtlas.Create("gameTileMapPrototype", gameTilePrototypeSet, TileSize, TileSize);
            
            playerPrototypeSet = GlobalContent.Load<Texture2D>("player_spritesheet");
            playerPrototypeAtlas = Texture2DAtlas.Create("playerTileMapPrototype", playerPrototypeSet, TileSize, TileSize);

            //font = GlobalContent.Load<SpriteFont>("File");
            tileSpaces = new List<TileSpace>();
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardExtended.Update();
            keyb = KeyboardExtended.GetState();
            MouseExtended.Update();
            mouseState = MouseExtended.GetState();
        }

        /// <summary>
        /// from space invaders game
        /// NOT USED IN THIS GAME
        /// </summary>
        /// <param name="player"></param>
        public static void CamMoveHorizontal(PlayerOverworld player)
        {
            MainCamera.Move(new(player.rectangle.X - 100, player.rectangle.Y - 100));
        }

        public static void CamMove(Rectangle player)
        {
            var playerFocusX = player.X - (ViewportWidth / 2) + 16;
            var playerFocusY = player.Y - (ViewportHeight / 2) + 16;
            MainCamera.Position = new(playerFocusX, playerFocusY);
        }

        /// <summary>
        /// NOT USED IN THIS GAME
        /// </summary>
        private static void GenerateSeed()
        {
            if (keyb.WasKeyPressed(Keys.Enter))
            {
                Globals.tileSpaces.Clear();
                seed = SeedBuilder.TheSeedGetsSomeOnes(seed);
                SeedBuilder.MaketheSeedGrid(gridSeed);
                //SeedBuilder.MakeSeedGridFromList();
                gridSeed = SeedBuilder.SplitTheSeedToAGrid(seed);
            }
        }

        /// <summary>
        /// NOT USED IN THIS GAME
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_spriteBatch"></param>
        public static void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {

            if (!string.IsNullOrEmpty(seed))
            {
                _spriteBatch.DrawString(font, seed, new Vector2(8, 8), Color.DarkGreen);
            }

            _spriteBatch.DrawString(font, "press enter to generate a new seed", new Vector2(0, 32), Color.White);

            //SeedBuilder.DrawtheSeedGrid(_spriteBatch, gridSeed);
            //SeedBuilder.DrawSeedGridFromList(_spriteBatch, map);
        }

        public static bool CheckMouseIntersection(AnyObject obj)
        {
            return mouseRect.Intersects(new Rectangle((int)obj.absolutePosition.X, (int)obj.absolutePosition.Y, TileSize, TileSize));
        }

        public static void IsMouseHovering(FootballPlayer obj)
        {
            if (CheckMouseIntersection(obj))
            {
                obj.color = Color.White;
            }
            else
            {
                obj.color = obj.reservedColor;
            }
        }
    }
}
