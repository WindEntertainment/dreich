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

      window = new Window("", new());
      WindServices.Instance.Register(window);
    }

    public void Loop() {
      logger.Info("Launch engine loop");

      Dispose();
    }

    private void Dispose() {
    }
  }
}