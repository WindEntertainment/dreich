# add_executable(${PROJECT_NAME} ${HEADERS} ${SOURCES} ./native/src/main.cpp)
add_library(${PROJECT_NAME} SHARED ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PRIVATE ./native/include/)
