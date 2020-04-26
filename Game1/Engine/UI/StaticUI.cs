using Engine.Entity;
using Engine.UI;

namespace Engine.Engine.UI
{
    class StaticUI : GameEntity, IStaticUI
    {
        public void SetText(string text)
        {
            Text = text;
        }
    }
}
