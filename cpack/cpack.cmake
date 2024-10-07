set(CPACK_GENERATOR "ZIP")

if(BUILD_WASM)
  set(CPACK_PACKAGE_FILE_NAME "wind-wasm")
else()
  set(CPACK_PACKAGE_FILE_NAME "wind")
endif()

install(TARGETS wind LIBRARY DESTINATION .)



if(BUILD_WASM)
  file(GLOB_RECURSE CONAN_FILES "conan-deploy/web/full_deploy/host/**/*.a")
  foreach(conan_file ${CONAN_FILES})
    message(${conan_file})
    install(FILES ${conan_file} DESTINATION .)
  endforeach()

  install(
    DIRECTORY "engine/bin/Release/web/net8.0/publish"
    DESTINATION .
    FILES_MATCHING
      PATTERN "*.wasm"
      PATTERN "*.html"
      PATTERN "*.js"
      PATTERN "*.css"
      PATTERN "*.json"
  )
else()
  file(GLOB_RECURSE CONAN_FILES "conan-deploy/app/full_deploy/host/**/*.dylib")
  foreach(conan_file ${CONAN_FILES})
    message(${conan_file})
    install(FILES ${conan_file} DESTINATION .)
  endforeach()

  file(GLOB_RECURSE CSHARP_FILES "engine/bin/Release/app/*/publish/*.dll")
  foreach(csharp_file ${CSHARP_FILES})
    message(${csharp_file})
    install(FILES ${csharp_file} DESTINATION .)
  endforeach()
endif()

include(CPack)
