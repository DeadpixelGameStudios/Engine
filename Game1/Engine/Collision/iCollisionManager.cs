using System;
using System.Collections.Generic;
using Engine.Entity;
using Engine.Shape;
using static Engine.Collision.SAT;

namespace Engine.Collision
{
    public interface iCollisionManager
    {
        void AddCollidable(IShape shape);
        void AddCollisionListener(IShape shape);
        void Remove(IShape collidable);

        event EventHandler<CollisionDetails> RaiseCollision;
    }
}