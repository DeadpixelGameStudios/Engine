using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCode.Entities
{
    public interface IShape
    {
        List<Vector2> GetVertices();
    }
}
