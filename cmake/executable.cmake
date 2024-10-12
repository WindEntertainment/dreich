# add_executable(${PROJECT_NAME} ${HEADERS} ${SOURCES} ./native/src/main.cpp)

# set(EXE_SOURCES
# ./native/src/main.cpp
# ./native/includes/window.cpp
# )

# target_include_directories(${PROJECT_NAME} PRIVATE ./native/include/)



# Set properties to export functions
# set_target_properties(${PROJECT_NAME} PROPERTIES
#     OUTPUT_NAME "my_wasm_library"  # The output name for the .a file
# )

set(HEADERS
  ./native/include/native/math.hpp
  ./native/include/native/logger.hpp
  ./native/include/native/window.hpp
)

set(SOURCES
  ./native/src/window.cpp
)

# Create an executable for the WASM output (this step will produce the .wasm file)
add_executable(${PROJECT_NAME} ${HEADERS} ${SOURCES})
target_include_directories(${PROJECT_NAME} PUBLIC ./native/include/)


# Add a custom command to ensure that the .wasm file is created after linking
# add_custom_command(TARGET ${PROJECT_NAME}-exe
#     POST_BUILD
#     COMMAND emcc -o ${CMAKE_CURRENT_BINARY_DIR}/my_wasm_library.wasm $<TARGET_FILE:${PROJECT_NAME}-exe>
#     DEPENDS ${PROJECT_NAME}-exe
# )
