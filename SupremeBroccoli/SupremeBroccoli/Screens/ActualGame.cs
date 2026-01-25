using System;
using JairLib;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Diagnostics;
using System.Collections.Generic;

namespace SupremeBroccoli.Screens
{
    public class ActualGame : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private SpriteFont _font;
        private Vector2 _titlePosition;
        private PlayerOverworld playerOverworld;
        private List<TileSpace> roomMysterySpaces;

        public ActualGame(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Content.Load<SpriteFont>("coolvetica");
            _titlePosition = new Vector2(100, 50);
            Globals.Load();
            Globals.map = new JairLib.TileGenerators.MapBuilder();
            playerOverworld = new PlayerOverworld();
            Globals.MainCamera = new OrthographicCamera(Game._graphics.GraphicsDevice);
            roomMysterySpaces = new List<TileSpace>();

            foreach (var tile in Globals.map.Spaces)
            {
                if (tile.csvValue == 1)
                {
                    roomMysterySpaces.Add(tile);
                }

                //if (Globals.mouseRect.Intersects(tile.rectangle))// && tile.csvValue == 1)
                //{
                //    Debug.WriteLine("gfdsgfds");
                //    tile.color = Color.AliceBlue;
                //}
                //else if (tile.color == Color.AliceBlue && !Globals.mouseRect.Intersects(tile.rectangle))
                //{
                //    tile.color = Color.White;
                //}

            }

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here

            var transformMatrix = Globals.MainCamera.GetViewMatrix();

            Game._spriteBatch.Begin(transformMatrix: transformMatrix);


            Globals.map.DrawMapFromList(Game._spriteBatch);
            playerOverworld.Draw(Game._spriteBatch);

            Game._spriteBatch.DrawString(_font, "Main Menu", _titlePosition, Color.White);
            Game._spriteBatch.DrawString(_font, "Press Enter To Play", new Vector2(100, 100), Color.White);
            Game._spriteBatch.End();
            
        }
        int ran = 0;
        public override void Update(GameTime gameTime)
        {

            playerOverworld.Update(gameTime, Globals.map);
            Globals.CamMove(playerOverworld.rectangle);

            Globals.Update(gameTime);

            foreach (var tile in Globals.map.Spaces)
            {
                //if (tile.csvValue != 1)
                //{
                //    break;
                //}

                if (Globals.CheckMouseIntersectionRect(tile) && Globals.mouseRect.Intersects(tile.rectangle) )
                {

                    //Debug.WriteLine("gfdsgfds");
                    tile.color = Color.Red;
                }
                //else if (tile.color == Color.AliceBlue && !Globals.mouseRect.Intersects(tile.rectangle))
                //{
                //    tile.color = Color.White;
                //}


            }

            //Debug.WriteLine(ran);
            //Debug.WriteLine(Globals.mouseRect);


            if (Globals.keyb.WasKeyPressed(Keys.H))
                ScreenManager.ShowScreen(new MainMenu(Game));
            
        }
    }
}
