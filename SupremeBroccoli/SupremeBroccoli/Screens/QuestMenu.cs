using Gum;
using Gum.Forms.Controls;
using JairLib;
using JairLib.CombatSimulator;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using SupremeBroccoli.Components;
using SupremeBroccoli.Screens.Towns;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SupremeBroccoli.Screens
{

    /// <summary>
    /// we can clear this all out. Town 1 has quest board working in the same screen
    /// </summary>
    public class QuestMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        internal QuestBoard screen { get; set; }

        public QuestMenu(Game game) : base(game)
        {
        }

        public static Dictionary<string, string> EXAMPLE_QUESTS = new Dictionary<string, string>()
        {
            { "quest_1", "This is the first quest. It is a simple quest that requires the player to collect 10 apples." },
            { "quest_2", "This is the second quest. It is a more complex quest that requires the player to defeat a boss." },
            { "quest_3", "This is the third quest. It is a very complex quest that requires the player to complete a series of tasks." }
        };
        
        public override void LoadContent()
        {
            //Game.GumUI.Initialize(Game, "UI/SideQuests.gumx");
            base.LoadContent();
            Globals.Load();
            Atlases.Load();

            var questInstance = new QuestOnBoard();
            questInstance.QuestTitle.Text = "help with cat!";
            questInstance.QuestDetails.Text = "Old Lady Maguffette has lost her cat Madam Whispy, again. Cat is usually ";
            
            screen.StackPanel.AddChild(questInstance); 
            //.QuestOnBoardInstance3.QuestTitle.Text = "quest_2";
            //screen.QuestOnBoardInstance3.QuestDetails.Text = EXAMPLE_QUESTS["quest_2"];

            QuestOnBoard qob1 = new QuestOnBoard();
            qob1.QuestTitle.Text = "quest_1";
            qob1.QuestDetails.Text = EXAMPLE_QUESTS["quest_1"];
            screen.StackPanel.AddChild(qob1);
            screen.AddToRoot();
            //Game.GumUI.Root.AddToRoot();

            qob1.AcceptQuestButton.Click += (s, e) =>
            {
                //Game.screenManager.LoadScreen(new Town_1(Game));
                int x = 10 * Globals.TileSize,
                    y = (5 * Globals.TileSize) - Globals.TileSize;

                //Game.GumUI.Root.Children[0].RemoveFromRoot();
                RpgPlayer.PlayerOverworld.Position = new(x, y);
                RpgPlayer.PlayerOverworld.rectangle = new(x, y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
                ScreenManager.CloseScreen();
                ScreenManager.ShowScreen(new Towns.Town_1(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            };

            qob1.RejectQuestButton.Click += (s, e) =>
            {
                //screen.QuestOnBoardInstance.RemoveFromRoot();
                screen.StackPanel.Visual.Visible = false;
                //Game.GumUI.Root.RemoveChild(screen.QuestOnBoardInstance);
                screen.StackPanel.AddToRoot();
            };
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            
            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            Game.GumUI.Draw();
            
            Game._spriteBatch.End();

        }
        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            screen.UpdateState();
            Game.GumUI.Update(gameTime);
            //Debug.WriteLine("state: " + screen.QuestOnBoardInstance.AcceptQuestButton.ButtonCategoryState);
        }

        public Screen ReturnToThisScreen;
        internal void ChangeBackScreen(GraphicsDevice graphics, ScreenManager _screenManager)
        {

            _screenManager.CloseScreen();
            _screenManager.ShowScreen(ReturnToThisScreen, new FadeTransition(graphics, Color.Black, 0.5f));
        }
    }

}

