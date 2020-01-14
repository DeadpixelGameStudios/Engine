using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    interface iControllerObserver
    {
        void gamePadInput(Buttons gamePadButtons, GamePadThumbSticks thumbSticks);
    }
}
