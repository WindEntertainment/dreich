using static SDL2.SDL;

namespace Wind
{
    public partial class WindEngine
    {
        public static void Loop()
        {
            if (_logger == null)
            {
                Console.WriteLine("You must initialize the engine before entering the main loop!");
                return;
            }

            _logger.Info("Launch engine loop");

            bool quit = false;
            SDL_Event e;

            while (!quit)
            {
                while (SDL_PollEvent(out e) != 0)
                {
                    if (e.type == SDL_EventType.SDL_QUIT)
                    {
                        quit = true;
                    }
                }
            }
        }
    }
}