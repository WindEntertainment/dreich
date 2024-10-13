#include <iostream>

extern "C" {
void log(const char* message) {
  std::cout << "Log: " << message << std::endl;
}
}
