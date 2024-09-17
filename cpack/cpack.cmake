set(CPACK_GENERATOR "ZIP")
set(CPACK_PACKAGE_FILE_NAME "wind-cpp")

install(TARGETS wind-cpp LIBRARY DESTINATION .)

include(CPack)
