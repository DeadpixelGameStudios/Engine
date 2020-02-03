using DemoCode.Entities;
using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Demo
    {
        private IEngineAPI engine;

        public Demo(IEngineAPI pEngine)
        {
            engine = pEngine;
        }

        public void DemoLevel()
        {
            engine.LoadEntity<Player>("Walls/player1", new Vector2(100, 100));
            engine.LoadEntity<Wall>("Walls/wall-closed", new Vector2(200, 200));
            engine.LoadEntity<Wall>("Walls/wall-closed", new Vector2(300, 300));
            engine.LoadEntity<Wall>("Walls/wall-closed", new Vector2(400, 400));
        }

    }
}
