using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Paddle : GameEntity
    {
        public Paddle()
        {
            // Comes up with error if i did Velocity.Y = 11;
            // https://stackoverflow.com/questions/1747654/cannot-modify-the-return-value-error-c-sharp
            // Did it this way
            Velocity = new Vector2(Velocity.X, 11);
        }

        public void setPosition(float xPos, float yPos)
        {
            Position = new Vector2(xPos, yPos);
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public void Update(Vector2 vel)
        {
            Position += vel;
        }
    }
}
