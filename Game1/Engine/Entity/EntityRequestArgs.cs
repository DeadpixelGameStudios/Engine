using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Entity
{
    public class EntityRequestArgs : EventArgs
    {
        public Vector2 Position { get; set; }
        public string Texture { get; set; }
        public Type type { get; set; }
    }
}