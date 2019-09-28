using Game1.Engine.Entity;
using Game1.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Paddle : GameEntity, IInputObserver
    {
        private float playerAcceleration = 5f;
        private BasicInput inputKeys;

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

        public void subscirbeToInput(BasicInput keys)
        {
            Input.Subscribe(this, keys.allKeys);
            inputKeys = keys;
        }

        public void input(Keys key)
        {
            if (inputKeys.allKeys.Contains(key))
            {
                if(key == inputKeys.up)
                {
                    Update(new Vector2(0, (-1 - playerAcceleration)));
                }
                else if(key == inputKeys.down)
                {
                    Update(new Vector2(0, (-1 + playerAcceleration)));
                }
                else if(key == inputKeys.left)
                {
                    Update(new Vector2((-1 - playerAcceleration), 0));
                }
                else if (key == inputKeys.right)
                {
                    Update(new Vector2((-1 + playerAcceleration), 0));
                }
            }
        }
    }
}
