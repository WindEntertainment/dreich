set(CPACK_GENERATOR "ZIP")
set(CPACK_PACKAGE_FILE_NAME "Logger")

set(CPACK_SOURCE_IGNORE_FILES
  "/build/;/.git/;/.vscode/;/CMakeFiles/;/CMakeCache.txt;/Makefile;/cmake_install.cmake"
)


install(DIRECTORY "${CMAKE_SOURCE_DIR}/Logger/" DESTINATION "./Logger")
install(FILES ${CMAKE_SOURCE_DIR}/conanfile.py DESTINATION .)
install(FILES ${CMAKE_SOURCE_DIR}/CMakeLists.txt DESTINATION .)
install(FILES ${CMAKE_SOURCE_DIR}/.env DESTINATION .)

include(CPack)
