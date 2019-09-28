using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;

namespace Game1.Engine.Input
{
    public class Input : IInputObserverable
    {
        static Vector2 direction;
        private static List<EntityKey> m_entityKeyList = new List<EntityKey>();
        private static List<IInputObserver> m_subList = new List<IInputObserver>();

        private struct EntityKey
        {
            public EntityKey(int id, List<Keys> key)
            {
                uid = id;
                keys = key;
            }

            public int uid;
            public List<Keys> keys;
        }

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

        public static void Subscribe(IInputObserver sub, List<Keys> keys)
        {
            m_subList.Add(sub);
            m_entityKeyList.Add(new EntityKey(0, keys));
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            foreach(var sub in m_entityKeyList)
            {
                foreach(var key in sub.keys)
                {
                    if (keyboardState.IsKeyDown(key))
                    {
                        notifyInput(key);
                    }
                }
            }
        }

        public void notifyInput(Keys key)
        {
            foreach(var sub in m_subList)
            {
                sub.input(key);
            }
        }
    }
}
