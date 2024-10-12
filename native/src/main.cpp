#include "native/logger.hpp"
#include "native/window.hpp"

int main() {
  windInitRenderer();
  windCreateWindow(0, 0, 800, 600, (char*)"Hello, From C++");
  return EXIT_SUCCESS;
}
