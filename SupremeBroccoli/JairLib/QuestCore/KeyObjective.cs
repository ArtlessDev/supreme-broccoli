using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace JairLib.QuestCore
{
    public class KeyObjective : AnyObject//: ITileObject
    {
        public KeyObjective() {
            //TODO: make the json read the 2nd constructor, not this one
            textureAtlas = Atlases.SetAtlas(textureAtlasId);
            texture = textureAtlas[textureValue];
            color = Color.White;
        }
        public KeyObjective(Texture2DAtlas specifiedAtlas) {
            texture = specifiedAtlas[textureValue];
            color = Color.White;
        }
        public string objectiveTitle { get; set; }
        public string objectiveDescription { get; set; }
        public string identifier { get; set; }
        private int _x;
        private int _y;
        public int X
        {
            get { return _x; }
            set { _x = value * Globals.TileSize; }
        }
        public int Y {
            get { return _y; }
            set { _y = value * Globals.TileSize; }
        }
        public int width {get; set;}
        public int height {get; set;}
        public Rectangle rectangle => new Rectangle(X,Y,width,height);
        public int textureValue {  get; set; }
        public Texture2DRegion texture { get; set; }
        public Texture2DAtlas textureAtlas { get; set; }
        public int textureAtlasId { get; set; }
        public Color color { get; set; }
        public bool IsCompletedFlag { get; set; }
        public bool IsMainQuest { get; set; }
        public bool IsAutoTrigger { get; set; }
        public bool DemandsPlayerResponse { get; set; }
        
    
        public void Update(GameTime gameTime, PlayerOverworld player)
        {
            if (player.rectangle.Intersects(rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
            {
                //Debug.WriteLine(this.objectiveTitle);
                IsCompletedFlag = true;
            }
        }

        public CustomGuiGroup isPlayerInteracting(CustomGuiGroup gui)
        {
            bool tempIsPlayerSelecting = gui.baseGui.DemandsPlayerResponse;
            var playerctx = RpgPlayer.PlayerOverworld;
            var playerIntersectFlag = playerctx.interactionBox.Intersects(rectangle);

            if (gui.selectionGui.isGuiEnabled)
                return gui.selectionGui.ChooseDialogueOption(gui);
            if (!Globals.keyb.WasKeyPressed(Keys.E))
                return gui;
            if (!playerIntersectFlag)
                return gui;

            Globals.LockEKey = true;
            playerctx.interactWithBox();
            gui.baseGui.currentText = objectiveDescription;
            gui.baseGui.DemandsPlayerResponse = this.DemandsPlayerResponse;
            gui.baseGui.isGuiEnabled = !gui.baseGui.isGuiEnabled;

            if (gui.baseGui.DemandsPlayerResponse)
                gui.selectionGui.isGuiEnabled = !gui.selectionGui.isGuiEnabled;

            return gui;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            if (IsCompletedFlag) 
            { 
                _spriteBatch.DrawString(Globals.font, objectiveDescription, new(rectangle.X, rectangle.Y), Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, new Vector2(rectangle.X, rectangle.Y), color);
            }
        }

        public void DrawNoCheck(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            if (IsAutoTrigger && player.rectangle.Intersects(rectangle))
            {
                _spriteBatch.DrawString(Globals.font, objectiveDescription, new(player.rectangle.X, player.rectangle.Y+32), Color.White);
            }

            _spriteBatch.Draw(texture, new Vector2(rectangle.X, rectangle.Y), color);
            if(IsCompletedFlag)
            {
                _spriteBatch.DrawString(Globals.font, objectiveDescription, new(rectangle.X, rectangle.Y-32), Color.White);
            }
        }
    }
}
