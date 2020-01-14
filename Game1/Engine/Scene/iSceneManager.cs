using Game1.Engine.Entity;
using System;
using System.Collections.Generic;

namespace Game1.Engine.Scene
{
    /// <summary>
    /// Manages gameplay scene
    /// </summary>
    interface iSceneManager
    {
        /// <summary>
        /// Record entity has been spawned
        /// </summary>
        List<iEntity> storeEntity { get; set; }

        /// <summary>
        /// Spawn entity into scene
        /// </summary>
        /// <param name="entityInstance">Spawn Entity</param>
        void Spawn(iEntity entityInstance);

        /// <summary>
        /// Entity Removal from scene
        /// </summary>
        /// <typeparam name="T">Dynamic</typeparam>
        /// <param name="uid">Unique ID</param>
        /// <param name="uname">Unique Name</param>
        void Remove<T>(Guid uid, string uname) where T : iEntity;

        /// <summary>
        /// Retrieval of reference to spawned entity
        /// </summary>
        /// <typeparam name="T">Dynamic typing</typeparam>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>Reference to entity</returns>
        iEntity GetEntity<T>(Guid id, string name) where T : iEntityManager;

        /// <summary>
        /// Load Resources
        /// </summary>
        void LoadResources();

        /// <summary>
        /// The unload content ensures all content from the current scene is
        /// released before loading a new scene
        /// </summary>
        void UnloadContent();
        
        void Update();

        void Draw();

        void loadLevel(string level);

    }
}
