﻿using Engine.Collision;
using Engine.Engine.Collision;
using Engine.Entity;
using Engine.Input;
using Engine.Managers;
using Engine.Render;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Engine.Scene
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

        private iManager inputMan = new KeyboardInput();
        private iManager mouseInput = new MouseInput();
        private iManager controllerMan = new ControllerInput();

        private List<iManager> managerList;
        #endregion


        public SceneManager(ContentManager content)
        {
            renderMan = new Renderer();
            contentMan = content;

            sceneGraph = new SceneGraph();
            storeEntity = new List<iEntity>();
        }


        /// <summary>
        /// Setup of initial gameplay state
        /// </summary>
        public void Initialize(GraphicsDevice graph)
        {
            renderMan.Init(graph);

            managerList = new List<iManager>()
            {
                inputMan,
                mouseInput,
                controllerMan,
                (iManager)collManager
            };
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
            
            sceneGraph.childNodes.ForEach(e => LoadResource(e));
        }

        /// <summary>
        /// Loads a single iEntities resource into memory
        /// </summary>
        /// <param name="ent">The entity to load</param>
        public void LoadResource(iEntity ent)
        {
            ent.Texture = contentMan.Load<Texture2D>(ent.TextureString);

            if(ent.GetVertices().Count == 0)
            {
                ent.SetVertices(new List<Vector2>() { new Vector2(0,0), new Vector2(ent.Texture.Width, 0), new Vector2(ent.Texture.Width, ent.Texture.Height), new Vector2(0, ent.Texture.Height) });
            }

            if(!string.IsNullOrEmpty(ent.FontString))
            {
                ent.Font = LoadFont(ent.FontString);
            }
        }

        public SpriteFont LoadFont(string font)
        {
            return contentMan.Load<SpriteFont>(font);
        }


        /// <summary>
        /// Spawn entity into scene
        /// </summary>
        /// <param name="entityInstance">Spawn Entity</param>
        /// 
        public void Spawn(iEntity entityInstance)
        {
            if (!storeEntity.Contains(entityInstance))
            {
                // Insert entity into scene
                sceneGraph.addEntity(entityInstance);
                storeEntity.Add(entityInstance);

                if (entityInstance is iCollidable)
                {
                    collManager.AddCollidable(entityInstance);
                }

                if (entityInstance is ICollisionListener)
                {
                    var colEnt = (ICollisionListener)entityInstance;
                    collManager.RaiseCollision += colEnt.Collision;

                    collManager.AddCollisionListener(entityInstance);
                }

                renderMan.addEntity(entityInstance);
            }
        }


        /// <summary>
        /// Spawns a UI iEntity
        /// </summary>
        /// <param name="UI">The UI to spawn</param>
        public void SpawnUI(iEntity UI)
        {
            sceneGraph.addEntity(UI);
            storeEntity.Add(UI);
            renderMan.addUI(UI);

            //if(UI is IInteractiveUI)
            //{
            //    //MouseInput.Subscribe((IMouseInputObserver)UI);
            //}
        }
        

        /// <summary>
        /// Entity Removal from scene
        /// </summary>
        /// <typeparam name="T">Dynamic</typeparam>
        /// <param name="uid">Unique ID</param>
        /// <param name="uname">Unique Name</param>
        public void Remove<T>(T ent) where T : iEntity
        {
            renderMan.Remove(ent);
            collManager.Remove(ent);

            sceneGraph.removeEntity(ent.UID, ent.UName);
            storeEntity.Remove(ent);

            ent.Dispose();
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

        /// <summary>
        /// Updates all
        /// </summary>
        public void Update()
        {
            foreach(var ent in sceneGraph.childNodes.ToList())
            {
                if(ent.Destroy)
                {
                    Remove(ent);
                }
                else
                {
                    ent.Update();
                }
            }

            foreach(var manager in managerList)
            {
                manager.Update();
            }
        }

        /// <summary>
        /// Calls the renderers draw method
        /// </summary>
        public void Draw()
        {
            renderMan.Draw();
        }
    }
}
