using Game1.Engine.Entity;
using Game1.Engine.Render;
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
    class SceneManager : iSceneManager
    {
        #region Data Members

        iSceneGraph sceneGraph;

        Vector2 origin;

        #endregion

        #region Properites

        public List<iEntity> storeEntity { get; set; }

        #endregion

        private Renderer renderMan;

        /// <summary>
        /// Constructor
        /// </summary>
        public SceneManager(GraphicsDevice graph)
        {
            renderMan = new Renderer(graph);
            Initialize();
        }

        /// <summary>
        /// Setup of initial gameplay state
        /// </summary>
        private void Initialize()
        {
            sceneGraph = new SceneGraph();
            storeEntity = new List<iEntity>();
        }

        /// <summary>
        /// Association of resources to entity
        /// </summary>
        public void LoadResources(ContentManager Content)
        {
            // Not sure how to do
            //Association should be performed through a method (e.g. LoadResources())
            //separate from other initialisation operations
            //• It is recommended to associate resources before other initialisation operations, which might require
            //(some of) the associated resources

            sceneGraph.childNodes.ForEach(e => e.Texture = Content.Load<Texture2D>(e.TextureString));

            renderMan.Init();
        }

        /// <summary>
        /// Spawn entity into scene
        /// </summary>
        /// <param name="entityInstance">Spawn Entity</param>
        public void Spawn(iEntity entityInstance)
        {
            if (!storeEntity.Contains(entityInstance))
            {
                // Insert entity into scene
                sceneGraph.addEntity(entityInstance);
                storeEntity.Add(entityInstance);

                if(entityInstance.UName.Contains("UI"))
                {
                    renderMan.addUI(entityInstance);
                }
                else
                {
                    renderMan.addEntity(entityInstance);
                }
            }
        }

        /// <summary>
        /// Entity Removal from scene
        /// </summary>
        /// <typeparam name="T">Dynamic</typeparam>
        /// <param name="uid">Unique ID</param>
        /// <param name="uname">Unique Name</param>
        public void Remove<T>(Guid uid, string name) where T : iEntity
        {
            if (storeEntity.AsEnumerable().Select(x => x).
                Where(x => x.UID == uid && x.UName == name).Count() > 0)
            {
                sceneGraph.removeEntity(uid, name);

                storeEntity.RemoveAll(
                    x => x.UID.Equals(uid) && x.UName.Equals(name));

                // how do i set entity position to null?
            }
        }


        /// <summary>
        /// Retrieval of reference to spawned entity
        /// </summary>
        /// <typeparam name="T">Dynamic typing</typeparam>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>Reference to entity</returns>
        public iEntity GetEntity<T>(Guid id, string name) where T : iEntityManager
        {
            // if entity is in scene then 
            if (storeEntity.AsEnumerable().Select(x => x).
                Where(x => x.UID == id && x.UName == name).Count() > 0)
            {
                //Retrieval of reference to spawned entity
                // not tested but idk if it works. I wish it does
                // GetEntity method is in the entitymanager class
                return ((iEntityManager)this).GetEntity(id, name);
            }
            else
            {
                return null;
            }
        }

        public List<iEntity> GetAllEntities()
        {

            return storeEntity;


        }

        /// <summary>
        /// The unload content ensures all content from the current scene is
        /// released before loading a new scene
        /// </summary>
        public void UnloadContent()
        {
            sceneGraph.removeAll();
        }

        public void Update()
        {
            sceneGraph.childNodes.ForEach(entity => entity.Update());
        }

        public void Draw()
        {
            renderMan.Draw();
        }
    }
}
