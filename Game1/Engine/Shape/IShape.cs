using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Shape
{
    public interface IShape
    {
        List<Vector2> GetVertices();
        void SetVertices(List<Vector2> verts);
        Vector2 GetPosition();
        Rectangle GetBoundingBox();
    }
}
