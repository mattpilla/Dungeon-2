using System;

namespace Dungeon_2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (D2 game = new D2())
            {
                game.Run();
            }
        }
    }
#endif
}

