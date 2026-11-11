using Gum.Forms.Controls;
using JairLib;
using JairLib.QuestCore;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SupremeBroccoli.Components;
using SupremeBroccoli.Screens;
using System.Collections.Generic;

namespace SupremeBroccoli.Core
{
    public class QuestJson
    {
        public string QuestTitle { get; set; }
        public string QuestDetails { get; set; }
        public string QuestGiver { get; set; }
        public string QuestReward { get; set; }
        public string QuestStatus { get; set; }
    }
    public class QuestBoardObject : AnyObject
    {
        public QuestBoardObject()
        {
            texture2D = Globals.GlobalContent.Load<Texture2D>(TextureFileString);
        }
        public QuestBoardObject(Rectangle _rect)
        {
            texture2D = Globals.GlobalContent.Load<Texture2D>(TextureFileString);
            rectangle = _rect;
            QuestBoardGui = new();
        }
        internal QuestBoard QuestBoardGui { get; set; }
        const string TextureFileString = "OverworldTextures/QuestBoard";
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, rectangle, Color.White);
        }

        internal void Update(GameTime gameTime)
        {
            if (IsPlayerInRange() && Globals.keyb.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                var visCheck = QuestBoardGui.IsVisible;

                if (!visCheck)
                {
                    QuestBoardGui.AddToRoot();
                    QuestBoardGui.IsVisible = true;
                }
                else
                {
                    QuestBoardGui.IsVisible = false;
                    QuestBoardGui.RemoveFromRoot();
                }
            }
        }

        public bool IsPlayerInRange()
        {
            return RpgPlayer.PlayerOverworld.interactionBox.Intersects(rectangle);
        }

        internal QuestBoard OpenGui()
        {
            QuestBoardGui = new QuestBoard();
            QuestBoardGui.AddToRoot();

            return QuestBoardGui;
        }
    }
}