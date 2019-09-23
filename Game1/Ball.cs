using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Ball : GameEntity
    {
        float mSpeed;

        Random random;

        public Ball()
        {
            random = new Random();

            mSpeed = random.Next(5, 9);

            Velocity = new Vector2(3, 0);

            Serve();
        }

        private void Serve()
        {
            SetPosition();

            // Generate a number that will send the ball in any direction 60 degrees to the right
            float rotation = (float)(Math.PI / 2 +
                (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));

            //  Checks a random number between 1 and 2,
            // Making ball launch left/right on a 50/50 chance bases
            if (random.Next(1, 3) == 2)
            {
                // If 2 set velocity.x to opposite value
                // Launch left
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
            }

            // Send the ball in the appropriate direction and speed moving right
            Velocity *= mSpeed;

        }

        private void SetPosition()
        {
            Position = new Vector2(Kernel.ScreenWidth / 2, Kernel.ScreenHeight / 2);
        }

        private void CheckWallCollision()
        {
            // this just looking if it collides with the bottom
            if (Position.Y < 0 || Position.Y > Kernel.ScreenHeight)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y * -1);
            }
        }

        public override void Update(GameTime gametime)
        {
            Position += Velocity;
            CheckWallCollision();

            base.Update(gametime);
        }
    }
}
