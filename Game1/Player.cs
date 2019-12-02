﻿using Game1.Engine.Entity;
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
            new BasicInput(Keys.W, Keys.S, Keys.A, Keys.D, Keys.None, Keys.E),
            new BasicInput(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.None, Keys.End),
            new BasicInput(Keys.I, Keys.K, Keys.J, Keys.L, Keys.None, Keys.O),
            new BasicInput(Keys.NumPad8, Keys.NumPad5, Keys.NumPad4, Keys.NumPad6, Keys.None, Keys.NumPad9)
        };

        private BasicInput inputKeys;
        private static int playerCount = 0;

        private float acceleration = 0.5f;

        private uint abilityTimer = 0;
        private bool abilityTimeout = false;

        private bool sprintActive = false;

        private bool hasArtifact = false;

        GamePadInput inputButtons = new GamePadInput(Buttons.RightThumbstickRight, Buttons.RightThumbstickLeft, Buttons.X, Buttons.RightTrigger);

        private List<string> lineTextureList = new List<string> { "Walls/line-pink", "Walls/line-blue", "Walls/line-red", "Walls/line-green" };
        private string setLine;

        #endregion

        
        public Player()
        {
            
            if (GamePad.GetCapabilities(playerCount).IsConnected)
            {
                ControllerInput.Subscribe(this, inputButtons.allButtons, playerCount);
                setLine = lineTextureList[playerCount];
            }
            else
            {
                BasicInput keys;

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
                acceleration = 8f;
            }
        
            playerCount++;

            CameraManager.RequestCamera(this);
            CollisionManager.subCollision(this);

            DrawPriority = 1f;
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
                else if(key == inputKeys.use)
                {
                    if(!abilityTimeout)
                    {
                        OnEntityRequested(new Vector2(Position.X + 80, Position.Y), "Walls/wall-left", typeof(Wall));
                        //abilityTimeout = true;
                    }
                }

                Position += Velocity;
            }

            Velocity = new Vector2(0, 0);
        }
        

        public override void Update()
        {
            #region remove this dirty ass code after prototyping

            Vector2 vel = new Vector2(0,0);

            if (isColliding)
            {
                if(CollidingEntity is FinishLine && hasArtifact)
                {
                    Console.WriteLine("Woooo I Win - " + UName);
                }

                if(CollidingEntity is Artifact)
                {
                    hasArtifact = true;
                    CollidingEntity.Transparency = 0f;
                }

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


            if(abilityTimeout)
            {
                abilityTimer++;
            }

            if(abilityTimer >= 300)
            {
                abilityTimeout = false;
                abilityTimer = 0;
            }

            //drawing line behind player
            //if(Velocity.X != 0 || Velocity.Y != 0)
            //{
                
            //}
            OnEntityRequested(new Vector2(Position.X, Position.Y), setLine, typeof(Artifact));

        }

        private void Rotate(Vector2 val)
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

        public void gamePadInput(Buttons gamePadButtons, GamePadThumbSticks thumbSticks)
        {
            var leftThumbX = thumbSticks.Left.X * acceleration;
            var leftThumbY = thumbSticks.Left.Y * -1 * acceleration;

            Vector2 vel = new Vector2(leftThumbX, leftThumbY);
            
            if(sprintActive)
            {
                vel = vel * 3;
            }

            
            if (gamePadButtons == inputButtons.rotateCW)
            {
                //Rotate(thumbSticks.Right);
            }
            else if (gamePadButtons == inputButtons.rotateACW)
            {
                //Rotate(thumbSticks.Right);
            }
            else if(gamePadButtons == inputButtons.sprint)
            {
                sprintActive = true;
            }
            else if(gamePadButtons == inputButtons.use)
            {
                if (!abilityTimeout)
                {
                    OnEntityRequested(new Vector2(Position.X + 80, Position.Y), "Walls/wall-left", typeof(Wall));
                    //abilityTimeout = true;
                }
            }
            else
            {
                sprintActive = false;
            }

            Position += vel;
        }
    }
}
