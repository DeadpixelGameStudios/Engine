using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;
using Game1.Engine.Managers;
using Microsoft.Xna.Framework.Input;
using PS4Mono;

namespace Game1.Engine.Input
{
    class ControllerInput : iControllerObserverable, iManager
    {
        static List<EntityButton> m_entityButtonList = new List<EntityButton>();
        static List<iControllerObserver> m_subList = new List<iControllerObserver>();

        private static Dictionary<int, iControllerObserver> playerDict = new Dictionary<int, iControllerObserver>();
        private const int maxControllers = 4;

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
        

        public ControllerInput()
        {

        }

        public static void Subscribe(iControllerObserver sub, List<Buttons> buttons, int playerCount)
        {
            m_subList.Add(sub);
            m_entityButtonList.Add(new EntityButton(0, buttons));
            playerDict.Add(playerCount, sub);
        }

        public void Update()
        {
            for (int i = 0; i < maxControllers; i++)
            {
                if (GamePad.GetCapabilities(i).IsConnected)
                {
                    GamePadState gamePadState = GamePad.GetState(i);

                    foreach (EntityButton sub in m_entityButtonList)
                    {
                        foreach (Buttons button in sub.buttons)
                        {
                            if (gamePadState.IsButtonDown(button))
                            {
                                notifyGamePadInput(i, button, gamePadState.ThumbSticks);
                            }
                            else if(gamePadState.ThumbSticks.Left.X != 0 || gamePadState.ThumbSticks.Left.Y != 0)
                            {
                                notifyGamePadInput(i, 0, gamePadState.ThumbSticks);
                            }
                        }
                    }
                }
            }

        }

        public void notifyGamePadInput(int playerIndex, Buttons gamePadButtons, GamePadThumbSticks thumbSticks)
        {
            if(playerIndex < playerDict.Count)
            {
                playerDict[playerIndex].gamePadInput(gamePadButtons, thumbSticks);
            }
            
        }
    }
}
