using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.UI
{
    public abstract class InteractiveUI : IInteractiveUI
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
