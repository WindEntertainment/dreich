cmake_minimum_required(VERSION 3.22)

project(wind)

message("Building with CMake version: ${CMAKE_VERSION}")

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

set(MATH_LIB math)
set(RENDERER_LIB renderer)
set(LOGGER_LIB logger)

add_compile_options(-fms-extensions)

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

add_subdirectory(./Math/Native/)
add_subdirectory(./Renderer/Native)
add_subdirectory(./Logger/Native)

add_library(${PROJECT_NAME} SHARED empty.cpp)
target_link_libraries(${PROJECT_NAME} ${MATH_LIB} ${RENDERER_LIB} ${LOGGER_LIB})

add_custom_command(TARGET ${PROJECT_NAME}
  POST_BUILD
  COMMAND dotnet publish
  WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
  COMMENT "Running dotnet publish"
)

include(cpack/cpack.cmake)
