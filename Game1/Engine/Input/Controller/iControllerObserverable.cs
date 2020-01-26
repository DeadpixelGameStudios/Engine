using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    public interface iControllerObserverable
    {
        void notifyGamePadInput(int playerIndex, Buttons gamePadButtons, GamePadThumbSticks thumbSticks);
    }
}
