using Game1.Engine.Entity;
using Game1.Engine.Input;
using Game1.Engine.Misc.FPSCounter;
using Game1.Engine.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth, ScreenHeight;

        private FrameCounter frameCounter = new FrameCounter();

        // Just testing out the npc from the yt video
        Texture2D texture;
        Vector2 textureLocn;

        Ball ball;
        List<Paddle> paddle = new List<Paddle>();

        iEntityManager entityManager = new EntityManager();

        iSceneManager sceneManager = new SceneManager();

        KeyboardInput inputMan = new KeyboardInput();

        MouseInput mouseInput = new MouseInput();

        //SpriteFont font;

        //Look at this - he made an engine using mono game where u can drag drop stuff. engine got ui
        // https://github.com/Memorix101/MonoGame_ComponentSystem

        // https://www.youtube.com/watch?v=9QYfcJBsy1k

        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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

            //font = Content.Load<SpriteFont>("Text");

            ball = entityManager.RequestInstance<Ball>();
            paddle.Add(entityManager.RequestInstance<Paddle>());
            paddle.Add(entityManager.RequestInstance<Paddle>());
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //texture = Content.Load<Texture2D>("Textures/NPC/main_character_single");
            //textureLocn = new Vector2(ScreenWidth / 2, ScreenHeight / 2);

            ball.Texture = Content.Load<Texture2D>("Resources/ball");

            // if multiple objects got the same texture use this?
            paddle.ForEach(p => p.Texture = Content.Load<Texture2D>("Resources/paddle"));
        
            // Subscribe paddles to input (terrible way of doing this - need to figure out a better way to do this)
            paddle[0].subscirbeToInput(new Entity.BasicInput(Keys.W, Keys.S, Keys.A, Keys.D));
            paddle[1].subscirbeToInput(new Entity.BasicInput(Keys.Up, Keys.Down, Keys.Left, Keys.Right));

            MouseInput.Subscribe(paddle[0]);
            MouseInput.Subscribe(paddle[1]);



            sceneManager.Spawn(ball, new Vector2(ScreenWidth / 2, ScreenWidth / 2));
            sceneManager.Spawn(paddle[0], new Vector2(0, ScreenHeight / 2 - 50));
            sceneManager.Spawn(paddle[1], new Vector2(1550, ScreenHeight / 2 - 50));
            
            sceneManager.LoadResources(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            
            //ball.Update(gameTime);
            sceneManager.Update(gameTime);
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
            GraphicsDevice.Clear(Color.BlueViolet);

            // TODO: Add your drawing code here
            

            spriteBatch.Begin();
            //WWspriteBatch.Draw(texture, textureLocn, Color.White);

            sceneManager.Draw(spriteBatch);

            //ball.Draw(spriteBatch);

            // look i got this. so instead of saying for each paddle list call draw. do it in linq
            //https://stackoverflow.com/questions/3198053/generics-call-a-method-on-every-object-in-a-listt
            //paddle.ForEach(p => p.Draw(spriteBatch));

            //spriteBatch.DrawString(font, "test", new Vector2(0, 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
