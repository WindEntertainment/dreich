project(${RENDERER_LIB})

set(HEADERS
    ./include/renderer.hpp
)

set(SOURCES
    ./src/renderer.cpp
)

add_library(${PROJECT_NAME} STATIC ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./include/)

target_link_libraries(${PROJECT_NAME} PRIVATE math)