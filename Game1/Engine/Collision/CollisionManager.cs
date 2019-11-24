using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Engine.Entity;

namespace Game1
{
    public class CollisionManager: iCollisionManager
    {

        #region SHIT COLLISION TO BE DELETED WHEN EVENTS ARE IN

        List<iEntity> Collidables = new List<iEntity>();
        public static List<iEntity> subList = new List<iEntity>();
        

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

        public void CheckCollision()
        {
            checkCollidables();
        }

        public static void subCollision(iEntity ent)
        {
            subList.Add(ent);
        }

        private void checkCollidables()
        {
            foreach (var ent1 in subList)
            {
                foreach (var ent2 in Collidables)
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
            CheckCollision();
        }
        #endregion  
    }
}