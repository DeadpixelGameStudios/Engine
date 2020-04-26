using System;
using Engine.Entity;
using Engine.Input;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace GameCode.Entities
{
    public class HelpMe : GameEntity, IMouseInputObserver, IInteractiveUI
    {

        public event EventHandler<EventArgs> OnClick;
        public event EventHandler<EventArgs> OnHover;

        public HelpMe()
        {
            Transparency = 0.5f;
        }


        public void mouseInput(Vector2 pos, bool pressed)
        {
        }

        public virtual void Clicked()
        {
            OnClick?.Invoke(this, new EventArgs());
        }

        //Not sure if this is really needed?
        public virtual void Hovering()
        {
            Transparency = 0.5f;
            OnHover?.Invoke(this, new EventArgs());
        }

        public void SetText(string text)
        {
            throw new NotImplementedException();
        }
    }
}

