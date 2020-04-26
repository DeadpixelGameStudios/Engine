using Engine.Entity;
using Engine.UI;

namespace GameCode.Entities
{
    public class Heart : GameEntity, IStaticUI
    {
        public void SetText(string text)
        {
            Text = text;
        }
    }
}

