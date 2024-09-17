#!/bin/bash

source "$(dirname "$0")/global.sh"

call_dir=$(pwd)
root=""

while [[ "$#" -gt 0 ]]; do
  case $1 in
    --root) root="$2"; shift; ;;
    *) echo "Unknown parameter passed: $1"; exit; ;;
  esac
  shift
done

cd "$root" || exit

conan install . --build=missing --deployer-package=wind --deployer=full_deploy --deployer-folder="$root"/conan-deploy

cd "$call_dir" || exit
