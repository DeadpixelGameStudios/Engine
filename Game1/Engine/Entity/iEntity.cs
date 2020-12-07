using Engine.Shape;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Engine.Entity
{
    /// <summary>
    /// Contract for any game entity
    /// </summary>
    public interface iEntity : IShape
    {
        void Dispose();

        Guid UID { get; set; }
        String UName { get; set; }

        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        float Rotation { get; set; }
        Vector2 Origin { get; set; }
        Texture2D Texture { get; set; }
        string TextureString { get; set; }

        bool Destroy { get; set; }
        
        iEntity CollidingEntity { get; set; }
        Rectangle HitBox { get; }
        float DrawPriority { get; set; }
        float Transparency { get; set; }
        bool InputAccepted { get; set; }

        //stuff for text
        string Text { get; set; }
        Vector2 TextPosition { get; set; }
        string FontString { get; set; }
        SpriteFont Font { get; set; }

        void PassUI(IStaticUI ui);

        bool CanFinish { get; set; }

        void PassIEntity(iEntity ent);

        event EventHandler<EntityRequestArgs> EntityRequested;
        event EventHandler<LevelFinishedArgs> LevelFinished;

        void Setup(Guid id, string name);
        void Setup(Guid id, string name, string texture, Vector2 position, List<Vector2> verts = default(List<Vector2>));
        void Update();
    }
}
