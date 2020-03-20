using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Shape
{
    public interface IShape
    {
        List<Vector2> GetVertices();
        Vector2 GetPosition();
        Rectangle GetBoundingBox();
        bool IsCollisionListener();

        //this interface probably needs the definitions for the event handlers
        //with existing event handlers, the entity has been the one to raise the events, but in this case its the collisionManager, 
    }
}
