using JairLib;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using SupremeBroccoli.Core;
using System;
using System.Linq;

namespace SupremeBroccoli.Screens.Towns
{
    //town where the player begins
    public class Town_1 : GameScreen
    {
        #region local variables and screen constructor
        private new Game1 Game => (Game1)base.Game;
        MapBuilder mapTopLayer, mapBottomLayer, mapBlockerLayer;
        QuestSystem town_1_quest, town_1_quest_2;
        CustomGuiGroup town_1_gui;
        Rectangle To_Route_1 = new Rectangle(20 * Globals.TileSize, 26 * Globals.TileSize, 2 * Globals.TileSize, 2 * Globals.TileSize);
        Rectangle To_Route_3 = new Rectangle();
        internal QuestBoard _questBoardGui { get; set; }


        public Town_1(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }
        #endregion

        #region load/draw/update
        public override void LoadContent()
        {
            base.LoadContent();
            Globals.Load();
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);

            //non-work-pc
            mapBlockerLayer = new MapBuilder(ConfigStrings.town_1_blocker, 32, 32);
            mapBottomLayer = new MapBuilder(ConfigStrings.town_1_bottom, 32, 32);
            mapTopLayer = new MapBuilder(ConfigStrings.town_1_top, 32, 32);

            //work pc
            //mapBlockerLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_blocker_layer.csv", 20, 20);
            //mapBottomLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_bottom_layer.csv", 20, 20);
            //mapTopLayer = new MapBuilder(@"C:\Code\MonogameStudy\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\tilemaps\town_1\worldMap_town_1_top_layer.csv", 20, 20);

            ///THESE ARE OLD NOTES, RETAINING FOR REFERENCE
            ///need to add functionality to the buttons
            ///essentially just 'accept' and 'put back' quest options
            ///this will be a list of quests that the player can accept or decline
            ///and the player will only have access to one quest at a time, so if they accept a quest, it will be added to their quest log and they will not be able to accept another quest until they complete the current one

            town_1_quest = new QuestSystem(ConfigStrings.town_1_quest, Atlases.npcBatchOneAtlas);
            town_1_quest_2 = new QuestSystem(ConfigStrings.town_1_quest_2, Atlases.npcBatchOneAtlas);

            for (int i = 0; i < town_1_quest_2.CurrentQuest.KvpQuests.Count; i++)
            {
                var _reference = town_1_quest_2.CurrentQuest.KvpQuests.ToArray();
                town_1_quest_2.CurrentQuest.KvpQuests[_reference[i].Key].Animations = _reference[i].Value.LoadAnimations();

            }
            for (int i = 0; i < town_1_quest.CurrentQuest.KvpQuests.Count; i++)
            {
                var _reference = town_1_quest.CurrentQuest.KvpQuests.ToArray();
                town_1_quest.CurrentQuest.KvpQuests[_reference[i].Key].Animations = _reference[i].Value.LoadAnimations();

            }


            town_1_gui = new();

            RpgPlayer.PlayerOverworld.LoadAnimations();
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Game.CameraZoom();
            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            
            stableDraw();

            Game.GumUI.Draw();
            ///this shows the ui on the screen when the rest is commented out
            ///but not with everything else for some reason
            /// currently moving the gum for here into it's own menu. will see how that goes

            Game._spriteBatch.End();
        }
        public void stableDraw()
        {

            //mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);
            //mapBlockerLayer.DrawMapFromList(Game._spriteBatch);

            town_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);
            town_1_quest_2.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            //player shadow
            //RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch, Color.Black);
            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            //Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Route_1, Color.White);

            
            //RpgPlayer.PlayerOverworld.DrawShader(Game._spriteBatch, Globals.player_shader);
            //Globals.DrawShader(Game._spriteBatch, Globals.vignette_shader);

        }
        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            Game.CameraZoom();
            RpgPlayer.PlayerOverworld.DetectCollision(mapBlockerLayer);
            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);
            //town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);

            town_1_gui.update(gameTime);

            if (Globals.keyb.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.V))
            {
                Game.GumUI.Root.Children.Clear();
                var questMenu = new QuestMenu(Game) { ReturnToThisScreen = this,
                screen = new QuestBoard()
                };
                //questMenu.AddToRoot(); 
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(questMenu, new FadeTransition(GraphicsDevice, Color.Black, 0.5f));

            }

            //foreach(var t in town_1_quest.objectives)
            //{
            //    if(t.isPlayerInteracting())
            //        t.OpenGui(town_1_gui, gameTime);

            //    //foreach (AnimatedSprite anim in t.Animations)
            //    //{
            //    //    anim.Update(gameTime);
            //    //}
            //    t.CurrentAnimation.Update(gameTime);

            ///talking animation gets overridden on subsequent frame and goes back to idle. idle animation is also the animation for talking
            //};
            foreach (var t in town_1_quest.CurrentQuest.KvpQuests.Values)
            {
                t.AssignNpcState(town_1_gui);

                if(t.isPlayerInteracting())
                {
                    t.OpenGui(town_1_gui, gameTime, t.CurrentAnimation.Color);
                }
                
                //this is essentially a hover
                if(t.rectangle.Intersects(RpgPlayer.PlayerOverworld.interactionBox) && town_1_gui.baseGui.isGuiEnabled)
                    t.CurrentAnimation.Update(gameTime);
            }
            foreach (var t in town_1_quest_2.CurrentQuest.KvpQuests.Values)
            {
                t.AssignNpcState(town_1_gui);

                if (t.isPlayerInteracting())
                {
                    t.OpenGui(town_1_gui, gameTime, t.CurrentAnimation.Color);
                    t.CurrentAnimation.Update(gameTime);

                }

            }

            GoToRoute_1();

            Globals.MainCamera.LookAt(RpgPlayer.PlayerOverworld.Position);
            Globals.LockEKey = false;
        }
        #endregion

        #region teleporters
        public void GoToRoute_1()
        {
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Route_1))
            {
                int x = 13 * Globals.TileSize, 
                    y = 8 * Globals.TileSize;
                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new Routes.Route_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }
        public void GoToRoute_3()
        {
            //TODO set correct position 
            if (RpgPlayer.PlayerOverworld.rectangle.Intersects(To_Route_1))
                ScreenManager.ShowScreen(new Routes.Route_3(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
        }
        #endregion
    }
}
