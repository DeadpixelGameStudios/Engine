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

        public void CheckCollision(List<iEntity> pCollidables)
        {
            UpdateCollidableList(pCollidables);
            if (pCollidables.Count > 1)
            {
                checkCollidables(pCollidables);

            }
           



        }

        public void checkCollidables(List<iEntity> pEntityList)
        {

            iEntity ColidedEntity = null;

            for (int i = 0; i < pEntityList.Count; i++)
            {
                for (int k = 0; k < pEntityList.Count; k++)
                {
                    if (pEntityList[i].HitBox.Intersects(pEntityList[k].HitBox))
                    {
                        pEntityList[i].isColliding = true;
                        pEntityList[k].isColliding = true;

                        ColidedEntity = pEntityList[k];
                        pEntityList[i].CollidingEntity = ColidedEntity;
                    }
                    else
                    {
                        pEntityList[i].isColliding = false;


                    }
                }
            }

        }
        public void Update()
        {

        }
        #endregion  
    }
}