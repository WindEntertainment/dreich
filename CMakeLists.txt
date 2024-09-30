cmake_minimum_required(VERSION 3.22)

project(wind)

message("Building with CMake version: ${CMAKE_VERSION}")
message("WASM: ${BUILD_WASM}")
message(".Net: ${BUILD_DOTNET}")

###===================================================================================##

list(APPEND CMAKE_PREFIX_PATH "${CMAKE_BINARY_DIR}")

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)
if(BUILD_WASM)
  set(CMAKE_C_COMPILER "emcc")
  set(CMAKE_CXX_COMPILER "em++")
endif()
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
target_link_libraries(${PROJECT_NAME} PUBLIC SDL2::SDL2)

if(BUILD_WASM)
  set(glad_DIR "./build/web/build/Debug/generators")
else()
  set(glad_DIR "./build/app/build/Debug/generators")
endif()

find_package(glad CONFIG REQUIRED)
target_link_libraries(${PROJECT_NAME} PUBLIC glad::glad)

find_package(OpenGL REQUIRED)
target_link_libraries(${PROJECT_NAME} PUBLIC ${OPENGL_LIBRARIES})
target_include_directories(${PROJECT_NAME} PUBLIC ${OPENGL_INCLUDE_DIR})


###===================================================================================##

if(BUILD_WASM)
  if(BUILD_DOTNET)
    add_custom_command(TARGET ${PROJECT_NAME}
      POST_BUILD
      COMMAND dotnet publish Wind.Web.csproj
      WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}/engine
      COMMENT "Running dotnet publish"
    )
  endif()
  set_target_properties(${PROJECT_NAME} PROPERTIES
    LINK_FLAGS "-s WASM=1 -s EXPORTED_FUNCTIONS='[\"_main\"]'"
  )
else()
  if(BUILD_DOTNET)
    add_custom_command(TARGET ${PROJECT_NAME}
      POST_BUILD
      COMMAND dotnet publish Wind.csproj
      WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}/engine
      COMMENT "Running dotnet publish"
    )
  endif()
endif()

###===================================================================================##

add_executable(${PROJECT_NAME} ${HEADERS} ${SOURCES} ./native/src/main.cpp)
target_include_directories(${PROJECT_NAME} PRIVATE ./native/include/)
target_link_libraries(${PROJECT_NAME} PUBLIC ${PROJECT_NAME})

###===================================================================================##

include(cpack/cpack.cmake)
