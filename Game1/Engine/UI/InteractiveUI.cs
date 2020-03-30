using Engine.Entity;
using System;

namespace Engine.UI
{
    public abstract class InteractiveUI : GameEntity, IInteractiveUI
    {
        public event EventHandler<EventArgs> OnClick;
        public event EventHandler<EventArgs> OnHover;

        public virtual void Clicked()
        {
            OnClick?.Invoke(this, new EventArgs());
        }

        public virtual void Hovering()
        {
            OnHover?.Invoke(this, new EventArgs());
        }
    }
}
