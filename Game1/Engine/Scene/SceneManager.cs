using Game1.Engine.Entity;
using Game1.Engine.Input;
using Game1.Engine.Managers;
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

        private iSceneGraph sceneGraph;
        private Renderer renderMan;
        private ContentManager contentMan;

        #endregion
        

        #region Properites

        public List<iEntity> storeEntity { get; set; }

        #endregion


        #region Managers
        private iCollisionManager collManager = new CollisionManager();
        private iEntityManager entityManager = new EntityManager();

        private iManager inputMan = new KeyboardInput();
        private iManager mouseInput = new MouseInput();
        private iManager controllerMan = new ControllerInput();

        private List<iManager> managerList;
        #endregion


        public SceneManager(GraphicsDevice graph, ContentManager content)
        {
            renderMan = new Renderer(graph);
            contentMan = content;

            Initialize();
        }


        /// <summary>
        /// Setup of initial gameplay state
        /// </summary>
        private void Initialize()
        {
            sceneGraph = new SceneGraph();
            storeEntity = new List<iEntity>();

            managerList = new List<iManager>()
            {
                inputMan,
                mouseInput,
                controllerMan,
                (iManager)collManager
            };
        }


        public void loadLevel(string level)
        {
            int playerCount = 0;
            foreach (var asset in entityManager.requestLevel(level))
            {
                Spawn(asset);

                if (asset.UName.Contains("Player"))
                {
                    playerCount++;
                }
            }

            string uiSeperator = "Walls/" + playerCount.ToString() + "player";
            Spawn(entityManager.RequestInstanceAndSetup<UI>(uiSeperator, new Vector2(0, 0)));
            
        }


        /// <summary>
        /// Association of resources to entity
        /// </summary>
        public void LoadResources()
        {
            // Not sure how to do
            //Association should be performed through a method (e.g. LoadResources())
            //separate from other initialisation operations
            //• It is recommended to associate resources before other initialisation operations, which might require
            //(some of) the associated resources

            sceneGraph.childNodes.ForEach(e => e.Texture = contentMan.Load<Texture2D>(e.TextureString));

            renderMan.Init();
        }


        private void LoadResource(iEntity ent)
        {
            ent.Texture = contentMan.Load<Texture2D>(ent.TextureString);
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

                //commented out for line drawing
                collManager.addCollidable(entityInstance);

                if (entityInstance is UI)
                {
                    renderMan.addUI(entityInstance);
                }
                else
                {
                    renderMan.addEntity(entityInstance);
                    entityInstance.EntityRequested += OnEntityRequested;
                }
            }
        }


        public void OnEntityRequested(object source, EntityRequestArgs args)
        {
            var newEnt = entityManager.RequestInstanceAndSetup<Wall>(args.Texture, args.Position);
            LoadResource(newEnt);
            //Spawn(newEnt);

            //duplicated spawn stuff - remove
            storeEntity.Add(newEnt);

            //commented out for line drawing
            //collManager.addCollidable(entityInstance);
            renderMan.addEntity(newEnt);
            newEnt.EntityRequested += OnEntityRequested;
            
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

                storeEntity.RemoveAll(x => x.UID.Equals(uid) && x.UName.Equals(name));

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

            foreach(var manager in managerList)
            {
                manager.Update();
            }
        }


        public void Draw()
        {
            renderMan.Draw();
        }
    }
}
