using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game1.Engine.Entity;
namespace Game1
{
    public interface iCollisionManager
    {

        void UpdateCollidableList(List<iEntity> entList);
        void addCollidables(List<iEntity> entList);
        void CheckCollision();
        void Update();






    }
}