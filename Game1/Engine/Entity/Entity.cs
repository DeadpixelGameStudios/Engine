using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine.Entity
{
    /// <summary>
    /// Abstraction for any game entity
    /// </summary>
    abstract class Entity : iEntity
    {
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

        public Texture2D Texture
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
        /// This will distribute it to all other classes 
        /// In the hierarchy and any class which implements 
        /// An update with the same signature.
        /// </summary>
        public virtual void Update(GameTime gametime)
        {

        }

        #endregion
    }
}
