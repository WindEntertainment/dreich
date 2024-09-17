set(CPACK_GENERATOR "ZIP")
set(CPACK_PACKAGE_FILE_NAME "wind")

install(TARGETS wind LIBRARY DESTINATION .)

file(GLOB_RECURSE CONAN_FILES "conan-deploy/**/*.dylib")
foreach(conan_file ${CONAN_FILES})
  message(${conan_file})
  install(FILES ${conan_file} DESTINATION .)
endforeach()

file(GLOB_RECURSE CSHARP_FILES "Engine/bin/Release/*/publish/*.dll")
foreach(csharp_file ${CSHARP_FILES})
  message(${csharp_file})
  install(FILES ${csharp_file} DESTINATION .)
endforeach()

include(CPack)
