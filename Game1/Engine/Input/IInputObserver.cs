using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    public interface IInputObserver
    {
        void input(Keys keys);
    }
}
