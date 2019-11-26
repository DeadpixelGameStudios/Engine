using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;
using Microsoft.Xna.Framework.Input;
using PS4Mono;

namespace Game1.Engine.Input
{
    class ControllerInput : iControllerObserverable
    {
        static List<EntityButton> m_entityButtonList = new List<EntityButton>();
        static List<iControllerObserver> m_subList = new List<iControllerObserver>();

        private static Dictionary<int, iControllerObserver> playerDict = new Dictionary<int, iControllerObserver>();

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

        public static void Subscribe(iControllerObserver sub, List<Buttons> buttons, int playerCOun)
        {
            m_subList.Add(sub);
            m_entityButtonList.Add(new EntityButton(0, buttons));
            playerDict.Add(playerCOun, sub);
        }

        public void Update()
        {
            for(var i = 0; i < 5; ++i)
            {
                GamePadState gamePadState = GamePad.GetState(controllerConnected - 1);
                Console.WriteLine(gamePadState.IsConnected + " " + i);
            }

            for (int i = 0; i < 4; i++)
            {
                //GamePadState gamePadState = GamePad.GetState(controllerConnected - 1);

                if (GamePad.GetCapabilities(i).IsConnected)
                {
                    GamePadState gamePadState = GamePad.GetState(i);

                    foreach (EntityButton sub in m_entityButtonList)
                    {
                        foreach (Buttons button in sub.buttons)
                        {
                            if (gamePadState.IsButtonDown(button)) //Ps4Input.Ps4CheckAsnyc(controllerConnected - 1, button))
                            {
                                
                                notifyGamePadInput(i, button);
                            }
                        }
                    }
                }

                //foreach (EntityButton sub in m_entityButtonList)
                //{
                //    foreach (Buttons button in sub.buttons)
                //    {
                //        if (gamePadState.IsButtonDown(button)) //Ps4Input.Ps4CheckAsnyc(controllerConnected - 1, button))
                //        {
                //            notifyGamePadInput(controllerConnected - 1, button);
                //        }
                //    }
                //}
            }

        }

        public void notifyGamePadInput(int playerIndex, Buttons gamePadButtons)
        {
            //m_subList.ForEach(sub => sub.gamePadInput(playerIndex, gamePadButtons));
            if(playerIndex < playerDict.Count)
            {
                playerDict[playerIndex].gamePadInput(playerIndex, gamePadButtons);
            }
            
        }
    }
}
