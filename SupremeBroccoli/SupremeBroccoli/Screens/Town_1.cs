using Assimp;
using JairLib;
using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SupremeBroccoli.Screens
{
    internal class Town_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private PlayerOverworld playerOverworld;
        MapBuilder mapTopLayer, mapBottomLayer;
        QuestSystem town_1_quest;

        public Town_1(Game game) : base(game)
        {
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

            mapBottomLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_bottom_layer.csv",20, 20);
            mapTopLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_top_layer.csv", 20, 20);
            town_1_quest = new QuestSystem(@".\Content\Quests\quest_1.json", Atlases.beastiaryDexAtlas);
            //town_1_quest = new QuestSystem(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Core\Quests\quest_1.json", Atlases.beastiaryDexAtlas);

            playerOverworld.Position = new Vector2(mapTopLayer.Spaces[0].rectangle.X, mapTopLayer.Spaces[0].rectangle.Y);

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);

            town_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, playerOverworld);

            playerOverworld.Draw(Game._spriteBatch);

            //Game._spriteBatch.DrawString(_font, "Main Menu", _titlePosition, Color.White);
            //Game._spriteBatch.DrawString(_font, "Press Enter To Play", new Vector2(100, 100), Color.White);
            Game._spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            playerOverworld.Update(gameTime, mapTopLayer);

            town_1_quest.Update(gameTime, playerOverworld);

            Globals.MainCamera.LookAt(playerOverworld.Position);
            //throw new NotImplementedException();
        }
    }
}
