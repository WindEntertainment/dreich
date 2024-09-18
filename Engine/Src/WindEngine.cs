using Wind.Logging;
using Wind.Math;
using static SDL2.SDL;

namespace Wind {
  public class WindEngine {
    private Logger logger;
    private Window window;

    public WindEngine() {
      WindServices.Instance.Register(new LoggerManager());
      logger = new Logger("WindEngine", WindServices.Instance.Get<LoggerManager>());

      logger.Info("Launch engine initialization process");

      if (SDL_Init(SDL_INIT_VIDEO) < 0)
        logger.Error($"SDL could not initialize! SDL_Error: {SDL_GetError()}");

      window = new Window(
          "Hello World!",
          new Vector2i(800, 600)
      );

      WindServices.Instance.Register(window);
    }

    public void Loop() {
      logger.Info("Launch engine loop");

      bool quit = false;

      var glContext = SDL_GL_CreateContext(window.window);

      while (!quit) {
        while (SDL_PollEvent(out SDL_Event e) != 0) {
          if (e.type == SDL_EventType.SDL_QUIT)
            quit = true;
        }

        OpenGL.Gl.ClearColor(0, 0, 0, 0);
      }

      SDL_GL_DeleteContext(glContext);
      Dispose();
    }

    private void Dispose() {
      SDL_Quit();
    }
  }
}