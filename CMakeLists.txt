cmake_minimum_required(VERSION 3.22)

project(wind)

message("Building with CMake version: ${CMAKE_VERSION}")
message("WASM: ${BUILD_WASM}")
message("Output type: ${OUTPUT_TYPE}")
message(".Net: ${BUILD_DOTNET}")

if (BUILD_WASM)
  set(OUTPUT_TYPE "executable")
endif()

include(${CMAKE_SOURCE_DIR}/cmake/utils.cmake)

###===================================================================================##

list(APPEND CMAKE_PREFIX_PATH "${CMAKE_BINARY_DIR}")

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

###===================================================================================##

if (OUTPUT_TYPE STREQUAL "executable")
  import(executable OFF)
else()
  import(library OFF)
endif()

import(compiler ON)

###===================================================================================##

import(tests OFF)
import(dependencies OFF)
import(cpack ON)
