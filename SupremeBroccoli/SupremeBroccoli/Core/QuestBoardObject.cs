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
    public class QuestBoardObject : AnyObject
    {
        public QuestBoardObject()
        {
            texture2D = Globals.GlobalContent.Load<Texture2D>(FileString);
        }
        public QuestBoardObject(Rectangle _rect)
        {
            texture2D = Globals.GlobalContent.Load<Texture2D>(FileString);
            rectangle = _rect;
        }

        const string FileString = "OverworldTextures/QuestBoard";
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, rectangle, Color.White);
        }

        //public void Update(GameTime gameTime, QuestBoard _questBoardGui, Dictionary<Quest> quests)
        //{
        //    if (RpgPlayer.PlayerOverworld.interactionBox.Intersects(rectangle) && Globals.keyb.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
        //    {
        //        var qob1 = new QuestOnBoard();
        //        qob1.QuestTitle.Text = "lost cat";
        //        qob1.QuestDetails.Text = "Old Lady Maguffette has lost her cat Madam Whispy, again. Cat is usually ";

        //        QuestBoardGui = new();
        //        _questBoardGui.StackPanel.AddChild(qob1);
        //        _questBoardGui.AddToRoot();

        //    }
        //}

        public bool IsPlayerInRange()
        {
            return RpgPlayer.PlayerOverworld.interactionBox.Intersects(rectangle);
        }

        //public QuestBoard OpenGui()
        //{
        //    QuestBoardGui = new QuestBoard();
        //    QuestBoardGui.AddToRoot();

        //    return QuestBoardGui;
        //}
    }
}