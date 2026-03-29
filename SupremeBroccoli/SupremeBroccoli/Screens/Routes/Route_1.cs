using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
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
        private PlayerOverworld playerOverworld;
        private SpriteFont _font;
        private Vector2 _titlePosition;
        private List<TileSpace> roomMysterySpaces;
        MapBuilder mapTopLayer, mapBottomLayer;

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
            Globals.Load();
            Atlases.Load();
            playerOverworld = new PlayerOverworld();
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);
            Globals.MainCamera.Position = playerOverworld.Position;

            mapBottomLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_bottom.csv", 60, 50);
            mapTopLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_top.csv", 60, 50);
            //town_1_quest = new QuestSystem(@".\Content\Quests\quest_1.json", Atlases.beastiaryDexAtlas);
            //town_1_quest = new QuestSystem(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Core\Quests\quest_1.json", Atlases.beastiaryDexAtlas);

            playerOverworld.Position = new Vector2(mapTopLayer.Spaces[0].rectangle.X, mapTopLayer.Spaces[0].rectangle.Y);

        }
        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
