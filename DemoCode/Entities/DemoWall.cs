using System.Collections.Generic;
using Game1;
using Game1.Engine.Entity;
using Microsoft.Xna.Framework;

namespace DemoCode.Entities
{
    class DemoWall : GameEntity, iCollidable, IShape
    {
        public DemoWall()
        {

        }

        public List<Vector2> GetVertices()
        {
            return new List<Vector2>();
        }
    }
}
