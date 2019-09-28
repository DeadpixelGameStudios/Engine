using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    interface IInputObserverable
    {
        void notifyInput(Keys keys);
    }
}
