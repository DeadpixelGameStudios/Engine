using System.Collections.Generic;
using Game1.Engine.Entity;
using Game1.Engine.Shape;

namespace Game1
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