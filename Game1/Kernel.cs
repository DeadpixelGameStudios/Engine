using Game1.Engine.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        GraphicsDeviceManager graphics;
        public static int ScreenWidth, ScreenHeight;
        bool paused = false;
        iSceneManager sceneManager;
        

        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";

            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;

            this.IsMouseVisible = true;

            #region Set screen to middle
            // Setting screen to middle
            //this.Window.Position = new Point(200, 50);

            // Setting screen to middle
            Window.Position = new Point
                ((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) -
                (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) -
                (graphics.PreferredBackBufferHeight / 2));

            #endregion

        }


        protected override void Initialize()
        {
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            sceneManager = new SceneManager(GraphicsDevice, Content);

            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            sceneManager.loadLevel("test-level.tmx");
            
            sceneManager.LoadResources();
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            sceneManager.UnloadContent();
        }
        

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.P) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                paused = !paused;
            }


            if (!paused)
            {
                sceneManager.Update();
            }
            
            // Code for displaying FPS in output
            //private FrameCounter frameCounter = new FrameCounter();
            //var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //frameCounter.Update(deltaTime);
            //var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);
            //Console.WriteLine("Current fps - " + fps);

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumSlateBlue);

            sceneManager.Draw();

            base.Draw(gameTime);
        }
    }
}
