using System;


namespace King_of_Thieves
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                //git sanity test
                game.Run();
            }
        }
    }
#endif
}

