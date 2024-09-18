project(${MATH_LIB})

set(HEADERS
    ./include/math.hpp
)

set(SOURCES
    ./src/math.cpp
)

add_library(${PROJECT_NAME} STATIC ${HEADERS} ${SOURCES})
include_directories(./include/)
