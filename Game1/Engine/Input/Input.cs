using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Input
{
    public class Input
    {
        static Vector2 direction;

        public Input()
        {

        }

        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            direction = new Vector2(0, 0);

            KeyboardState keyboardState = Keyboard.GetState();
            int mPlayerAcceleration = 5;

            if (playerIndex == PlayerIndex.One)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    direction.Y = -1 - mPlayerAcceleration;
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    direction.Y = 1 + mPlayerAcceleration;
                }
            }

            if (playerIndex == PlayerIndex.Two)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    direction.Y = -1 - mPlayerAcceleration;
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    direction.Y = 1 + mPlayerAcceleration;
                }
            }

            return direction;
        }
    }
}
