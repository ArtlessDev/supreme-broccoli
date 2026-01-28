using System;
using JairLib;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Diagnostics;
using System.Collections.Generic;
using MonoGame.Extended.Input;
using MonoGame.Extended.Graphics;

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
            Globals.MainCamera.Position = playerOverworld.Position;
            roomMysterySpaces = new List<TileSpace>();

            Debug.WriteLine(Globals.MainCamera.Position);
            //foreach (var tile in Globals.map.Spaces)
            //{
            //    if (tile.csvValue == 1)
            //    {
            //        roomMysterySpaces.Add(tile);
            //    }

            //}

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            //var transformMatrix = Globals.MainCamera.GetViewMatrix();

            Game._spriteBatch.Begin(transformMatrix: Globals.MainCamera.GetViewMatrix());


            Globals.map.DrawMapFromList(Game._spriteBatch);
            playerOverworld.Draw(Game._spriteBatch);

            //Game._spriteBatch.DrawString(_font, "Main Menu", _titlePosition, Color.White);
            //Game._spriteBatch.DrawString(_font, "Press Enter To Play", new Vector2(100, 100), Color.White);
            Game._spriteBatch.End();
            
        }

        public override void Update(GameTime gameTime)
        {

            playerOverworld.Update(gameTime, Globals.map);

            //Globals.CamMove(playerOverworld.rectangle);

            Globals.Update(gameTime);

            //if (Globals.mouseState.WasButtonPressed(MouseButton.Left))
            //    Debug.WriteLine("pressed mb");

            if (Globals.keyb.WasKeyPressed(Keys.H))
                ScreenManager.ShowScreen(new MainMenu(Game));

            Globals.MainCamera.LookAt(playerOverworld.Position);


            foreach (var tile in Globals.map.Spaces)
            {
                //if (tile.csvValue != 1)
                //{
                //    break;
                //}
                Globals.MouseHovering(tile, playerOverworld);

                //Debug.WriteLine(tile.Position);
                if (Globals.CheckMouseIntersectionRect(tile)
                    && Globals.mouseState.WasButtonPressed(MouseButton.Left) 
                    && tile.Position.X == playerOverworld.Position.X)
                {
                    if (tile.csvValue != 1)
                    {
                        break;
                    }
                    
                    tile.csvValue = RollForBeast(tile);

                    ///TODO: NEED TO ADD CRETURES REPLACING THE TILES WITH CONTENTS OF THE MATRICES

                    //tile.texture = Globals.beastiaryAtlas[tile.csvValue];

                    //Debug.WriteLine(tile.csvValue);

                }
            }

        }

        public Texture2DRegion AtlasPicker(TileSpace tile, int value, bool newBeastFlag)
        {
            if (!newBeastFlag)
            {
                //Debug.WriteLine("non-dex: " + value);
                return tile.texture = Globals.beastiaryAtlas[value];
            }

            Debug.WriteLine("dex: " + value);
            return tile.texture = Globals.beastiaryDexAtlas[value];
        }

        private int RollForBeast(TileSpace tile)
        {
            double chanceToGet = .2d;

            double rolledGetValue = Random.Shared.NextDouble();

            if (rolledGetValue > chanceToGet)
            {
                AtlasPicker(tile, 6, false);
                return 6;
            }

            int idNum = Random.Shared.Next(0,17);

            tile.beast = BeastSystem.BeastDeserializer(idNum);

            AtlasPicker(tile, idNum, true);
            return idNum;
        }
    }
}
