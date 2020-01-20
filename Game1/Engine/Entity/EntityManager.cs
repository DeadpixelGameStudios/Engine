using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Entity
{
    //[Serializable]
    /// <summary>
    /// Manages entities lifecycle
    /// </summary>
    public class EntityManager : iEntityManager
    {
        #region Data Members

        /// <summary>
        /// Store list of the entity names
        /// </summary>
        List<String> entityNames;
        
        private LevelLoader levelLoader;

        #endregion

        #region Properties

        /// <summary>
        /// Store entity reference
        /// </summary>
        public List<iEntity> storeEntity { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityManager()
        {
            storeEntity = new List<iEntity>();
            entityNames = new List<string>();
            levelLoader = new LevelLoader();
        }

        
        /// <summary>
        /// Request instance and call Setup on the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="texture">The texture string</param>
        /// <param name="position">POsition of the entity</param>
        /// <returns></returns>
        public T RequestInstanceAndSetup<T>(string texture, Vector2 position) where T : iEntity, new()
        {
            return CreateInstanceAndSetup<T>(texture, position);
        }

        /// <summary>
        /// Requests level by string
        /// </summary>
        /// <param name="level">Name of the level file</param>
        /// <returns>List of initialised and setup iEntities</returns>
        public List<iEntity> requestLevel(string level)
        {
            var assets = levelLoader.requestLevel(level);
            List<iEntity> returnList = new List<iEntity>();

            foreach(var asset in assets)
            {
				var ent = (iEntity)Activator.CreateInstance(asset.info.type);
                Setup(ent, asset.info.texture, asset.position);
                returnList.Add(ent);
            }

            return returnList;
        }

        /// <summary>
        /// Create Instance, this is where it generates a id
        /// and gives its uname
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        private T CreateInstance<T>() where T : iEntity, new()
        {
            T requestedEntity = new T();
            
            storeEntity.Add(requestedEntity);
            entityNames.Add(requestedEntity.GetType().Name);
            Setup(requestedEntity);
            
            return requestedEntity;
        }

        /// <summary>
        /// Creates the requested instance and sets it up
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="texture"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private T CreateInstanceAndSetup<T>(string texture, Vector2 pos) where T : iEntity, new()
        {
            var requestedEntity = CreateInstance<T>();
            Setup(requestedEntity, texture, pos);

            return requestedEntity;
        }

        /// <summary>
        /// Sets up the entity
        /// </summary>
        /// <param name="entity">The entity to setup</param>
        /// <param name="texture">The texture to set for that entity</param>
        /// <param name="pos">The position of the entity</param>
        private void Setup(iEntity entity, string texture = "default", Vector2 pos = default(Vector2))
        {
            var id = Guid.NewGuid();

            entity.Setup(id, setEntityUName(entity.GetType().Name), texture, pos);
        }





        /// <summary>
        /// Give entity unique name by class and number
        /// </summary>
        /// <param name="name">class name</param>
        /// <returns></returns>
        private string setEntityUName(string name)
        {
            // return name with number of how many there are in the list -1 to give accurate number.
            return name + (entityNames.Select(n => n).Where(n => n == name).Count() - 1);
        }

        /// <summary>
        /// Look through list to find the entity and return it
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <param name="name">Unique Name</param>
        /// <returns>Entity</returns>
        public iEntity GetEntity(Guid id, string name)
        {
            try
            {
                return storeEntity.AsEnumerable().Select(e => e).
                Where(e => e.UID.Equals(id) && e.UName.Equals(name)).ToList()[0];
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Termination of Entity not sure how to
        /// Maybe look for entity in list and set to null?
        /// </summary>
        /// <param name="UID">Unique Identifier</param>
        /// <param name="UName">Unique Name</param>
        public void Terminate(Guid UID, string UName)
        {
            // unsure how to terminate. to dispose?

            if (storeEntity.AsEnumerable().Select(x => x).
                Where(x => x.UID == UID && x.UName == UName).Count() > 0)
            {
                storeEntity.AsEnumerable().
                    Where(e => e.UID.Equals(UID) && e.UName.Equals(UName)).
                    Select(e => e).ToList()[0] = null;



                //storeEntity.Remove(storeEntity.AsEnumerable().
                //    Where(e => e.UID.Equals(UID) && e.UName.Equals(UName)).
                //    Select(e => e).ToList()[0]);
            }
        }

        #endregion
    }
}