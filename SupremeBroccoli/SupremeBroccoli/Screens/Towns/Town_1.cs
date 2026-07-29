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

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());

            mapBottomLayer.DrawMapFromList(Game._spriteBatch);
            mapTopLayer.DrawMapFromList(Game._spriteBatch);
            //mapBlockerLayer.DrawMapFromList(Game._spriteBatch);

            town_1_quest.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);
            town_1_quest_2.DrawCurrentQuestObjective(Game._spriteBatch, RpgPlayer.PlayerOverworld);

            //player shadow
            //RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch, Color.Black);
            RpgPlayer.PlayerOverworld.Draw(Game._spriteBatch);

            //Game._spriteBatch.Draw(Atlases.WorldMapAtlas[0].Texture, To_Route_1, Color.White);

            if (town_1_gui != null)
                town_1_gui.draw(Game._spriteBatch);

           
            //RpgPlayer.PlayerOverworld.DrawShader(Game._spriteBatch, Globals.player_shader);
            //Globals.DrawShader(Game._spriteBatch, Globals.vignette_shader);

            Game._spriteBatch.End();

        }
        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            Game.CameraZoom();
            RpgPlayer.PlayerOverworld.DetectCollision(mapBlockerLayer);
            RpgPlayer.PlayerOverworld.Update(gameTime, mapTopLayer);
            //town_1_quest.Update(gameTime, RpgPlayer.PlayerOverworld);

            town_1_gui.update(gameTime);
            
            foreach(var t in town_1_quest.objectives)
            {
                if(t.isPlayerInteracting())
                    t.OpenGui(town_1_gui, gameTime);

                //foreach (AnimatedSprite anim in t.Animations)
                //{
                //    anim.Update(gameTime);
                //}

                if (t.isPlayerInteracting())
                {
                    t.NpcStates = NpcStates.Talking;
                }
                else
                {
                    t.NpcStates = NpcStates.Idle;
                }

            };
            foreach(var t in town_1_quest.CurrentQuest.KvpQuests.Values)
            {
                if(t.isPlayerInteracting())
                {
                    t.OpenGui(town_1_gui, gameTime);
                    t.Animations[1].Update(gameTime);

                }

                //foreach (AnimatedSprite anim in t.Animations)
                //{
                //    anim.Update(gameTime);
                //}


                if (t.isPlayerInteracting())
                {
                    t.NpcStates = NpcStates.Talking;
                }
                else
                {
                    t.NpcStates = NpcStates.Idle;
                }

            }
            foreach (var t in town_1_quest_2.CurrentQuest.KvpQuests.Values)
            {
                /// need a 'trigger' for the kvp objectives: when the previous one is set to true, 
                /// the previous/prereq should be disabled or only does the alt text.
                /// 
                /// if A is the prereq to B, A should be completed in order to try and complete B
                foreach (AnimatedSprite anim in t.Animations)
                {
                    anim.Update(gameTime);
                }

                if (t.isPlayerInteracting())
                {
                    t.NpcStates = NpcStates.Talking;
                }
                else
                {
                    t.NpcStates = NpcStates.Idle;
                }

                if (t.PrerequisiteObjective != QuestList.None)

                if(t.isPlayerInteracting())
                    t.OpenGui(town_1_gui, gameTime);
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
