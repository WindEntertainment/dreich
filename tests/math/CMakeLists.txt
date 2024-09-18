project(math-tests)

find_package(Boost REQUIRED COMPONENTS unit_test_framework)

add_executable(${PROJECT_NAME} main.cpp)

target_link_libraries(${PROJECT_NAME} PUBLIC Boost::unit_test_framework)

target_compile_options(${PROJECT_NAME} PRIVATE --coverage)
target_link_options(${PROJECT_NAME} PRIVATE --coverage)

include(CTest)
add_test(NAME math COMMAND ${PROJECT_NAME})
