using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Entity
{
    /// <summary>
    /// Class for custom EntityRequestArgs
    /// </summary>
    public class EntityRequestArgs : EventArgs
    {
        public Vector2 Position { get; set; }
        public string Texture { get; set; }
        public Type type { get; set; }
    }
}