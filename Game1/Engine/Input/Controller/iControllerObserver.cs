using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    public interface iControllerObserver
    {
        void gamePadInput(Buttons gamePadButtons, GamePadThumbSticks thumbSticks);
    }
}
