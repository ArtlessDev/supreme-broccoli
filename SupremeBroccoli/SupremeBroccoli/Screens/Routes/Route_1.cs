using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremeBroccoli.Screens.Routes
{
    internal class Route_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private SpriteFont _font;
        private Vector2 _titlePosition;
        private List<TileSpace> roomMysterySpaces;
        MapBuilder mapTopLayer, mapBottomLayer;
        RandomEncounterZone encounterZone;

        public Route_1(Game game) : base(game)
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

            //mapBottomLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_bottom.csv", 60, 50);
            //mapTopLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_top.csv", 60, 50);
            mapBottomLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_bottom.csv", 60, 50);
            mapTopLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_top.csv", 60, 50);
            //town_1_quest = new QuestSystem(@".\Content\Quests\quest_1.json", Atlases.beastiaryDexAtlas);
            //town_1_quest = new QuestSystem(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Core\Quests\quest_1.json", Atlases.beastiaryDexAtlas);
            encounterZone = new(3, 5, 10, 10);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);

            encounterZone.Draw(Game._spriteBatch);
            //route_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Town_1, Color.White);

            //Game._spriteBatch.DrawString(_font, "Main Menu", _titlePosition, Color.White);
            //Game._spriteBatch.DrawString(_font, "Press Enter To Play", new Vector2(100, 100), Color.White);
            Game._spriteBatch.End();

            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);

            //town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);
            encounterZone.Update(gameTime);
            //GoToRoute_1();
            GoToTown_1();
            GoToTown_2();

            Globals.MainCamera.LookAt(RpgPlayer.PlayerOverworld.Position);
        }
        Rectangle To_Town_1 = new Rectangle(2 * Globals.TileSize, 0 * Globals.TileSize, 6 * Globals.TileSize, 2 * Globals.TileSize);

        public void GoToTown_1()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Town_1))
            {
                int x = 12 * Globals.TileSize,
                    y = (18 * Globals.TileSize) - Globals.TileSize;
                RpgPlayer.PlayerOverworld.rectangle = new Rectangle(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                RpgPlayer.PlayerOverworld.Position = new(x, y);
                ScreenManager.ShowScreen(new Towns.Town_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }

        public void GoToTown_2()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Town_1))
            {
                ScreenManager.ShowScreen(new Towns.Town_2(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));

            }
        }
    }
}
