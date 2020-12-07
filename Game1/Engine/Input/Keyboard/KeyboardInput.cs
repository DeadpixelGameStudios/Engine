﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Engine.Managers;
using System;
using System.Linq;

namespace Engine.Input
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
            m_entityKeyList.Add(new EntityKey(0, keys));
            m_subList.Add(sub);
        }

        public static void UnSubscribe(IKeyboardInputObserver sub)
        {
            m_subList.Remove(sub);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (var sub in m_entityKeyList.ToList())
            {
                foreach(var key in sub.keys.ToList())
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
            foreach(var sub in m_subList.ToList())
            {
                sub.input(key);
            }
        }
    }
}
