using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Entity
{

    /// <summary>
    /// Abstraction for any game entity
    /// </summary>
    public abstract class Entity : iEntity
    {
        public event EventHandler<EntityRequestArgs> EntityRequested;

        /// <summary>
        /// Event handler for requesting an entity
        /// </summary>
        /// <param name="pos">Position of the entity to add</param>
        /// <param name="texture">Texture of the entity</param>
        /// <param name="pType">The Type of entity to add</param>
        public virtual void OnEntityRequested(Vector2 pos, string texture, Type pType)
        {
            EntityRequested?.Invoke(this, new EntityRequestArgs() { Position = pos, Texture = texture, type = pType });
        }

        public event EventHandler<EventArgs> LevelFinished;

        public virtual void OnLevelFinished()
        {
            LevelFinished?.Invoke(this, new EventArgs());
        }


        #region Properties

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Velocity
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;

        } = 0f;

        public Vector2 Origin
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get;
            set;
        }
        
        public string TextureString
        {
            get;
            set;
        }

        public Guid UID
        {
            get;
            set;
        }

        public String UName
        {
            get;
            set;
        }

        public float maxHealth
        {
            get;
            set;
        }

        public float currentHealth
        {
            get;

            set;
        }

        public Rectangle HitBox
        {
            
            get
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y,
                    Texture.Width, Texture.Height);
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public float DrawPriority
        {
            get;
            set;
        } = 0f;

        #region TEMPORARY COLLISION VARS REMOVE THESE BASTARDS WHEN EVENTS ARE DONE
        public bool isColliding
        {
            get;
            set;
        }

        public iEntity CollidingEntity
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region Input structs

        /// <summary>
        /// Basic keyboard input struct
        /// </summary>
        public struct BasicInput
        {
            public BasicInput(Keys pUp = Keys.None, Keys pDown = Keys.None, Keys pLeft = Keys.None, Keys pRight = Keys.None, Keys pSprint = Keys.None, Keys pUse = Keys.None, Keys pRotate = Keys.None)
            {
                up = pUp;
                down = pDown;
                left = pLeft;
                right = pRight;
                sprint = pSprint;
                use = pUse;
                rotate = pRotate;

                allKeys = new List<Keys>(new Keys[] { up, down, left, right, sprint, use, rotate });
                allKeys.RemoveAll((Keys key) => key == Keys.None);
            }

            public Keys up;
            public Keys down;
            public Keys left;
            public Keys right;
            public Keys sprint;
            public Keys use;
            public Keys rotate;

            public List<Keys> allKeys;

        }

        /// <summary>
        /// Basic GamePadInput struct for managing assigned gamepad keys
        /// </summary>
        public struct GamePadInput
        {
            public Buttons rotateCW;
            public Buttons rotateACW;
            public Buttons sprint;
            public Buttons use;

            public List<Buttons> allButtons;

            public GamePadInput(Buttons pRotateCW = 0, Buttons pRotateACW = 0, Buttons pUse = 0, Buttons pSprint = 0)
            {
                rotateCW = pRotateCW;
                rotateACW = pRotateACW;
                use = pUse;
                sprint = pSprint;

                allButtons = new List<Buttons>(new Buttons[] { rotateCW, rotateACW, use, sprint, });
                allButtons.RemoveAll((Buttons key) => key == 0);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Entity()
        {

        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Entity()
        {
            Console.WriteLine("HAHAHA - I have destoryed the entity");
        }

        /// <summary>
        /// Used to set the entities 
        /// UID and UName
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <param name="name">Unique Name</param>
        public void Setup(Guid id, string name)
        {
            UID = id;
            UName = name;
        }

        /// <summary>
        /// Setup the entity and set properties
        /// </summary>
        /// <param name="id">The UID of the entity</param>
        /// <param name="name">The UName of the entity</param>
        /// <param name="tex">The texture of the entity</param>
        /// <param name="pos">The starting postion of the entity</param>
        public void Setup(Guid id, string name, string tex, Vector2 pos)
        {
            UID = id;
            UName = name;
            TextureString = tex;
            Position = pos;
        }

        /// <summary>
        /// This will distribute it to all other classes 
        /// In the hierarchy and any class which implements 
        /// An update with the same signature.
        /// </summary>
        public virtual void Update()
        {
        }

        #endregion
    }
}