using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Game
    {
        private GameStateManager stateManager;
        private Kernel kernel;

        public Game()
        {
            stateManager = new GameStateManager();

            kernel = new Kernel();
            //Think we need this run call but not sure until we get both projects
            kernel.Run();



        }

        public void Load(object sender, EventArgs args)
        {
            //Kernel.Load();
        }

        public void Unload(object sender, EventArgs args)
        {
            //Kernel.Unload();
        }

        public void Update()
        {
            stateManager.Update();
        }
    }
}
