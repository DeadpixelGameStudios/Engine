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
    class Player : GameEntity, IKeyboardInputObserver
    {

        private List<BasicInput> inputOptions = new List<BasicInput> { new BasicInput(Keys.W, Keys.S, Keys.A, Keys.D), new BasicInput(Keys.Up, Keys.Down, Keys.Left, Keys.Right),
                                                                        new BasicInput(Keys.I, Keys.K, Keys.J, Keys.L), new BasicInput(Keys.NumPad8, Keys.NumPad5, Keys.NumPad4, Keys.NumPad6) };
        private BasicInput inputKeys;
        private static int playerCount = 0;

        private float acceleration = 5f;
        
        public Player()
        {
            BasicInput keys;
            try
            {
                keys = inputOptions[playerCount];
            }
            catch
            {
                keys = new BasicInput();
            }
            
            KeyboardInput.Subscribe(this, keys.allKeys);
            inputKeys = keys;
            playerCount++;
        }

        public void input(Keys key)
        {
            if (inputKeys.allKeys.Contains(key))
            {
                if (key == inputKeys.up)
                {
                    Update(new Vector2(0, (-1 * acceleration)));
                }
                else if (key == inputKeys.down)
                {
                    Update(new Vector2(0, (acceleration)));
                }
                else if (key == inputKeys.left)
                {
                    Update(new Vector2((-1 * acceleration), 0));
                }
                else if (key == inputKeys.right)
                {
                    Update(new Vector2((acceleration), 0));
                }
            }
        }

        public void Update(Vector2 vel)
        {
            Position += vel;
        }
    }
}
