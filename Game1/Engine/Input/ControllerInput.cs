using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using PS4Mono;

namespace Game1.Engine.Input
{
    class ControllerInput : iControllerObserverable
    {
        static List<EntityButton> m_entityButtonList = new List<EntityButton>();
        static List<iControllerObserver> m_subList = new List<iControllerObserver>();

        private struct EntityButton
        {
            public int uid;
            public List<Buttons> buttons;

            public EntityButton(int id, List<Buttons> button)
            {
                uid = id;
                buttons = button;
            }
        }

        public int controllerConnected
        {
            get
            {
                return Ps4Input.Ps4Count;
            }
        }

        public ControllerInput()
        {

        }

        public static void Subscribe(iControllerObserver sub, List<Buttons> buttons)
        {
            m_subList.Add(sub);
            m_entityButtonList.Add(new EntityButton(0, buttons));
        }

        public void Update()
        {
            for (int i = 0; i < controllerConnected; i++)
            {
                GamePadState gamePadState = GamePad.GetState(controllerConnected - 1);

                foreach (EntityButton sub in m_entityButtonList)
                {
                    foreach (Buttons button in sub.buttons)
                    {
                        if (gamePadState.IsButtonDown(button)) //Ps4Input.Ps4CheckAsnyc(controllerConnected - 1, button))
                        {
                            notifyGamePadInput(controllerConnected - 1, button);
                        }
                    }
                }
            }

        }

        public void notifyGamePadInput(int playerIndex, Buttons gamePadButtons)
        {
            m_subList.ForEach(sub => sub.gamePadInput(playerIndex, gamePadButtons));
        }
    }
}
