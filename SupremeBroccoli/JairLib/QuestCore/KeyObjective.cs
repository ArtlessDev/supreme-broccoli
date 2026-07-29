using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace JairLib.QuestCore
{
    public class KeyObjective : AnyObject//: ITileObject
    {
        #region constuctor and variables
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
        public List<string> objectiveDialogue { get; set; }
        public string identifier { get; set; }
        private int _x, _width;
        private int _y, _height;
        public int X
        {
            get { return _x; }
            set { _x = value * Globals.TileSize; }
        }
        public int Y {
            get { return _y; }
            set { _y = value * Globals.TileSize; }
        }
        public int width { 
            get { return _width; }
            set { _width = Globals.TileSize; }
        }
        public int height
        {
            get { return _height; }
            set { _height = Globals.TileSize; }
        }
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
        public QuestList PrerequisiteObjective { get; set; }
        Direction direction { get; set; }
        #endregion constructor and variables

        public void Update(GameTime gameTime, PlayerOverworld player)
        {
            //shoutout dean ritter, using a dictionary makes this possible in the easiest way
            if (PrerequisiteObjective == QuestList.None) 
            {

            }
            else 
            {
                if (player.rectangle.Intersects(rectangle)
                && Globals.keyb.WasKeyPressed(Keys.E)
                )
                {
                    //Debug.WriteLine(this.objectiveTitle);
                    IsCompletedFlag = true;
                }
            }
        }

        public bool isPlayerInteracting()
        {
            var playerctx = RpgPlayer.PlayerOverworld;
            var playerIntersectFlag = playerctx.interactionBox.Intersects(rectangle);
            

            if (!playerIntersectFlag)
                return false;

            if (Globals.keyb.WasKeyPressed(Keys.E))
            {
                Globals.LockEKey = true;
                return true;
            }
            else return false;

        }

        public NpcStates NpcStates;
        private SpriteSheet _spriteSheet;
        public AnimatedSprite[] Animations = new AnimatedSprite[8];
        public string animationString;
        public AnimatedSprite[] LoadAnimations()
        {

            _spriteSheet = new SpriteSheet("SpriteSheet/npc", Atlases.npcBatchOneAtlas);

            foreach(AnimatedSprite anisprite in Animations)
            {
                int index = Array.IndexOf(Animations, anisprite);
                var idlePoseString = $"npcAtlas_{textureValue}";
                var midYapString = $"npcAtlas_{textureValue+4}";
                animationString = $"npcAtlas_{identifier}_{index}_{textureValue + 4}";

                if (index%2 == 0)
                {
                    _spriteSheet.DefineAnimation(animationString, builder =>
                    {
                        builder.IsLooping(true)
                               .AddFrame(idlePoseString, TimeSpan.FromSeconds(0.2));
                    });

                }
                else
                {
                    _spriteSheet.DefineAnimation(animationString, builder =>
                    {
                        builder.IsLooping(true)
                               .AddFrame(idlePoseString, TimeSpan.FromSeconds(0.2))
                               .AddFrame(midYapString, TimeSpan.FromSeconds(0.2));
                    });
                }

                //_spriteSheet.DefineAnimation(animationString+"_idle", builder =>
                //{
                //    builder.IsLooping(true)
                //           .AddFrame(idlePoseString, TimeSpan.FromSeconds(0.2));
                //});
                

                Animations[index] = new AnimatedSprite(_spriteSheet, animationString);
            }

            return Animations;
        }

        public CustomGuiGroup OpenGui(CustomGuiGroup gui, GameTime gameTime)
        {
            bool tempIsPlayerSelecting = gui.baseGui.DemandsPlayerResponse;
            var playerctx = RpgPlayer.PlayerOverworld;

            if (!isPlayerInteracting())
                return gui;

            playerctx.interactWithBox();

            gui.baseGui.currentText = objectiveDescription;
            gui.baseGui.DemandsPlayerResponse = this.DemandsPlayerResponse;
            gui.baseGui.isGuiEnabled = !gui.baseGui.isGuiEnabled;

            if (gui.baseGui.DemandsPlayerResponse)
            {
                this.Update(gameTime, playerctx);
                gui.selectionGui.isGuiEnabled = !gui.selectionGui.isGuiEnabled;
            }

            return gui;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            Vector2 position = new(rectangle.X, rectangle.Y);
            var scale = new Vector2(1f, 1f);





            if (NpcStates == NpcStates.Idle)
            {
                _spriteBatch.Draw(Animations[0], position, 0, scale);

            }
            else if (NpcStates == NpcStates.Talking)
            {
                _spriteBatch.Draw(Animations[1], position, 0, scale);
            }

            //{
            //    switch (direction)
            //    {
            //        case (Direction.Down):
            //            _spriteBatch.Draw(Animations[0], position, 0, scale);
            //            break;
            //        case (Direction.Left):
            //            _spriteBatch.Draw(Animations[1], position, 0, scale);
            //            break;
            //        case (Direction.Right):
            //            //_walkSide.Effect = flipper;
            //            _spriteBatch.Draw(Animations[2], position, 0, scale);
            //            break;
            //        case (Direction.Up):
            //            _spriteBatch.Draw(Animations[3], position, 0, scale);
            //            break;
            //    }
            //}
            //else if (NpcStates == NpcStates.Talking)
            //{
            //    switch (direction)
            //    {
            //        case (Direction.Down):
            //            _spriteBatch.Draw(Animations[4], position, 0, scale);
            //            break;
            //        case (Direction.Left):
            //            _spriteBatch.Draw(Animations[5], position, 0, scale);
            //            break;
            //        case (Direction.Right):
            //            //_walkSide.Effect = flipper;
            //            _spriteBatch.Draw(Animations[6], position, 0, scale);
            //            break;
            //        case (Direction.Up):
            //            _spriteBatch.Draw(Animations[7], position, 0, scale);
            //            break;
            //    }
            //}

            //if (IsCompletedFlag)
            //{
            //    _spriteBatch.DrawString(Globals.font, objectiveDescription, new(rectangle.X, rectangle.Y), Color.White);
            //}
            //else
            //{
            //    _spriteBatch.Draw(texture, new Vector2(rectangle.X, rectangle.Y), color);
            //}
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
