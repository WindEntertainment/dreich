#pragma once

#include "keycodes.hpp"

extern "C" {

enum EventType {
  NONE,

  MOUSE_MOTION,
  MOUSE_BUTTON_DOWN,
  MOUSE_BUTTON_UP,

  KEY_DOWN,
  KEY_UP,

  QUIT,
};

struct WindEvent {
  EventType event;

  double x;
  double y;

  wind::Keycode keycode;
};

bool windInitRenderer();
void* windCreateWindow(int position_x, int position_y, int width, int height, char* title);
bool windPostInitRenderer();
WindEvent windPollEvent();
}