///
/// Comment out whichever one you dont want
///

#define Game
//#define Demo

using System;
using Game1;


namespace GameCode
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            var engine = new EngineMain();

#if Game
            var game = new GameMain(engine);
            game.TestLevel();

            using (engine)
                engine.Run();
#elif Demo
            //write code for initing tech demo
            engine.Run();
#endif
        }
    }
#endif
}
