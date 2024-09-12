using Wind.Mathf;
using static SDL2.SDL;

namespace Wind {
  public class Window {
    public void Close() {
      SDL_DestroyWindow(_window);
    }

    public Window(string title, Vector2i size) {
      _window = SDL_CreateWindow(
        title,
        SDL_WINDOWPOS_CENTERED,
        SDL_WINDOWPOS_CENTERED,
        size.x,
        size.y,
        SDL_WindowFlags.SDL_WINDOW_SHOWN // | SDL_WindowFlags.SDL_WINDOW_OPENGL
      );

      if (_window == IntPtr.Zero)
        throw new InvalidOperationException($"Window could not be created! SDL_Error: {SDL_GetError()}");
    }

    ~Window() {
      Close();
    }

    private readonly IntPtr _window;
  }
}
