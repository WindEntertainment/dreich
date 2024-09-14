project(renderer)

set(HEADERS
    ./include/renderer.hpp
)

set(SOURCES 
    ./src/renderer.cpp
)

add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./include/)