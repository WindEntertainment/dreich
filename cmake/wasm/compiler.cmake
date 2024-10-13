set(CMAKE_C_COMPILER "emcc")
set(CMAKE_CXX_COMPILER "em++")

###===================================================================================##

if(BUILD_DOTNET)
  add_custom_command(TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND dotnet publish Wind.Web.csproj
    WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}/engine
    COMMENT "Running dotnet publish"
  )
endif()

set_target_properties(${PROJECT_NAME} PROPERTIES
  OUTPUT_NAME "wind"
  LINK_FLAGS "-s SIDE_MODULE=1"
)

###===================================================================================##
