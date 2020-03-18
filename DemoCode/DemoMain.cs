using DemoCode.Entities;
using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            engine.LoadEntity<DemoPlayer>("Walls/player1", new Vector2(300, 300));

            List<Vector2> verts = new List<Vector2> { new Vector2(0,20), new Vector2(25, 0), new Vector2(50, 20), new Vector2(40, 50), new Vector2(10, 50) };
            engine.LoadEntity<DemoWall>("Walls/pentagon", new Vector2(400, 400), verts);

            List<Vector2> vertsSept = new List<Vector2> { new Vector2(25,0), new Vector2(50,14), new Vector2(50,31), new Vector2(24,50), new Vector2(13, 50), new Vector2(0, 30), new Vector2(0, 14) };
            engine.LoadEntity<DemoWall>("Walls/random hex", new Vector2(500, 500), vertsSept);

            List<Vector2> vertsHex = new List<Vector2> { new Vector2(100, 0), new Vector2(200, 48), new Vector2(200, 129), new Vector2(100, 200), new Vector2(0, 128), new Vector2(0, 48) };
            engine.LoadEntity<DemoWall>("Walls/reg-hex", new Vector2(150, 400), vertsHex);

        }

    }
}
