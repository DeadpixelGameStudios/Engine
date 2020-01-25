using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GameStateManager
    {
        public GameStateManager()
        {

        }

        public void HomePage()
        {
            RequestButton("DefaultButton", new Vector2(450, 700), new Vector2(200, 50), "2 Players", Color.Aqua, Color.Aquamarine);
            RequestButton("DefaultButton", new Vector2(550, 700), new Vector2(200, 50), "3 Players", Color.Aqua, Color.Aquamarine);
            RequestButton("DefaultButton", new Vector2(650, 700), new Vector2(200, 50), "4 Players", Color.Aqua, Color.Aquamarine);
        }

        public void PausePage()
        {

        }

        public void RequestButton(string texture, Vector2 position, Vector2 size, string text, Color color, Color altColor)
        {
            //to connect to game load
        }

        public void RemoveButton()
        {
            //to connect to game unload
        }


        public void Update()
        {

        }
    }
}
