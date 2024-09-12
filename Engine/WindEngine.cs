using static SDL2.SDL;

using Wind.Logging;
using Wind.Mathf;

namespace Wind
{
    public class WindEngine
    {
        private Logger _logger;
        private Window _window;

        public WindEngine()
        {
            WindServices.Instance.Register(new LoggerManager());
            _logger = new Logger("WindEngine", WindServices.Instance.Get<LoggerManager>());

            _logger.Info("Launch engine initialization process");

            if (SDL_Init(SDL_INIT_VIDEO) < 0)
                _logger.Error($"SDL could not initialize! SDL_Error: {SDL_GetError()}");

            try
            {
                _window = new Window(
                    "Hello World!",
                    new Vector2i(800, 600)
                );
                WindServices.Instance.Register(_window);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public void Loop()
        {
            _logger.Info("Launch engine loop");

            bool quit = false;

            while (!quit)
            {
                while (SDL_PollEvent(out SDL_Event e) != 0)
                {
                    if (e.type == SDL_EventType.SDL_QUIT)
                        quit = true;
                }

                // OpenGL.Gl.ClearColor(0, 0, 0, 0);
            }

            Dispose();
        }

        private void Dispose()
        {
            SDL_Quit();
        }
    }
}