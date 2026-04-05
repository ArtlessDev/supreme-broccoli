using Assimp;
using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SupremeBroccoli.Screens.Towns
{
    //town where the player begins
    public class Town_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        MapBuilder mapTopLayer, mapBottomLayer, mapBlockerLayer;
        QuestSystem town_1_quest;

        public Town_1(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }

        public override void LoadContent()
        {

            base.LoadContent();
            //_font = Content.Load<SpriteFont>("coolvetica");
            //_titlePosition = new Vector2(100, 50);

            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);
            //RpgPlayer.PlayerOverworld.Position = Game.startingPosition;

            mapBlockerLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_blocker_layer.csv", 20, 20);
            mapBottomLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_bottom_layer.csv", 20, 20);
            mapTopLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_top_layer.csv", 20, 20);
            town_1_quest = new QuestSystem(@".\Content\Quests\quest_1.json", Atlases.beastiaryDexAtlas);
            //town_1_quest = new QuestSystem(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Core\Quests\quest_1.json", Atlases.beastiaryDexAtlas);

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);
            //mapBlockerLayer.DrawMapFromList(Game._spriteBatch);

            town_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Route_1, Color.White);

            Game._spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            
            RpgPlayer.PlayerOverworld.DetectCollision(mapBlockerLayer);
            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);
            town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);
            
            GoToRoute_1();

            Globals.MainCamera.LookAt(RpgPlayer.PlayerOverworld.Position);
        }

        Rectangle To_Route_1 = new Rectangle(12 * Globals.TileSize, 18 * Globals.TileSize, 4 * Globals.TileSize, 2*Globals.TileSize);
        Rectangle To_Route_3 = new Rectangle();

        public void GoToRoute_1()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Route_1))
            {
                int x = 4 * Globals.TileSize, 
                    y = 2 * Globals.TileSize;
                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.ShowScreen(new Routes.Route_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }

        public void GoToRoute_3()
        {
            //TODO set correct position 
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Route_1))
                ScreenManager.ShowScreen(new Routes.Route_3(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
        }
    }
}
