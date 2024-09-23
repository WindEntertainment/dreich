using Wind.Math;
using static SDL2.SDL;

namespace Wind {
  public class Window {
    public void Close() {
    }

    public Window(string title, Vec2<int> size) {
    }

    ~Window() {
      Close();
    }
  }
}