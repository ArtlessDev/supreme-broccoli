using JairLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using SupremeBroccoli.Screens;

namespace SupremeBroccoli
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public GraphicsDevice _device;
        public SpriteBatch _spriteBatch;
        private readonly ScreenManager screenManager;
        public BoxingViewportAdapter viewportAdapter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.WindowWidth;
            _graphics.PreferredBackBufferHeight = Globals.WindowHeight;
            Content.RootDirectory = "Content";
            Globals.GlobalContent = Content;
            IsMouseVisible = true;
            
            screenManager = new ScreenManager();
            Components.Add( screenManager );
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Globals.ViewportWidth, Globals.ViewportHeight);
            Globals.MainCamera = new OrthographicCamera(viewportAdapter);

            screenManager.ShowScreen(new Town_1(this));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Globals.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
