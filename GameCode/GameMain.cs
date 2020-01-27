using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Game1;
using Game1.Engine.Entity;
using GameCode.Entities;
using Game1.Engine.UI;
using System.Linq;

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
        

        public void TestLevel(int playerNum)
        {
            var levelLoader = new LevelLoader();
            var level = levelLoader.requestLevel("test-level.tmx");

            int playerCount = 0;
            foreach(var asset in level.ToList())
            {
                if(asset.info.type.Equals(typeof(Player)))
                {
                    playerCount++;
                    if(playerCount > playerNum)
                    {
                        level.Remove(asset);
                        playerCount--;
                    }
                }
            }

            var ents = engine.LoadLevel(level);

            foreach(var ent in ents)
            {
                ent.LevelFinished += OnLevelFinished;
                ent.EntityRequested += OnEntityRequested;
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


        #region UI Testing
        //private IInteractiveUI startButton;
        //private IInteractiveUI Button2Players;
        //private IInteractiveUI Button3Players;
        //private IInteractiveUI Button4Players;

        //public void Start()
        //{
        //    //startButton = engine.LoadUI<Button>("start-button", new Vector2(700, 400));
        //    //startButton.OnClick += StartButton_OnClick;

        //    Button2Players = engine.LoadUI<Button>("2player-button", new Vector2(200, 400));
        //    Button2Players.OnClick += Button2Players_OnClick;

        //    Button3Players = engine.LoadUI<Button>("3player-button", new Vector2(700, 400));
        //    Button3Players.OnClick += Button3Players_OnClick;

        //    Button4Players = engine.LoadUI<Button>("4player-button", new Vector2(1200, 400));
        //    Button4Players.OnClick += Button4Players_OnClick;
        //}

        //private void Button4Players_OnClick(object sender, EventArgs e)
        //{
        //    TestLevel(4);
        //    Button4Players.OnClick -= Button4Players_OnClick;
        //    Button3Players.OnClick -= Button3Players_OnClick;
        //    Button2Players.OnClick -= Button2Players_OnClick;
        //}

        //private void Button3Players_OnClick(object sender, EventArgs e)
        //{
        //    TestLevel(3);
        //    Button4Players.OnClick -= Button4Players_OnClick;
        //    Button3Players.OnClick -= Button3Players_OnClick;
        //    Button2Players.OnClick -= Button2Players_OnClick;
        //}

        //private void Button2Players_OnClick(object sender, EventArgs e)
        //{
        //    TestLevel(2);
        //    Button4Players.OnClick -= Button4Players_OnClick;
        //    Button3Players.OnClick -= Button3Players_OnClick;
        //    Button2Players.OnClick -= Button2Players_OnClick;
        //}

        //private void StartButton_OnClick(object sender, EventArgs e)
        //{
        //    TestLevel(3);
        //    startButton.OnClick -= StartButton_OnClick;

        //}
        #endregion


    }
}
