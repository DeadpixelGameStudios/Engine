using System.Collections.Generic;
using Game1.Engine.Collision;
using Game1.Engine.Entity;
using Game1.Engine.Managers;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class CollisionManager: iCollisionManager, iManager
    {

        #region SHIT COLLISION TO BE DELETED WHEN EVENTS ARE IN

        List<iEntity> Collidables = new List<iEntity>();
        public static List<iEntity> subList = new List<iEntity>();

        QuadTree qTree = new QuadTree(0, new Rectangle(0,0, 1600, 900));

        public void UpdateCollidableList(List<iEntity> pCollidables)
        {
            Collidables.Clear();

            for (int i = 0; i < pCollidables.Count;i++)
            {
                if(pCollidables[i] is iCollidable)
                {
                    Collidables.Add(pCollidables[i]);
                }
                
            }
            
        }

        public void addCollidables(List<iEntity> entList)
        {
            UpdateCollidableList(entList);
        }

        public void addCollidable(iEntity ent)
        {
            if(ent is iCollidable)
            {
                Collidables.Add(ent);
                
            }
        }

        public void CheckCollision()
        {
           
            checkCollidables();
        }

        public static void subCollision(iEntity ent)
        {
            subList.Add(ent);
        }
    
        private void PopulateQuad()
        {
            qTree.clear();

            foreach (iEntity ent in Collidables)
            {
                qTree.insert(ent);
            }

        }

        private void checkCollidables()
        {
            List<iEntity> potentialCollision = new List<iEntity>();


            foreach (var ent1 in subList)
            {
               qTree.retrieve(potentialCollision, ent1);

                foreach (var ent2 in potentialCollision)
                {
                    if(ent1.UName != ent2.UName)
                    {
                        if (ent1.HitBox.Intersects(ent2.HitBox))
                        {
                            ent1.isColliding = true;
                            iEntity ColidedEntity = ent2;
                            ent1.CollidingEntity = ColidedEntity;
                        }
                    }
                }
            }

        }


        public void Update()
        {
            if (Collidables.Count > 0)
            {
                PopulateQuad();
            }
            
            CheckCollision();
        }
        #endregion  
    }
}