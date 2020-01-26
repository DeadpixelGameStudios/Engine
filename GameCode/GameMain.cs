using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Game1;
using Game1.Engine.Entity;
using GameCode.Entities;

namespace GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain
    {
        private IEngineAPI engine;

        public GameMain(IEngineAPI pEngine)
        {
            engine = pEngine;
        }
        

        public void TestLevel()
        {
            var levelLoader = new LevelLoader();
            var ents = engine.LoadLevel(levelLoader.requestLevel("test-level.tmx"));

            int playerCount = 0;
            foreach(var ent in ents)
            {
                ent.LevelFinished += OnLevelFinished;
                ent.EntityRequested += OnEntityRequested;

                if(ent is Player)
                {
                    playerCount++;
                }
            }

            if(playerCount > 1)
            {
                string uiSeperator = "Walls/" + playerCount.ToString() + "player";
                engine.LoadUI<UI>(uiSeperator, new Vector2(0, 0));
            }
        }

        private void OnEntityRequested(object sender, EntityRequestArgs e)
        {
            engine.LoadEntity<Wall>(e.Texture, e.Position);
        }

        private void OnLevelFinished(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
