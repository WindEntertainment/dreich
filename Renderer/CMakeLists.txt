cmake_minimum_required(VERSION 3.22)

project(renderer)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

message("Building with CMake version: ${CMAKE_VERSION}")

set(HEADERS
    ./include/renderer.hpp
)

set(SOURCES 
    ./src/renderer.cpp
)

add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./include/)

if (CMAKE_CXX_COMPILER_ID STREQUAL "GNU" OR CMAKE_CXX_COMPILER_ID STREQUAL "Clang")
    target_compile_options(${PROJECT_NAME} PRIVATE -fms-extensions)
endif()

# option(ENABLE_TESTS "Enable tests" OFF)

# if(ENABLE_TESTS AND CMAKE_BUILD_TYPE STREQUAL "Debug")
#   message("Testing enabled")

#   enable_testing()
#   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} --coverage -fprofile-arcs -ftest-coverage")

#   add_custom_target(coverage
#     COMMAND ${CMAKE_COMMAND} -E remove_directory ${CMAKE_SOURCE_DIR}/tests/coverage
#     COMMAND mkdir ${CMAKE_SOURCE_DIR}/tests/coverage
#     COMMAND game-tests
#     COMMAND wind-tests
#     COMMAND gcovr --root ${CMAKE_SOURCE_DIR} --exclude '.*/tests/.*' --exclude '.*/CMakeFiles/.*' --exclude '.*/build/.*' --html --html-details -o ${CMAKE_SOURCE_DIR}/tests/coverage/index.html
#     WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
#     COMMENT "Generating code coverage report..."
#   )

#   add_subdirectory(./tests/)
# endif()