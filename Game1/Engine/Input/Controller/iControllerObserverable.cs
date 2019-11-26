using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS4Mono;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    interface iControllerObserverable
    {
        void notifyGamePadInput(int playerIndex, Buttons gamePadButtons, GamePadThumbSticks thumbSticks);
    }
}
