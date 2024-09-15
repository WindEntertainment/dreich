project(logger)

set(HEADERS
    ./include/logger.hpp
)

set(SOURCES
    ./src/logger.cpp
)

add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./include/)
