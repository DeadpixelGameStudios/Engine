using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Engine.Entity
{
    /// <summary>
    /// Contract for any game entity
    /// </summary>
    public interface iEntity
    {
        Guid UID { get; set; }
        String UName { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        float Rotation { get; set; }
        Vector2 Origin { get; set; }
        Texture2D Texture { get; set; }
        string TextureString { get; set; }
        float currentHealth { get; set; }
        float maxHealth { get; set; }
        bool Visible { get; set; }
        bool isColliding { get; set; }
        iEntity CollidingEntity { get; set; }
        Rectangle HitBox { get; }
        float DrawPriority { get; set; }

        event EventHandler<EntityRequestArgs> EntityRequested;

        void Setup(Guid id, string name);
        void Setup(Guid id, string name, string texture, Vector2 position);
        void Update();
    }
}
