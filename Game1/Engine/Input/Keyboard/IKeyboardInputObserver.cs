using Microsoft.Xna.Framework.Input;
using System;

namespace Game1.Engine.Input
{
    public interface IKeyboardInputObserver
    {
        void input(Keys keys);
    }
}
