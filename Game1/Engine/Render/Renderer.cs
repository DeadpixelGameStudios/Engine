using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Render
{
    public class Renderer
    {
        private List<iEntity> entityList;
        private List<iEntity> uiList;

        private Viewport defaultView = new Viewport(0, 0, Kernel.ScreenWidth, Kernel.ScreenHeight);

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphDevice;

        private CameraManager cameraMan;

        public Renderer(GraphicsDevice graph)
        {
            graphDevice = graph;
            spriteBatch = new SpriteBatch(graphDevice);

            entityList = new List<iEntity>();
            uiList = new List<iEntity>();
        }

        public void Init()
        {
            cameraMan = new CameraManager();
            cameraMan.AddCameras();
        }

        public void addEntity(iEntity ent)
        {
            entityList.Add(ent);
        }

        public void addUI(iEntity ui)
        {
            uiList.Add(ui);
        }


        public void Draw()
        {
            foreach (var cam in cameraMan.Update())
            {
                graphDevice.Viewport = cam.viewPort;

                spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, cam.transform);
                drawEntities(entityList);
                spriteBatch.End();
            }

            graphDevice.Viewport = defaultView;

            spriteBatch.Begin();
            drawEntities(uiList);
            spriteBatch.End();

        }

        private void drawEntities(List<iEntity> drawList)
        {
            foreach (var entity in drawList)
            {
                //if (entity.Visible)
               // {
                    if (entity.UName.Contains("Player"))
                    {
                        spriteBatch.Draw(entity.Texture, entity.Position, null, Color.White, entity.Rotation, new Vector2((int)entity.Texture.Width / 2, (int)entity.Texture.Height / 2), 1, SpriteEffects.None, 0);

                    }
                    else
                    {
                        spriteBatch.Draw(entity.Texture, entity.Position, Color.White);

                    }
                //}

            }
        }

    }

}
