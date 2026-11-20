using Gum.Forms.Controls;
using JairLib;
using JairLib.QuestCore;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SupremeBroccoli.Screens;

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
            //texture = Globals.GlobalContent.Load<Texture2D>(TextureFileString);
        }
        public QuestBoardObject(Rectangle _rect)
        {
            texture2D = Globals.GlobalContent.Load<Texture2D>(TextureFileString);
            rectangle = _rect;
            int colliderHeight = rectangle.Height / 2;

            color = Color.White;
            Collider2d = new Rectangle(_rect.X, _rect.Y+colliderHeight, rectangle.Width, colliderHeight);
            QuestBoardGui = new();
        }
        internal QuestBoard QuestBoardGui { get; set; }
        const string TextureFileString = "OverworldTextures/QuestBoard";
        public void Draw(SpriteBatch spriteBatch)
        {
            var origin = new Vector2(0, 0);
            var position = new Vector2(rectangle.X, rectangle.Y);
            var scale = new Vector2(.5f, .5f);
            var rotation = 0f;

            float customLayerDepth = (Collider2d.Center.X) / 10000f;

            spriteBatch.Draw(texture2D, position, null, color, rotation, origin, scale, SpriteEffects.None, customLayerDepth);
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