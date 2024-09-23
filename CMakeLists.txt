cmake_minimum_required(VERSION 3.22)

project(wind)

message("Building with CMake version: ${CMAKE_VERSION}")

###===================================================================================##

list(APPEND CMAKE_PREFIX_PATH "${CMAKE_BINARY_DIR}")

set(CMAKE_CXX_STANDARD 23)
# set(CMAKE_C_COMPILER "emcc")
# set(CMAKE_CXX_COMPILER "em++")
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)
add_compile_options(-fms-extensions)

###===================================================================================##

if(ENABLE_TESTS AND CMAKE_BUILD_TYPE STREQUAL "Debug")
  message("Testing enabled")

  enable_testing()
  set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} --coverage -fprofile-arcs -ftest-coverage")

  add_custom_target(coverage
    COMMAND ${CMAKE_COMMAND} -E remove_directory ${CMAKE_SOURCE_DIR}/tests/coverage
    COMMAND mkdir ${CMAKE_SOURCE_DIR}/tests/coverage
    COMMAND tests
    COMMAND gcovr --root ${CMAKE_SOURCE_DIR} --exclude '.*/tests/.*' --exclude '.*/CMakeFiles/.*' --exclude '.*/build/.*' --html --html-details -o ${CMAKE_SOURCE_DIR}/tests/coverage/index.html
    WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
    COMMENT "Generating code coverage report..."
  )

  add_subdirectory(./tests/)
endif()

###===================================================================================##

set(HEADERS
    ./native/include/window.hpp
    ./native/include/renderer.hpp
)

set(SOURCES
    ./native/src/renderer.cpp
    ./native/src/window.cpp
)

add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./native/include/)

###===================================================================================##

find_package(fmt CONFIG REQUIRED)
target_link_libraries(${PROJECT_NAME} PUBLIC fmt::fmt-header-only)

find_package(SDL2 REQUIRED)
if(APPLE)
  target_link_libraries(${PROJECT_NAME} PUBLIC SDL2::SDL2)
else()
  target_link_libraries(${PROJECT_NAME} PUBLIC SDL2::SDL2main SDL2::SDL2)
endif()

set(glad_DIR "./build/Debug/generators")
find_package(glad CONFIG REQUIRED)
target_link_libraries(${PROJECT_NAME} PUBLIC glad::glad)

find_package(OpenGL REQUIRED)
target_link_libraries(${PROJECT_NAME} PUBLIC ${OPENGL_LIBRARIES})
target_include_directories(${PROJECT_NAME} PUBLIC ${OPENGL_INCLUDE_DIR})


###===================================================================================##

add_custom_command(TARGET ${PROJECT_NAME}
  POST_BUILD
  COMMAND dotnet publish
  WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
  COMMENT "Running dotnet publish"
)

###===================================================================================##

add_executable(${PROJECT_NAME}-exe ${HEADERS} ${SOURCES} ./native/src/main.cpp)
target_include_directories(${PROJECT_NAME}-exe PRIVATE ./native/include/)
target_link_libraries(${PROJECT_NAME}-exe PUBLIC ${PROJECT_NAME})

###===================================================================================##

include(cpack/cpack.cmake)
