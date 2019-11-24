using Game1.Engine.Entity;
using Game1.Engine.Input;
using Game1.Engine.Misc.FPSCounter;
using Game1.Engine.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        GraphicsDeviceManager graphics;

        public static int ScreenWidth, ScreenHeight;

        private FrameCounter frameCounter = new FrameCounter();

        //Ball ball;
        //List<Paddle> paddle = new List<Paddle>();

        iEntityManager entityManager;
        iSceneManager sceneManager;
        iCollisionManager collManager = new CollisionManager();
        KeyboardInput inputMan = new KeyboardInput();
        MouseInput mouseInput = new MouseInput();

        //Look at this - he made an engine using mono game where u can drag drop stuff. engine got ui
        // https://github.com/Memorix101/MonoGame_ComponentSystem

        // https://www.youtube.com/watch?v=9QYfcJBsy1k

        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";
            
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;
            
            this.IsMouseVisible = true;

            #region set screen to middle
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
            // TODO: Add your initialization logic here
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            sceneManager = new SceneManager(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            int playerCount = 0;
            foreach (var asset in entityManager.requestLevel("test-level.tmx"))
            {
                sceneManager.Spawn(asset);

                if (asset.UName.Contains("Player"))
                {
                    playerCount++;
                }
            }
            
            string uiSeperator = "Walls/" + playerCount.ToString() + "player";
            sceneManager.Spawn(entityManager.RequestInstanceAndSetup<UI>(uiSeperator, new Vector2(0, 0)));

            sceneManager.LoadResources(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
                Exit();

            // TODO: Add your update logic here

            
            sceneManager.Update(gameTime);
            collManager.CheckCollision(sceneManager.GetAllEntities());
            inputMan.Update();
            mouseInput.Update();
            

            // Code for displaying FPS in output
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
