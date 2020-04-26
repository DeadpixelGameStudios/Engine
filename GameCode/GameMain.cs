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

            var ents = engine.LoadLevel(level);

            var key = engine.LoadEntity<Artifact>("key", new Vector2(900, 500));

            foreach (var ent in ents)
            {
                if(ent is Player)
                {
                    //var heart = engine.LoadUI<Heart>("heart", ent.Position, "Font");
                    //ent.PassUI(heart);
                    ent.LevelFinished += OnLevelFinished;
                    ent.EntityRequested += OnEntityRequested;
                    ent.PassIEntity(key);
                    playerList.Add(ent);
                }
                
            }

            if(playerCount > 1)
            {
                string uiSeperator = "Walls/" + playerCount.ToString() + "player";
                engine.LoadUI<UI>(uiSeperator, new Vector2(0, 0));
            }

            //Need logic for pause button first
            //var pauseButton = engine.LoadUI<Button>("pause", new Vector2(1550, 0));
            
            var patient = engine.LoadEntity<Patient>("Patient", new Vector2(950, 500));

            

            //Floor textures
            var floor1 = engine.LoadEntity<Background>("floor2", new Vector2(0, 0));
            var floor2 = engine.LoadEntity<Background>("floor2", new Vector2(1600, 0));
            var floor3 = engine.LoadEntity<Background>("floor2", new Vector2(0, 900));
            var floor4 = engine.LoadEntity<Background>("floor2", new Vector2(1600, 900));
        }
        



        private void OnEntityRequested(object sender, EntityRequestArgs e)
        {
            engine.LoadEntity<Trail>(e.Texture, e.Position);
        }

        private void OnLevelFinished(object sender, LevelFinishedArgs e)
        {
            if(e.Finisher.CanFinish)
            {
                var finishScreen = engine.LoadUI<FinishScreen>("finish-screen", new Vector2(0, 0));

                //will ideally be unloading things but this works
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
