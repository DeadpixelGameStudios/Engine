using DemoCode.Entities;
using Game1;
using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Demo
    {
        private IEngineAPI engine;

        // put it like this so u can pass the property value in
        // will suffice for now
        DemoLevelLoader levelLoader;

        public Demo(IEngineAPI pEngine)
        {
            engine = pEngine;
        }

        public void DemoLevel(int playerNum)
        {
            levelLoader = new DemoLevelLoader();
            var level = levelLoader.requestLevel("test-level.tmx");

            int playerCount = 0;
            foreach (var asset in level.ToList())
            {
                if (asset.info.type.Equals(typeof(Player)))
                {
                    playerCount++;
                    if (playerCount > playerNum)
                    {
                        level.Remove(asset);
                        playerCount--;
                    }
                }
            }

            var ents = engine.LoadLevel(level);

            foreach (var ent in ents)
            {
                ent.LevelFinished += OnLevelFinished;
                ent.EntityRequested += OnEntityRequested;
            }

            engine.SetPathFindingGrid(levelLoader.levelWidth, levelLoader.levelHeight);
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
