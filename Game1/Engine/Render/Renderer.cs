using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        /// <summary>
        /// Initialise the renderer and spawn camera manager
        /// </summary>
        public void Init()
        {
            cameraMan = new CameraManager();
            cameraMan.AddCameras();
        }

        /// <summary>
        /// Add an entity to render list
        /// </summary>
        /// <param name="ent">The entity to add</param>
        public void addEntity(iEntity ent)
        {
            entityList.Add(ent);
        }

        /// <summary>
        /// Add UI element to be rendered
        /// </summary>
        /// <param name="ui">The UI element to draw</param>
        public void addUI(iEntity ui)
        {
            uiList.Add(ui);
        }

        /// <summary>
        /// Draws all entities to be rendered
        /// </summary>
        public void Draw()
        {
            foreach (var cam in cameraMan.Update())
            {
                graphDevice.Viewport = cam.viewPort;

                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.transform);
                drawEntities(entityList);
                spriteBatch.End();
            }

            graphDevice.Viewport = defaultView;

            spriteBatch.Begin();
            drawEntities(uiList);
            spriteBatch.End();

        }

        /// <summary>
        /// Takes a list of entities to draw with SpriteBatch
        /// </summary>
        /// <param name="drawList"></param>
        private void drawEntities(List<iEntity> drawList)
        {
            foreach (var entity in drawList)
            {
                spriteBatch.Draw(entity.Texture, entity.Position, null, Color.White, entity.Rotation, new Vector2(0, 0), 1, SpriteEffects.None, entity.DrawPriority);
            }
        }

    }

}
