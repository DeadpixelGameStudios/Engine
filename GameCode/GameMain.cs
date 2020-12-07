using Microsoft.Xna.Framework;
using System;
using Engine;
using Engine.Entity;
using GameCode.Entities;
using System.Linq;
using System.Collections.Generic;
using Engine.UI;

namespace GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain
    {
        private IEngineAPI engine;

        private IInteractiveUI Button2Players;
        private IInteractiveUI Button3Players;
        private IInteractiveUI Button4Players;

        private iEntity startScreen;

        private List<iEntity> playerList;


        public GameMain(IEngineAPI pEngine)
        {
            engine = pEngine;
            playerList = new List<iEntity>();
        }

        
        public void Start()
        {
            // Load the start screen
            startScreen = engine.LoadUI<Background>("home-screen", new Vector2(0, 0), "Font");
            startScreen.DrawPriority = 0;
            startScreen.TextPosition = new Vector2(600, 600);
            startScreen.Text = "Find the artefact (key) throughout the level and deliver it to the patient to win.";

            Button2Players = engine.LoadUI<Button>("2player-button", new Vector2(200, 400));
            Button2Players.OnClick += Button2Players_OnClick;
            
            Button3Players = engine.LoadUI<Button>("3player-button", new Vector2(700, 400));
            Button3Players.OnClick += Button3Players_OnClick;

            Button4Players = engine.LoadUI<Button>("4player-button", new Vector2(1200, 400));
            Button4Players.OnClick += Button4Players_OnClick;
        }


        public void TestLevel(int playerNum)
        {
            var levelLoader = new LevelLoader();
            var level = levelLoader.requestLevel("big-level.tmx");

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

            // Place the patient and the artefact
            var patient = engine.LoadEntity<Patient>("Patient", new Vector2(950, 500));
            var key = engine.LoadEntity<Artifact>("key", new Vector2(1050, 1250));

            // Load the level and set up players
            var ents = engine.LoadLevel(level);
            foreach (var ent in ents)
            {
                if(ent is Player)
                {
                    ent.LevelFinished += OnLevelFinished;
                    ent.EntityRequested += OnEntityRequested;
                    ent.PassIEntity(key);
                    playerList.Add(ent);
                }
                
            }

            //Add the UI seperator
            if(playerCount > 1)
            {
                string uiSeperator = "Walls/" + playerCount.ToString() + "player";
                engine.LoadUI<UI>(uiSeperator, new Vector2(0, 0));
            }
            
            //Floor textures
            var floor1 = engine.LoadEntity<Background>("floor2", new Vector2(0, 0));
            var floor2 = engine.LoadEntity<Background>("floor2", new Vector2(1600, 0));
            var floor3 = engine.LoadEntity<Background>("floor2", new Vector2(0, 900));
            var floor4 = engine.LoadEntity<Background>("floor2", new Vector2(1600, 900));
        }
        
        

        private void OnEntityRequested(object sender, EntityRequestArgs e)
        {
            if(e.type == typeof(Trail))
            {
                engine.LoadEntity<Trail>(e.Texture, e.Position);
            }
            else if(e.type == typeof(Ice))
            {
                engine.LoadEntity<Ice>(e.Texture, e.Position);
            }
            else
            {
                engine.LoadEntity<Wall>(e.Texture, e.Position);
            }
        }

        private void OnLevelFinished(object sender, LevelFinishedArgs e)
        {
            if(e.Finisher.CanFinish)
            {
                var finishScreen = engine.LoadUI<FinishScreen>("finish-screen", new Vector2(0, 0));

                foreach (var player in playerList)
                {
                    player.InputAccepted = false;
                }

                var winningPlayer = engine.LoadUI<Wall>(e.Finisher.TextureString, new Vector2(800, 425));
                playerList.Remove(e.Finisher);

                int playerNum = 0;
                foreach (var player in playerList)
                {
                    playerNum++;
                    var newPlayer = engine.LoadUI<Wall>(player.TextureString, new Vector2(600 + (100 * playerNum), 750));
                }
            }
        }


        #region UI Events
        private void Button4Players_OnClick(object sender, EventArgs e)
        {
            TestLevel(4);
            Button4Players.OnClick -= Button4Players_OnClick;
            Button3Players.OnClick -= Button3Players_OnClick;
            Button2Players.OnClick -= Button2Players_OnClick;

            removeButtons();
        }

        private void Button3Players_OnClick(object sender, EventArgs e)
        {
            TestLevel(3);
            Button4Players.OnClick -= Button4Players_OnClick;
            Button3Players.OnClick -= Button3Players_OnClick;
            Button2Players.OnClick -= Button2Players_OnClick;

            removeButtons();
        }

        private void Button2Players_OnClick(object sender, EventArgs e)
        {
            TestLevel(2);
            Button4Players.OnClick -= Button4Players_OnClick;
            Button3Players.OnClick -= Button3Players_OnClick;
            Button2Players.OnClick -= Button2Players_OnClick;

            removeButtons();
        }

        private void removeButtons()
        {
            engine.UnLoad((iEntity)Button4Players);
            engine.UnLoad((iEntity)Button3Players);
            engine.UnLoad((iEntity)Button2Players);

            engine.UnLoad(startScreen);
        }
        #endregion


    }
}
