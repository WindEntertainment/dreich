#pragma once
#include <math.hpp>

namespace wind {

class Window {
public:
  struct Config;

public:
  void init(void (*)(Config* self));
  void init(Config);

  void destroy();

  // setters
  void setTitle(const char* title);
  void setSize(vec2i size);
  void setPosition(vec2i position);
  void setResizable(bool resizable = true);
  void setVisiableCursor(bool visiableCursor = true);
  void setVsync(bool enable);

  // getters
  const char* title();
  vec2i size();
  vec2i position();
  bool isFullscreen();
  bool isResizable();
  bool isVisiableCursor();
  bool isVsync();
  int getFPS();

  // lifecycle
  void close();
  bool update();
  void show();
};

struct Window::Config {
  std::string title = "Wind";
  vec2i size = {800, 600};
  vec2i position = {0, 0};

  bool fullScreen = true;
  bool resizable = false;
  bool visibleCursor = true;
  bool vSync = true;

  vec2i openglVersion{3, 3};
};

} // namespace wind
