#include <SDL2/SDL.h>
#include <SDL2/SDL_error.h>
#include <SDL_events.h>
#include <SDL_keycode.h>
#include <SDL_mouse.h>
#include <glad/glad.h>
#include <map>

#include "logger.hpp"
#include "window.hpp"

wind::Keycode mapSdlKeyToKeycode(SDL_Keycode key);
wind::Keycode mapSdlButtonToKeyCode(Uint8 button);

ILoggerStream* Logger::stream = new ConsoleStream();
Logger Logger::native = Logger();

bool windInitRenderer() {
  Logger::native.info("Called native::windInitRenderer");

  if (SDL_Init(SDL_INIT_EVERYTHING) != 0) {
    Logger::native.error("Failed initialization SDL: {}", SDL_GetError());
    return false;
  }

  SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
  SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);
  SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);

  return true;
}

void* windCreateWindow(int position_x, int position_y, int width, int height, char* title) {
  Logger::native.info("Called native::windCreateWindow");

  SDL_Window* window = NULL;

  window = SDL_CreateWindow(
    title,
    position_x,
    position_y,
    width,
    height,
    SDL_WINDOW_SHOWN | SDL_WINDOW_OPENGL);

  if (window == NULL) {
    Logger::native.error("Failed create window: {}", SDL_GetError());
    return nullptr;
  }

  SDL_GLContext glContext = SDL_GL_CreateContext(window);
  if (glContext == nullptr) {
    Logger::native.error("Failed create OpenGL context for window: {}", SDL_GetError());

    SDL_DestroyWindow(window);
    return nullptr;
  }

  return window;
}

bool windPostInitRenderer() {
  if (!gladLoadGLLoader((GLADloadproc)SDL_GL_GetProcAddress)) {
    Logger::Logger::native.error("Failed to load GLLoader (GLAD)");
    return false;
  }

  return true;
}

WindEvent windPollEvent() {

  WindEvent windEvent;
  SDL_Event sdlEvent;

  if (!SDL_PollEvent(&sdlEvent))
    windEvent.event = EventType::NONE;
  else {
    switch (sdlEvent.type) {
    case SDL_QUIT:
      windEvent.event = EventType::QUIT;
      break;
    case SDL_KEYDOWN:
      windEvent.event = EventType::KEY_DOWN;
      windEvent.keycode = mapSdlKeyToKeycode(sdlEvent.key.keysym.sym);
      break;
    case SDL_KEYUP:
      windEvent.event = EventType::KEY_UP;
      windEvent.keycode = mapSdlKeyToKeycode(sdlEvent.key.keysym.sym);
      break;
    case SDL_MOUSEBUTTONDOWN:
      windEvent.event = EventType::MOUSE_BUTTON_DOWN;
      windEvent.keycode = mapSdlButtonToKeyCode(sdlEvent.button.button);
      break;
    case SDL_MOUSEBUTTONUP:
      windEvent.event = EventType::MOUSE_BUTTON_UP;
      windEvent.keycode = mapSdlButtonToKeyCode(sdlEvent.button.button);
      break;
    case SDL_MOUSEMOTION:
      windEvent.event = EventType::MOUSE_MOTION;
      windEvent.x = sdlEvent.motion.x;
      windEvent.y = sdlEvent.motion.y;
      break;
    }

    return windEvent;
  }

  return windEvent;
}

wind::Keycode mapSdlButtonToKeyCode(Uint8 button) {
  static std::map<SDL_Keycode, wind::Keycode> sdlButtonToKeycode = {
    {SDL_BUTTON_LEFT, wind::Keycode::M_ButtonLeft},
    {SDL_BUTTON_RIGHT, wind::Keycode::M_ButtonRight},
    {SDL_BUTTON_MIDDLE, wind::Keycode::M_ButtonMiddle},
  };

  if (sdlButtonToKeycode.contains(button))
    return sdlButtonToKeycode[button];
  return wind::Keycode::Unknown;
}

