using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1;
using System;

namespace GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain
    {
        private EngineAPI colonel;

        public GameMain()
        {
            
        }
        
        public void Test()
        {
            System.Console.WriteLine("running");

            colonel = new Kernel();
            colonel.Start();
            colonel.Load();
            colonel.UnLoad();

            
        }

    }
}
