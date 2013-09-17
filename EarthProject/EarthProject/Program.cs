using System;

namespace EarthProject
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EarthProject game = new EarthProject())
            {
                game.Run();
            }
        }
    }
#endif
}

