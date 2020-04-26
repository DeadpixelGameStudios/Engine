using Engine.Camera;
using Engine.Collision;
using Engine.Engine.Collision;
using Engine.Entity;
using Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameCode.Entities
{
    internal class Player : GameEntity, IKeyboardInputObserver, iControllerObserver, iCollidable, ICameraSubject, ICollisionListener
    {

        #region Data Members

        private List<BasicInput> inputOptions = new List<BasicInput>
        {
            new BasicInput(Keys.W, Keys.S, Keys.A, Keys.D, Keys.LeftShift, Keys.E),
            new BasicInput(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.None, Keys.End),
            new BasicInput(Keys.I, Keys.K, Keys.J, Keys.L, Keys.None, Keys.O),
            new BasicInput(Keys.NumPad8, Keys.NumPad5, Keys.NumPad4, Keys.NumPad6, Keys.None, Keys.NumPad9)
        };

        private BasicInput inputKeys;
        private static int playerCount = 0;

        private float acceleration = 2f;

        private uint abilityTimer = 0;
        private bool abilityTimeout = false;

        private bool sprintActive = false;

        private int playerNum;

        private iEntity artefact;

        GamePadInput inputButtons = new GamePadInput(Buttons.RightThumbstickRight, Buttons.RightThumbstickLeft, Buttons.X, Buttons.RightTrigger);

        public bool hasArtefact = false;

        #endregion

        
        public Player()
        {
            if (GamePad.GetCapabilities(playerCount).IsConnected)
            {
                ControllerInput.Subscribe(this, inputButtons.allButtons, playerCount);
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
                acceleration = 4f;
            }
        
            playerCount++;
            playerNum = playerCount;

            DrawPriority = 1f;
        }

        public override void Dispose()
        {
            //unsubscribe to keyboard input
            KeyboardInput.UnSubscribe(this);
            //Unsubscribe to controller input
        }

        public override void PassIEntity(iEntity ent)
        {
            artefact = ent;
        }


        public void input(Keys key)
        {
            if(InputAccepted)
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
                        Velocity = new Vector2(acceleration, 0);
                    }
                    else if (key == inputKeys.use)
                    {
                        if (!abilityTimeout)
                        {
                            //OnEntityRequested(new Vector2(Position.X + 80, Position.Y), "Walls/wall-left", typeof(Wall));
                            abilityTimeout = true;
                        }
                    }

                    if (key == inputKeys.sprint)
                    {
                        acceleration = 8f;

                        //heartRate += 0.5f;
                        //heartBeat.SetText(Math.Round(heartRate) + "bpm");
                    }
                    else
                    {
                        acceleration = 4f;

                        //if(heartRate < 75)
                        //{
                        //    heartRate += 0.05f;
                        //    heartBeat.SetText(Math.Round(heartRate) + "bpm");
                        //}
                        //else
                        //{
                        //    heartRate = 75;
                        //    heartBeat.SetText(Math.Round(heartRate) + "bpm");
                        //}
                    }

                    //Console.WriteLine(Texture.Name);
                    OnEntityRequested(new Vector2(Position.X + 25, Position.Y + 25), Texture.Name + "-trail", typeof(Trail));
                    Position += Velocity;
                }
            }
            
            Velocity = new Vector2(0, 0);
        }
        

        public override void Update()
        {
            if(abilityTimeout)
            {
                abilityTimer++;
            }

            if(abilityTimer >= 300)
            {
                abilityTimeout = false;
                abilityTimer = 0;
            }

            if(hasArtefact)
            {
                artefact.Position = Position - new Vector2(25, 25);
            }

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
            //acceleration was 2 for this

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
                    abilityTimeout = true;
                }
            }
            else
            {
                sprintActive = false;
            }

            Position += vel;
        }

        public void Collision(object sender, CollisionDetails colDetails)
        {
            if(colDetails.ColliderUname == UName)
            {
                if(colDetails.ColldingObject is Patient)
                {
                    OnLevelFinished(this);
                }
                else if(colDetails.ColldingObject is Artifact)
                {
                    CanFinish = true;
                    hasArtefact = true;
                }
                else if(colDetails.ColldingObject is Player)
                {
                    CanFinish = false;
                    hasArtefact = false;
                    artefact.Position += new Vector2(100, 100);
                }
                else
                {
                    Position += colDetails.mtv;
                }
            }
        }
    }
}
