using Engine.Entity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Collision
{
    public class CollisionDetails
    {
        public string ColliderUname;
        public iEntity ColldingObject;
        public Vector2 mtv;

        public CollisionDetails(string uname, iEntity ent, Vector2 pMtv)
        {
            ColliderUname = uname;
            ColldingObject = ent;
            mtv = pMtv;
        }
        
    }
}
