using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using SupremeBroccoli.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremeBroccoli.Screens.Towns
{
    internal class Town_2 : GameScreen
    {
        #region local variables and screen constructor
        private new Game1 Game => (Game1)base.Game;
        MapBuilder mapTopLayer, mapBottomLayer, mapBlockerLayer;
        QuestSystem town_2_quest;
        CustomGuiGroup town_2_gui;
        //town destroyed by the dragon
        public Town_2(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }
        #endregion local variables and screen constructor
        public override void LoadContent()
        {
            base.LoadContent();
            Globals.Load();
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);

            //non-work-pc
            mapBlockerLayer = new MapBuilder(ConfigStrings.town_2_blocker, 60, 40);
            mapBottomLayer = new MapBuilder(ConfigStrings.town_2_bottom, 60, 40);
            mapTopLayer = new MapBuilder(ConfigStrings.town_2_top, 60, 40);

        }
        public override void Draw(GameTime gameTime)
        {
            //throw new NotImplementedException();
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);
            //mapBlockerLayer.DrawMapFromList(Game._spriteBatch);

            //town_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            //Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Route_1, Color.White);

            //if (town_1_gui != null)
            //    town_1_gui.draw(Game._spriteBatch);

            Game._spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            RpgPlayer.PlayerOverworld.DetectCollision(mapBlockerLayer);
            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);
            //town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);

            //town_1_gui.update(gameTime);
            //foreach (var t in town_1_quest.objectives)
            //{
            //    t.isPlayerInteracting(town_1_gui);
            //}
            //foreach (var t in town_1_quest.CurrentQuest.KvpQuests.Values)
            //{
            //    t.isPlayerInteracting(town_1_gui);
            //}

            //GoToRoute_1();

            Globals.MainCamera.LookAt(RpgPlayer.PlayerOverworld.Position);
            Globals.LockEKey = false;
        }
    }
}
