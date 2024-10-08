add_executable(${PROJECT_NAME} ${HEADERS} ${SOURCES} ./native/src/main.cpp)
target_include_directories(${PROJECT_NAME} PRIVATE ./native/include/)
