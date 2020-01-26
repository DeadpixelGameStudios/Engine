using Game1.Engine.Entity;
using Game1.Engine.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EngineMain : Game, IEngineAPI
    {
        GraphicsDeviceManager graphics;
        iSceneManager sceneManager;
        iEntityManager entityManager;

        public static int ScreenWidth, ScreenHeight;
        bool paused = false;

        public EngineMain()
        {
            #region Setup
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";

            ScreenWidth = 1600;
            ScreenHeight = 900;

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            
            this.IsMouseVisible = true;

            // Setting screen to middle
            Window.Position = new Point
                ((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) -
                (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) -
                (graphics.PreferredBackBufferHeight / 2));

            #endregion

            sceneManager = new SceneManager(Content);
            entityManager = new EntityManager();
        }


        protected override void Initialize()
        {
            base.Initialize();
        }
        

        public List<iEntity> LoadLevel(List<LevelInfo.LevelAsset> levelInfo)
        {
            var entities = entityManager.CreateLevel(levelInfo);
            foreach(var ent in entities)
            {
                sceneManager.Spawn(ent);
            }

            return entities;
        }


        public T LoadEntity<T>(string texture, Vector2 position) where T : iEntity, new()
        {
            var ent = entityManager.RequestInstanceAndSetup<T>(texture, position);
            sceneManager.Spawn(ent);

            if(GraphicsDevice != null)
            {
                sceneManager.LoadResource(ent);
            }
            
            return ent;
        }


        public T LoadUI<T>(string texture, Vector2 position) where T : iEntity, new()
        {
            var ui = entityManager.RequestInstanceAndSetup<T>(texture, position);
            sceneManager.SpawnUI(ui);

            if (GraphicsDevice != null)
            {
                sceneManager.LoadResource(ui);
            }
            
            return ui;
        }


        public void UnLoad(iEntity ent)
        {
            //not sure what this implementation looks like yet really
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            sceneManager.Initialize(GraphicsDevice);
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


            sceneManager.Update();

            
            #region FPS counter
            // Code for displaying FPS in output
            //private FrameCounter frameCounter = new FrameCounter();
            //var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //frameCounter.Update(deltaTime);
            //var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);
            //Console.WriteLine("Current fps - " + fps);
            #endregion

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
