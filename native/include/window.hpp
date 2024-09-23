#pragma once

extern "C" {
bool windInitRenderer();
void* windCreateWindow(int position_x, int position_y, int width, int height, char* title);
}