using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.UI
{
    public interface IInteractiveUI
    {
        event EventHandler<EventArgs> OnClick;
        event EventHandler<EventArgs> OnHover;
    }
}
