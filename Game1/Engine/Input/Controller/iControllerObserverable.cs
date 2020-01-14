using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    interface iControllerObserverable
    {
        void notifyGamePadInput(int playerIndex, Buttons gamePadButtons, GamePadThumbSticks thumbSticks);
    }
}
