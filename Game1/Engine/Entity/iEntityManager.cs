using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Entity
{
    /// <summary>
    /// Manages entities lifecycle
    /// </summary>
    interface iEntityManager
    {
        /// <summary>
        /// Store a list of Entities
        /// </summary>
        List<iEntity> storeEntity { get; set; }

        /// <summary>
        /// Request to create a new Instance of Entity
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns>Reference to EntityInstance</returns>
        T RequestInstance<T>() where T : iEntity, new();

        T RequestInstanceAndSetup<T>(string texture, Vector2 postion) where T : iEntity, new();

        //request a level by string
        List<iEntity> requestLevel(string level);

        /// <summary>
        /// Get Entity from their ID and name
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <param name="name">Unique Name</param>
        /// <returns>Entity</returns>
        iEntity GetEntity(Guid id, string name);

        /// <summary>
        /// Termination of Entity not sure how to
        /// Maybe look for entity in list and set to null?
        /// </summary>
        /// <param name="UID">Unique Identifier</param>
        /// <param name="UName">Unique Name</param>
        void Terminate(Guid UID, string UName);
    }
}