using System.Collections.Generic;
using Engine.Entity;
using Engine.Shape;

namespace Engine.Collision
{
    public interface iCollisionManager
    {

        //void UpdateCollidableList(List<iEntity> entList);
        //void addCollidables(List<iEntity> entList);
        //void addCollidable(iEntity ent);
        //void CheckCollision();

        void AddCollidable(IShape shape);
        
        void Update();
    }
}