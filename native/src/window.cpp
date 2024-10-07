
#ifdef _WIN32
#include <SDL.h>
#include <SDL_error.h>
#else
#include <SDL2/SDL.h>
#include <SDL2/SDL_error.h>
#endif
#include <glad/glad.h>

#include "logger.hpp"
#include "window.hpp"

ILoggerStream* Logger::stream = new ConsoleStream();
Logger Logger::native = Logger();

bool windInitRenderer() {
  Logger::native.info("Called native::windInitRenderer");

  if (SDL_Init(SDL_INIT_EVERYTHING) != 0) {
    Logger::native.error("Failed initialization SDL: {}", SDL_GetError());
    return false;
  }

  SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
  SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);
  SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);

  return true;
}

void* windCreateWindow(int position_x, int position_y, int width, int height, char* title) {
  Logger::native.info("Called native::windCreateWindow");

  SDL_Window* window = NULL;

  window = SDL_CreateWindow(
    title,
    position_x,
    position_y,
    width,
    height,
    SDL_WINDOW_SHOWN | SDL_WINDOW_OPENGL);

  if (window == NULL) {
    Logger::native.error("Failed create window: {}", SDL_GetError());
    return nullptr;
  }

  SDL_GLContext glContext = SDL_GL_CreateContext(window);
  if (glContext == nullptr) {
    Logger::native.error("Failed create OpenGL context for window: {}", SDL_GetError());

    SDL_DestroyWindow(window);
    return nullptr;
  }

  return window;
}

bool windPostInitRenderer() {
  if (!gladLoadGLLoader((GLADloadproc)SDL_GL_GetProcAddress)) {
    Logger::Logger::native.error("Failed to load GLLoader (GLAD)");
    return false;
  }

  return true;
}
