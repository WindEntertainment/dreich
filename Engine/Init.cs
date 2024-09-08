using static SDL2.SDL;

using Wind.Logging;
using Wind.Mathf;

namespace Wind
{
    public partial class WindEngine
    {
        static private Logger? _logger;

        public static void Init()
        {
            WindServices.Instance.Register(new LoggerManager());
            _logger = new Logger("WindEngine", WindServices.Instance.Get<LoggerManager>());

            _logger.Info("Launch engine initialization process");

            if (SDL_Init(SDL_INIT_VIDEO) < 0)
                _logger.Error($"SDL could not initialize! SDL_Error: {SDL_GetError()}");

            try
            {
                WindServices.Instance.Register(new Window(
                    "Hello World!",
                    new Vector2i(800, 600)
                ));
            }
            catch (Exception ex)
            {
                _logger?.Error(ex.Message);
            }
        }

        public static void Dispose()
        {
            SDL_Quit();
        }
    }
}