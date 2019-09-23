using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Entity
{
    //[Serializable]
    /// <summary>
    /// Manages entities lifecycle
    /// </summary>
    class EntityManager : iEntityManager
    {
        #region Data Members

        Guid id;

        /// <summary>
        /// Store list of the entity names
        /// </summary>
        List<String> entityNames;

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
            Initialize();
        }

        /// <summary>
        /// Any Initialization done here not in constructor
        /// </summary>
        private void Initialize()
        {
            storeEntity = new List<iEntity>();
            entityNames = new List<string>();

            //* Was trying to serialize entity *
            // its a bit wrong it would create an xml
            // then scene manager would open it and read it

            //Stream stream = File.Open("EntityManager.xml", FileMode.Create);
            //BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(stream, GetType().Name);
            //stream.Close();

            //(this)formatter.Deserialize(stream);
        }

        /// <summary>
        /// Load Resources of entity
        /// </summary>
        /// <param name="conManager">content manager</param>
        public void LoadResources(ContentManager conManager)
        {
            //idk what to do here
        }

        /// <summary>
        /// Request Instance of entity
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns></returns>
        public T RequestInstance<T>() where T : iEntity, new()
        {
            return CreateInstance<T>();
        }

        /// <summary>
        /// Create Instance, this is where it generates a id
        /// and gives its uname
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns></returns>
        private T CreateInstance<T>() where T : iEntity, new()
        {
            // create new instance
            T iReqestedEntity = new T();

            // Generate UID
            id = Guid.NewGuid();

            // Add the entity class name to list
            entityNames.Add(iReqestedEntity.GetType().Name);

            // call entity setup method
            iReqestedEntity.Setup
                (id, setEntityUName(iReqestedEntity.GetType().Name));

            // add entity to list
            storeEntity.Add(iReqestedEntity);

            // return
            return iReqestedEntity;
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

            // Terminate();

        }

        #endregion
    }
}
