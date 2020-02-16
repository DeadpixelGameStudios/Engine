///
/// Comment out to run as Demo
///
//#define Game

using DemoCode;
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
            
#else
            GameMain game = new GameMain(engine);
            game.TestLevel(3);

            //Demo demo = new Demo(engine);
            //demo.DemoLevel();
#endif

            using (engine)
                engine.Run();

        }
    }
#endif
}
