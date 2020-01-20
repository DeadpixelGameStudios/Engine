using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Game1.Engine.Entity
{
    /// <summary>
    /// Manages entities lifecycle
    /// </summary>
    public interface iEntityManager
    {
        /// <summary>
        /// Store a list of Entities
        /// </summary>
        List<iEntity> storeEntity { get; set; }

        /// <summary>
        /// Request to create a new Instance of Entity and set up
        /// </summary>
        /// <param name="postion">The position to set the entity</param>
        /// <param name="texture">The texture of the requested entity</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns>Reference to EntityInstance</returns>
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