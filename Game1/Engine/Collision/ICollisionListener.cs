using Engine.Collision;

namespace Engine.Engine.Collision
{
    public interface ICollisionListener
    {
        void Collision(object sender, CollisionDetails colDetails);
    }
}
