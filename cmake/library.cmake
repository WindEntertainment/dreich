set(HEADERS
  ./native/include/native/math.hpp
  ./native/include/native/logger.hpp
  ./native/include/native/window.hpp
)

set(SOURCES
  ./native/src/window.cpp
)

add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PUBLIC ./native/include/)
