using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace SupremeBroccoli.Screens.Routes
{
    internal class Route_1 : GameScreen
    {
        #region local variables and screen constructor
        private new Game1 Game => (Game1)base.Game;
        MapBuilder mapTopLayer, mapBottomLayer, mapBlockerLayer; 
        RandomEncounterZone encounterZone;
        GameTime gameTimeLocal;
        bool switcher = false;
        Rectangle To_Town_1 = new Rectangle(2 * Globals.TileSize, 0 * Globals.TileSize, 6 * Globals.TileSize, 2 * Globals.TileSize);
        //Rectangle To_Town_2 = new Rectangle(2 * Globals.TileSize, 10* Globals.TileSize, 6 * Globals.TileSize, 2 * Globals.TileSize);
        Rectangle To_Town_2 = new Rectangle(40 * Globals.TileSize, 60 * Globals.TileSize, 6 * Globals.TileSize, 2 * Globals.TileSize);

        public Route_1(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }
        #endregion

        #region load/draw/update
        public override void LoadContent()
        {
            gameTimeLocal = new GameTime();
            base.LoadContent();
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);

            ///pc
            //mapBlockerLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_blocker.csv", 60, 50);
            //mapBottomLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_bottom.csv", 60, 50);
            //mapTopLayer = new MapBuilder(@"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_top.csv", 60, 50);
            
            ///work
            mapBlockerLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_blocker.csv", 60, 50);
            mapBottomLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_bottom.csv", 60, 50);
            mapTopLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\route_1\worldMap_route_1_top.csv", 60, 50);
            
            encounterZone = new(2, 4, 50, 50);
            encounterZone.Load();
            encounterZone.encounterTimer.Elapsed += CheckForEncounter;
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBlockerLayer.DrawMapFromList(Game._spriteBatch);
            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);

            if (switcher)
                encounterZone.Draw(Game._spriteBatch);
            //route_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Town_2, Color.White);

            Game._spriteBatch.End();

        }
        public override void Update(GameTime gameTime)
        {
            gameTimeLocal = gameTime;
            //CheckForEncounter();
            Game.CameraZoom();


            Globals.Update(gameTime);

            if (Globals.keyb.WasKeyPressed(Keys.E))
            {
                if (switcher)
                    switcher = false;
                else
                    switcher = true;
            }

            RpgPlayer.PlayerOverworld.DetectCollision(mapBlockerLayer);
            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);

            //town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);
            encounterZone.Update(gameTime);


            GoToTown_1();
            GoToTown_2();

            Globals.MainCamera.LookAt(RpgPlayer.PlayerOverworld.Position);
        }
        #endregion

        #region teleporters to other areas
        public void GoToTown_1()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Town_1))
            {
                int x = 15 * Globals.TileSize,
                    y = (18 * Globals.TileSize) - Globals.TileSize;
                
                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new Towns.Town_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }
        public void GoToTown_2()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Town_2))
            {
                int x = 16 * Globals.TileSize,
                    y = (3 * Globals.TileSize) - Globals.TileSize;

                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new Towns.Town_2(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }
        #endregion 

        private void CheckForEncounter(object? sender, ElapsedEventArgs e)
        {
            if (RpgPlayer.PlayerOverworld.state != PlayerState.Walking)
                return;

            encounterZone.areWeEncounteringWithThis = encounterZone.RollForByte();

            Debug.WriteLine($"threshhold: {encounterZone.encounterThreshold}| rolled: {encounterZone.areWeEncounteringWithThis}");

            if (encounterZone.areWeEncounteringWithThis % encounterZone.encounterThreshold == 0 && encounterZone.isPlayerInZone)
            {
                encounterZone.encounterThreshold = 2;
                encounterZone.encounterTimer.Enabled = false;
                encounterZone.encounterTimer.Stop();
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new CombatSimulator(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));

            }
            else
            {
                if (encounterZone.encounterThreshold>=4)
                    encounterZone.encounterThreshold -= 2;
            }
        }
    }
}
