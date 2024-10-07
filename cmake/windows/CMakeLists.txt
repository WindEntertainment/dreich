if(BUILD_DOTNET)
  add_custom_command(TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND dotnet publish Wind.csproj
    WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}/engine
    COMMENT "Running dotnet publish"
  )
endif()

###===================================================================================##

include(${CMAKE_SOURCE_DIR}/cmake/windows/cpack.cmake)
