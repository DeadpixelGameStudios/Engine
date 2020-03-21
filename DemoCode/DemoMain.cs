using DemoCode.Entities;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game1;
using Game1.Engine.Entity;
using Game1.Engine.Pathfinding;
using GameCode.Entities;
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

            IPathFinding path = new PathFinding(levelLoader.grid);
            engine.SetPathFindingGrid(levelLoader.grid, true);


            var star = engine.LoadEntity<Star>("Walls/Star", new Vector2(1500, 100));

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

                if (ent is Player)
                {
                    
                    Player player = (Player)ent;
                    player.injectPathFinding(path, star);
                    ent.EntityRequested += OnEntityRequested;
                }
            }

            if (playerCount > 1)
            {
                string uiSeperator = "Walls/" + playerCount.ToString() + "player";
                engine.LoadUI<UI>(uiSeperator, new Vector2(0, 0));
            }

            
            engine.LoadUI<HelpMe>("help-me", new Vector2(0,0));
            
        }

        private void OnEntityRequested(object sender, EntityRequestArgs e)
        {
            engine.LoadEntity<BrownPath>(e.Texture, e.Position);
        }

        private void OnLevelFinished(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
