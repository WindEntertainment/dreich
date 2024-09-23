#include "logger.hpp"
#include "renderer.hpp"
#include "window.hpp"

int main() {
  windInitRenderer();
  windCreateWindow(0, 0, 800, 600, (char*)"Hello, From C++");
  return EXIT_SUCCESS;
}