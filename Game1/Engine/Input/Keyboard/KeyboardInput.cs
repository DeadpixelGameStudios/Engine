using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Game1.Engine.Managers;

namespace Game1.Engine.Input
{
    public class KeyboardInput : IKeyboardInputObserverable, iManager
    {
        private static List<EntityKey> m_entityKeyList = new List<EntityKey>();
        private static List<IKeyboardInputObserver> m_subList = new List<IKeyboardInputObserver>();

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

        public KeyboardInput()
        {

        }

        public static void Subscribe(IKeyboardInputObserver sub, List<Keys> keys)
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
