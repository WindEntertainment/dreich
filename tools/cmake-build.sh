#!/bin/bash

source "$(dirname "$0")/global.sh"

build_type=Release
target=game
skip_configure=false
testing_config=""
wasm=false

call_dir=$(pwd)
root=""

while [[ "$#" -gt 0 ]]; do
  case $1 in
    -sc|--skip-configure) skip_configure=true; ;;
    -bt|--build-type) build_type="$2"; shift ;;
    -wt|--with-testing) testing_config="--with-testing"; ;;
    -w|--wasm) wasm=true; ;;
    -t|--target) target="$2"; shift ;;
    --root) root="$2"; shift ;;
    *) echo "Unknown parameter passed: $1"; exit 1 ;;
  esac
  shift
done

if [ $wasm = true ]; then
  if [[ $skip_configure = false ]]; then
    wm run cmake-configure --build-type "$build_type" $testing_config -w
  fi
  emmake cmake --build "$root/build/web/build/$build_type" --parallel 10 --target "$target" --verbose
else
  if [[ $skip_configure = false ]]; then
    wm run cmake-configure --build-type "$build_type" $testing_config
  fi
  cmake --build "$root/build/app/build/$build_type" --parallel 10 --target "$target" --verbose
fi


cd "$call_dir" || exit
