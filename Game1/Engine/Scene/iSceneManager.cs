using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Scene
{
    /// <summary>
    /// Manages gameplay scene
    /// </summary>
    interface iSceneManager
    {
        // Add something for input cause scene can have input would need
        // to ensure the right scene has control over the right input?

        // camera?

        // if scene active? or nah
        //? bool Active { get; set; }

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
        /// <param name="conManager">content manager</param>
        void LoadResources(ContentManager conManager);

        /// <summary>
        /// The unload content ensures all content from the current scene is
        /// released before loading a new scene
        /// </summary>
        void UnloadContent();

        void Update();
        List<iEntity> GetAllEntities();

        void Update(GameTime gametime);

        void Draw();

    }
}
