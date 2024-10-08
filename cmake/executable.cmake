add_executable(${PROJECT_NAME}-engine ${HEADERS} ${SOURCES} ./native/src/main.cpp)
target_include_directories(${PROJECT_NAME}-engine PRIVATE ./native/include/)
target_link_libraries(${PROJECT_NAME}-engine PUBLIC ${PROJECT_NAME})
