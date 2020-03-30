using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Shape
{
    public interface IShape
    {
        List<Vector2> GetVertices();
        Vector2 GetPosition();
        Rectangle GetBoundingBox();
    }
}