wind::Keycode mapSdlKeyToKeycode(SDL_Keycode key) {
  static std::map<SDL_Keycode, wind::Keycode> sdlKeyToKeycode = {
    {SDLK_UNKNOWN, wind::Keycode::Unknown},
    {SDLK_RETURN, wind::Keycode::K_Return},
    {SDLK_ESCAPE, wind::Keycode::K_Escape},
    {SDLK_BACKSPACE, wind::Keycode::K_Backspace},
    {SDLK_TAB, wind::Keycode::K_Tab},
    {SDLK_SPACE, wind::Keycode::K_Space},
    {SDLK_EXCLAIM, wind::Keycode::K_Exclaim},
    {SDLK_QUOTEDBL, wind::Keycode::K_QuoteDbl},
    {SDLK_HASH, wind::Keycode::K_Hash},
    {SDLK_PERCENT, wind::Keycode::K_Percent},
    {SDLK_DOLLAR, wind::Keycode::K_Dollar},
    {SDLK_AMPERSAND, wind::Keycode::K_Ampersand},
    {SDLK_QUOTE, wind::Keycode::K_Quote},
    {SDLK_LEFTPAREN, wind::Keycode::K_LeftParen},
    {SDLK_RIGHTPAREN, wind::Keycode::K_RightParen},
    {SDLK_ASTERISK, wind::Keycode::K_Asterisk},
    {SDLK_PLUS, wind::Keycode::K_Plus},
    {SDLK_COMMA, wind::Keycode::K_Comma},
    {SDLK_MINUS, wind::Keycode::K_Minus},
    {SDLK_PERIOD, wind::Keycode::K_Period},
    {SDLK_SLASH, wind::Keycode::K_Slash},
    {SDLK_0, wind::Keycode::K_0},
    {SDLK_1, wind::Keycode::K_1},
    {SDLK_2, wind::Keycode::K_2},
    {SDLK_3, wind::Keycode::K_3},
    {SDLK_4, wind::Keycode::K_4},
    {SDLK_5, wind::Keycode::K_5},
    {SDLK_6, wind::Keycode::K_6},
    {SDLK_7, wind::Keycode::K_7},
    {SDLK_8, wind::Keycode::K_8},
    {SDLK_9, wind::Keycode::K_9},
    {SDLK_COLON, wind::Keycode::K_Colon},
    {SDLK_SEMICOLON, wind::Keycode::K_Semicolon},
    {SDLK_LESS, wind::Keycode::K_Less},
    {SDLK_EQUALS, wind::Keycode::K_Equals},
    {SDLK_GREATER, wind::Keycode::K_Greater},
    {SDLK_QUESTION, wind::Keycode::K_Question},
    {SDLK_AT, wind::Keycode::K_At},
    {SDLK_LEFTBRACKET, wind::Keycode::K_LeftBracket},
    {SDLK_BACKSLASH, wind::Keycode::K_Backslash},
    {SDLK_RIGHTBRACKET, wind::Keycode::K_RightBracket},
    {SDLK_CARET, wind::Keycode::K_Caret},
    {SDLK_UNDERSCORE, wind::Keycode::K_Underscore},
    {SDLK_BACKQUOTE, wind::Keycode::K_BackQuote},
    {SDLK_a, wind::Keycode::K_A},
    {SDLK_b, wind::Keycode::K_B},
    {SDLK_c, wind::Keycode::K_C},
    {SDLK_d, wind::Keycode::K_D},
    {SDLK_e, wind::Keycode::K_E},
    {SDLK_f, wind::Keycode::K_F},
    {SDLK_g, wind::Keycode::K_G},
    {SDLK_h, wind::Keycode::K_H},
    {SDLK_i, wind::Keycode::K_I},
    {SDLK_j, wind::Keycode::K_J},
    {SDLK_k, wind::Keycode::K_K},
    {SDLK_l, wind::Keycode::K_L},
    {SDLK_m, wind::Keycode::K_M},
    {SDLK_n, wind::Keycode::K_N},
    {SDLK_o, wind::Keycode::K_O},
    {SDLK_p, wind::Keycode::K_P},
    {SDLK_q, wind::Keycode::K_Q},
    {SDLK_r, wind::Keycode::K_R},
    {SDLK_s, wind::Keycode::K_S},
    {SDLK_t, wind::Keycode::K_T},
    {SDLK_u, wind::Keycode::K_U},
    {SDLK_v, wind::Keycode::K_V},
    {SDLK_w, wind::Keycode::K_W},
    {SDLK_x, wind::Keycode::K_X},
    {SDLK_y, wind::Keycode::K_Y},
    {SDLK_z, wind::Keycode::K_Z},
    {SDLK_CAPSLOCK, wind::Keycode::K_CapsLock},
    {SDLK_F1, wind::Keycode::K_F1},
    {SDLK_F2, wind::Keycode::K_F2},
    {SDLK_F3, wind::Keycode::K_F3},
    {SDLK_F4, wind::Keycode::K_F4},
    {SDLK_F5, wind::Keycode::K_F5},
    {SDLK_F6, wind::Keycode::K_F6},
    {SDLK_F7, wind::Keycode::K_F7},
    {SDLK_F8, wind::Keycode::K_F8},
    {SDLK_F9, wind::Keycode::K_F9},
    {SDLK_F10, wind::Keycode::K_F10},
    {SDLK_F11, wind::Keycode::K_F11},
    {SDLK_F12, wind::Keycode::K_F12},
    {SDLK_PRINTSCREEN, wind::Keycode::K_PrintScreen},
    {SDLK_SCROLLLOCK, wind::Keycode::K_ScrollLock},
    {SDLK_PAUSE, wind::Keycode::K_Pause},
    {SDLK_INSERT, wind::Keycode::K_Insert},
    {SDLK_HOME, wind::Keycode::K_Home},
    {SDLK_PAGEUP, wind::Keycode::K_PageUp},
    {SDLK_DELETE, wind::Keycode::K_Delete},
    {SDLK_END, wind::Keycode::K_End},
    {SDLK_PAGEDOWN, wind::Keycode::K_PageDown},
    {SDLK_RIGHT, wind::Keycode::K_Right},
    {SDLK_LEFT, wind::Keycode::K_Left},
    {SDLK_DOWN, wind::Keycode::K_Down},
    {SDLK_UP, wind::Keycode::K_Up},
    {SDLK_NUMLOCKCLEAR, wind::Keycode::K_NumLockClear},
    {SDLK_KP_DIVIDE, wind::Keycode::K_KpDivide},
    {SDLK_KP_MULTIPLY, wind::Keycode::K_KpMultiply},
    {SDLK_KP_MINUS, wind::Keycode::K_KpMinus},
    {SDLK_KP_PLUS, wind::Keycode::K_KpPlus},
    {SDLK_KP_ENTER, wind::Keycode::K_KpEnter},
    {SDLK_KP_1, wind::Keycode::K_Kp1},
    {SDLK_KP_2, wind::Keycode::K_Kp2},
    {SDLK_KP_3, wind::Keycode::K_Kp3},
    {SDLK_KP_4, wind::Keycode::K_Kp4},
    {SDLK_KP_5, wind::Keycode::K_Kp5},
    {SDLK_KP_6, wind::Keycode::K_Kp6},
    {SDLK_KP_7, wind::Keycode::K_Kp7},
    {SDLK_KP_8, wind::Keycode::K_Kp8},
    {SDLK_KP_9, wind::Keycode::K_Kp9},
    {SDLK_KP_0, wind::Keycode::K_Kp0},
    {SDLK_KP_PERIOD, wind::Keycode::K_KpPeriod},
    {SDLK_APPLICATION, wind::Keycode::K_Application},
    {SDLK_POWER, wind::Keycode::K_Power},
    {SDLK_KP_EQUALS, wind::Keycode::K_KpEquals},
    {SDLK_F13, wind::Keycode::K_F13},
    {SDLK_F14, wind::Keycode::K_F14},
    {SDLK_F15, wind::Keycode::K_F15},
    {SDLK_F16, wind::Keycode::K_F16},
    {SDLK_F17, wind::Keycode::K_F17},
    {SDLK_F18, wind::Keycode::K_F18},
    {SDLK_F19, wind::Keycode::K_F19},
    {SDLK_F20, wind::Keycode::K_F20},
    {SDLK_F21, wind::Keycode::K_F21},
    {SDLK_F22, wind::Keycode::K_F22},
    {SDLK_F23, wind::Keycode::K_F23},
    {SDLK_F24, wind::Keycode::K_F24},
    {SDLK_EXECUTE, wind::Keycode::K_Execute},
    {SDLK_HELP, wind::Keycode::K_Help},
    {SDLK_MENU, wind::Keycode::K_Menu},
    {SDLK_SELECT, wind::Keycode::K_Select},
    {SDLK_STOP, wind::Keycode::K_Stop},
    {SDLK_AGAIN, wind::Keycode::K_Again},
    {SDLK_UNDO, wind::Keycode::K_Undo},
    {SDLK_CUT, wind::Keycode::K_Cut},
    {SDLK_COPY, wind::Keycode::K_Copy},
    {SDLK_PASTE, wind::Keycode::K_Paste},
    {SDLK_FIND, wind::Keycode::K_Find},
    {SDLK_MUTE, wind::Keycode::K_Mute},
    {SDLK_VOLUMEUP, wind::Keycode::K_VolumeUp},
    {SDLK_VOLUMEDOWN, wind::Keycode::K_VolumeDown},
    {SDLK_KP_COMMA, wind::Keycode::K_KpComma},
    {SDLK_KP_EQUALSAS400, wind::Keycode::K_KpEqualsAs400},
    {SDLK_ALTERASE, wind::Keycode::K_AltErase},
    {SDLK_SYSREQ, wind::Keycode::K_SysReq},
    {SDLK_CANCEL, wind::Keycode::K_Cancel},
    {SDLK_CLEAR, wind::Keycode::K_Clear},
    {SDLK_PRIOR, wind::Keycode::K_Prior},
    {SDLK_RETURN2, wind::Keycode::K_Return2},
    {SDLK_SEPARATOR, wind::Keycode::K_Separator},
    {SDLK_OUT, wind::Keycode::K_Out},
    {SDLK_OPER, wind::Keycode::K_Oper},
    {SDLK_CLEARAGAIN, wind::Keycode::K_ClearAgain},
    {SDLK_CRSEL, wind::Keycode::K_Crsel},
    {SDLK_EXSEL, wind::Keycode::K_Exsel},
    {SDLK_KP_00, wind::Keycode::K_Kp00},
    {SDLK_KP_000, wind::Keycode::K_Kp000},
    {SDLK_THOUSANDSSEPARATOR, wind::Keycode::K_ThousandsSeparator},
    {SDLK_DECIMALSEPARATOR, wind::Keycode::K_DecimalSeparator},
    {SDLK_CURRENCYUNIT, wind::Keycode::K_CurrencyUnit},
    {SDLK_CURRENCYSUBUNIT, wind::Keycode::K_CurrencySubUnit},
    {SDLK_KP_LEFTPAREN, wind::Keycode::K_KpLeftParen},
    {SDLK_KP_RIGHTPAREN, wind::Keycode::K_KpRightParen},
    {SDLK_KP_LEFTBRACE, wind::Keycode::K_KpLeftBrace},
    {SDLK_KP_RIGHTBRACE, wind::Keycode::K_KpRightBrace},
    {SDLK_KP_TAB, wind::Keycode::K_KpTab},
    {SDLK_KP_BACKSPACE, wind::Keycode::K_KpBackspace},
    {SDLK_KP_A, wind::Keycode::K_KpA},
    {SDLK_KP_B, wind::Keycode::K_KpB},
    {SDLK_KP_C, wind::Keycode::K_KpC},
    {SDLK_KP_D, wind::Keycode::K_KpD},
    {SDLK_KP_E, wind::Keycode::K_KpE},
    {SDLK_KP_F, wind::Keycode::K_KpF},
    {SDLK_KP_XOR, wind::Keycode::K_KpXor},
    {SDLK_KP_POWER, wind::Keycode::K_KpPower},
    {SDLK_KP_PERCENT, wind::Keycode::K_KpPercent},
    {SDLK_KP_LESS, wind::Keycode::K_KpLess},
    {SDLK_KP_GREATER, wind::Keycode::K_KpGreater},
    {SDLK_KP_AMPERSAND, wind::Keycode::K_KpAmpersand},
    {SDLK_KP_DBLAMPERSAND, wind::Keycode::K_KpDblAmpersand},
    {SDLK_KP_VERTICALBAR, wind::Keycode::K_KpVerticalBar},
    {SDLK_KP_DBLVERTICALBAR, wind::Keycode::K_KpDblVerticalBar},
    {SDLK_KP_COLON, wind::Keycode::K_KpColon},
    {SDLK_KP_HASH, wind::Keycode::K_KpHash},
    {SDLK_KP_SPACE, wind::Keycode::K_KpSpace},
    {SDLK_KP_AT, wind::Keycode::K_KpAt},
    {SDLK_KP_EXCLAM, wind::Keycode::K_KpExclam},
    {SDLK_KP_MEMSTORE, wind::Keycode::K_KpMemStore},
    {SDLK_KP_MEMRECALL, wind::Keycode::K_KpMemRecall},
    {SDLK_KP_MEMCLEAR, wind::Keycode::K_KpMemClear},
    {SDLK_KP_MEMADD, wind::Keycode::K_KpMemAdd},
    {SDLK_KP_MEMSUBTRACT, wind::Keycode::K_KpMemSubtract},
    {SDLK_KP_MEMMULTIPLY, wind::Keycode::K_KpMemMultiply},
    {SDLK_KP_MEMDIVIDE, wind::Keycode::K_KpMemDivide},
    {SDLK_KP_PLUSMINUS, wind::Keycode::K_KpPlusMinus},
    {SDLK_KP_CLEAR, wind::Keycode::K_KpClear},
    {SDLK_KP_CLEARENTRY, wind::Keycode::K_KpClearEntry},
    {SDLK_KP_BINARY, wind::Keycode::K_KpBinary},
    {SDLK_KP_OCTAL, wind::Keycode::K_KpOctal},
    {SDLK_KP_DECIMAL, wind::Keycode::K_KpDecimal},
    {SDLK_KP_HEXADECIMAL, wind::Keycode::K_KpHexadecimal},
    {SDLK_LCTRL, wind::Keycode::K_LeftCtrl},
    {SDLK_LSHIFT, wind::Keycode::K_LeftShift},
    {SDLK_LALT, wind::Keycode::K_LeftAlt},
    {SDLK_LGUI, wind::Keycode::K_LeftGui},
    {SDLK_RCTRL, wind::Keycode::K_RightCtrl},
    {SDLK_RSHIFT, wind::Keycode::K_RightShift},
    {SDLK_RALT, wind::Keycode::K_RightAlt},
    {SDLK_RGUI, wind::Keycode::K_RightGui},
    {SDLK_MODE, wind::Keycode::K_Mode},
    {SDLK_AUDIONEXT, wind::Keycode::K_AudioNext},
    {SDLK_AUDIOPREV, wind::Keycode::K_AudioPrev},
    {SDLK_AUDIOSTOP, wind::Keycode::K_AudioStop},
    {SDLK_AUDIOPLAY, wind::Keycode::K_AudioPlay},
    {SDLK_AUDIOMUTE, wind::Keycode::K_AudioMute},
    {SDLK_MEDIASELECT, wind::Keycode::K_MediaSelect},
    {SDLK_WWW, wind::Keycode::K_Www},
    {SDLK_MAIL, wind::Keycode::K_Mail},
    {SDLK_CALCULATOR, wind::Keycode::K_Calculator},
    {SDLK_COMPUTER, wind::Keycode::K_Computer},
    {SDLK_AC_SEARCH, wind::Keycode::K_AcSearch},
    {SDLK_AC_HOME, wind::Keycode::K_AcHome},
    {SDLK_AC_BACK, wind::Keycode::K_AcBack},
    {SDLK_AC_FORWARD, wind::Keycode::K_AcForward},
    {SDLK_AC_STOP, wind::Keycode::K_AcStop},
    {SDLK_AC_REFRESH, wind::Keycode::K_AcRefresh},
    {SDLK_AC_BOOKMARKS, wind::Keycode::K_AcBookmarks},
    {SDLK_BRIGHTNESSDOWN, wind::Keycode::K_BrightnessDown},
    {SDLK_BRIGHTNESSUP, wind::Keycode::K_BrightnessUp},
    {SDLK_DISPLAYSWITCH, wind::Keycode::K_DisplaySwitch},
    {SDLK_KBDILLUMTOGGLE, wind::Keycode::K_KbdillumToggle},
    {SDLK_KBDILLUMDOWN, wind::Keycode::K_KbdillumDown},
    {SDLK_KBDILLUMUP, wind::Keycode::K_KbdillumuUp},
    {SDLK_EJECT, wind::Keycode::K_Eject},
    {SDLK_SLEEP, wind::Keycode::K_Sleep},
  };

  if (sdlKeyToKeycode.contains(key))
    return sdlKeyToKeycode[key];
  return wind::Keycode::Unknown;
}