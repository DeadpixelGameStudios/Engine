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
    /// Abstraction for all entities in a specific game
    /// </summary>
    abstract class GameEntity : Entity
    {
        public static bool CheckPaddleBallCollistion(Paddle player, Ball ball)
        {
            return player.HitBox.Intersects(ball.HitBox);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Position, Color.White);
        }
    }
}
