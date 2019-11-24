using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Texture2D Texture { get; set; }
        string TextureString { get; set; }
        float currentHealth { get; set; }
        float maxHealth { get; set; }
        bool Visible { get; set; }

        void Setup(Guid id, string name);
        void Setup(Guid id, string name, string texture, Vector2 position);
        void Update();
    }
}
