using Game1.Engine.Entity;
using Game1.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS4Mono;

namespace Game1
{
    class Player : GameEntity, IKeyboardInputObserver, iControllerObserver, iCollidable
    {
        #region Data Members

        private List<BasicInput> inputOptions = new List<BasicInput>
        {
            new BasicInput(Keys.W, Keys.S, Keys.A, Keys.D),
            new BasicInput(Keys.Up, Keys.Down, Keys.Left, Keys.Right),
            new BasicInput(Keys.I, Keys.K, Keys.J, Keys.L),
            new BasicInput(Keys.NumPad8, Keys.NumPad5, Keys.NumPad4, Keys.NumPad6)
        };
        private BasicInput inputKeys;
        private static int playerCount = 0;

        private float acceleration = 2f;
        float rotationVel = 3f;
        float linearVel = 4;

        List<GamePadInput> inputOptionsController = new List<GamePadInput>
        {
            new GamePadInput(Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight, Buttons.RightThumbstickLeft)
        };

        GamePadInput inputButtons;

        #endregion

        public Player()
        {
            BasicInput keys;
            GamePadInput controller;

            if (GamePad.GetCapabilities(playerCount).IsConnected)//Ps4Input.Ps4Count > playerCount)
            {
                try
                {
                    controller = inputOptionsController[0];
                }
                catch
                {
                    controller = new GamePadInput();
                }
                ControllerInput.Subscribe(this, controller.allButtons, playerCount);
                inputButtons = controller;
            }
            else
            {
                try
                {
                    keys = inputOptions[playerCount];
                }
                catch
                {
                    keys = new BasicInput();
                }

                KeyboardInput.Subscribe(this, keys.allKeys);
                inputKeys = keys;
            }
        
            playerCount++;

            CameraManager.RequestCamera(this);
            CollisionManager.subCollision(this);
        }
        

        public void input(Keys key)
        {
            if (inputKeys.allKeys.Contains(key))
            {
                if (key == inputKeys.up)
                {
                    Velocity = new Vector2(0, -1 * acceleration);
                }
                else if (key == inputKeys.down)
                {
                    Velocity = new Vector2(0, acceleration);
                }
                else if (key == inputKeys.left)
                {
                    Velocity = new Vector2(-1 * acceleration, 0);
                }
                else if (key == inputKeys.right)
                {
                    Velocity = new Vector2(acceleration , 0);
                }

                Position += Velocity;
            }
        }
        

        public override void Update()
        {
            #region remove this dirty ass code after prototyping

            Vector2 vel = new Vector2(0,0);

            if (isColliding)
            {
                if (Position.X > CollidingEntity.Position.X)
                {
                    vel = new Vector2(4, 0);
                }
                else
                {
                    vel = new Vector2(-4, 0);
                }

                if (Position.Y > CollidingEntity.Position.Y)
                {
                    vel = new Vector2(0, 4);
                }
                else
                {
                    vel = new Vector2(0, -4);
                }
            }
            isColliding = false;

            Position += vel;

            #endregion

        }

        private void update2(Vector2 val)
        {
            Rotation = val.X;
            // this rotation was testing
            if (val.X > 0)
            {
                Rotation += val.X;
            }
            else
            {
                Rotation -= val.X;
            }
        }

        public void gamePadInput(int playerIndex, Buttons gamePadButtons)
        {
            Vector2 vel = new Vector2(0, 0);

            if (inputButtons.allButtons.Contains(gamePadButtons))
            {
                if (gamePadButtons == Buttons.LeftThumbstickUp)
                {
                    vel = new Vector2(0, (-1 * acceleration));
                }
                else if (gamePadButtons == Buttons.LeftThumbstickDown)
                {
                    vel = new Vector2(0, (acceleration));
                }
                else if (gamePadButtons == Buttons.LeftThumbstickLeft)
                {
                    vel = new Vector2((-1 * acceleration), 0);
                }
                else if (gamePadButtons == Buttons.LeftThumbstickRight)
                {
                    vel = new Vector2((acceleration), 0);
                }
                // rotate
                else if (gamePadButtons == Buttons.RightThumbstickRight)
                {
                    //Rotation -= MathHelper.ToRadians(rotationVel);
                    //Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - Rotation));

                    update2(GamePad.GetState(playerIndex).ThumbSticks.Right);
                }
                else if (gamePadButtons == Buttons.RightThumbstickLeft)
                {
                    //Rotation += MathHelper.ToRadians(rotationVel);
                    //Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - Rotation));

                    update2(GamePad.GetState(playerIndex).ThumbSticks.Right);
                }
                Position += vel;
            }
        }
    }
}
