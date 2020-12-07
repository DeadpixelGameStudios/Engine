using Engine.Engine.UI;
using System;

namespace Engine.UI
{
    public interface IInteractiveUI : IUI
    {
        event EventHandler<EventArgs> OnClick;
        event EventHandler<EventArgs> OnHover;
    }
}
