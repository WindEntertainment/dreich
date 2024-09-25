namespace Wind.Logging {
  public class Logger(string loggerName, LoggerManager loggerManager) {
    public void Error(string message) {
      Message("Error", message);
    }

    public void Info(string message) {
      Message("Info", message);
    }

    public void Warn(string message) {
      Message("Warn", message);
    }

    public void Debug(string message) {
      Message("Debug", message);
    }

    private void Message(string level, string message) {
      _loggerManager.Message(_loggerName, level, message);
    }

    private LoggerManager _loggerManager = loggerManager;
    private string _loggerName = loggerName;
  };

  public class LoggerManager {
    public void Message(string env, string level, string message) {
      Console.WriteLine($"({env}) [{level}] {message}");
    }
  }
}