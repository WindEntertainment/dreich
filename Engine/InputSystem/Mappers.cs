
using static SDL2.SDL;


namespace Wind {
  public partial class InputSystem {
    public KeyAction mapStringToKeyAction(string value) {
      return EnumExtensions.ParseOrDefault(value, KeyAction.Unknown);
    }

    Keycode mapStringToKeycode(string value) {
      return EnumExtensions.ParseOrDefault(value, Keycode.Unknown);
    }

    Dictionary<SDL_EventType, KeyAction> sdlActionToKeyAction = new Dictionary<SDL_EventType, KeyAction>() {
      [SDL_EventType.SDL_KEYUP] = KeyAction.Released,
      [SDL_EventType.SDL_KEYDOWN] = KeyAction.Pressed,
    };

    KeyAction mapGlfwActionToKeyAction(SDL_EventType eventType) {
      return sdlActionToKeyAction.TryGetValue(eventType, out KeyAction keyAction) ? keyAction : KeyAction.Unknown;
    }

    Dictionary<uint, Keycode> sdlMouseCodeToKeycode = new Dictionary<uint, Keycode>() {
      [SDL_BUTTON_LEFT] = Keycode.MOUSE_BUTTON_LEFT,
      [SDL_BUTTON_MIDDLE] = Keycode.MOUSE_BUTTON_MIDDLE,
      [SDL_BUTTON_RIGHT] = Keycode.MOUSE_BUTTON_RIGHT,
    };

    Key mapGlfwMouseCodeToKey(uint key, SDL_EventType action) {
      Keycode mouseKeycode = sdlMouseCodeToKeycode.TryGetValue(key, out Keycode keyAction) ? keyAction : Keycode.Unknown;
      return new Key(mouseKeycode, mapGlfwActionToKeyAction(action));
    }

    // Key mapGlfwJoystickCodeToKey(int glfwKey, int action)
    // {
    //   switch (glfwKey)
    //   {
    //     case GLFW_JOYSTICK_1:
    //       return Key{ KEYCODES::JOYSTICK_1, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_2:
    //       return Key{ KEYCODES::JOYSTICK_2, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_3:
    //       return Key{ KEYCODES::JOYSTICK_3, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_4:
    //       return Key{ KEYCODES::JOYSTICK_4, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_5:
    //       return Key{ KEYCODES::JOYSTICK_5, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_6:
    //       return Key{ KEYCODES::JOYSTICK_6, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_7:
    //       return Key{ KEYCODES::JOYSTICK_7, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_8:
    //       return Key{ KEYCODES::JOYSTICK_8, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_9:
    //       return Key{ KEYCODES::JOYSTICK_9, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_10:
    //       return Key{ KEYCODES::JOYSTICK_10, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_11:
    //       return Key{ KEYCODES::JOYSTICK_11, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_12:
    //       return Key{ KEYCODES::JOYSTICK_12, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_13:
    //       return Key{ KEYCODES::JOYSTICK_13, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_14:
    //       return Key{ KEYCODES::JOYSTICK_14, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_15:
    //       return Key{ KEYCODES::JOYSTICK_15, mapGlfwActionToKeyAction(action)};
    //     case GLFW_JOYSTICK_16:
    //       return Key{ KEYCODES::JOYSTICK_16, mapGlfwActionToKeyAction(action)};
    //     default:
    //       return Key{ KEYCODES::KEY_UNKNOWN, mapGlfwActionToKeyAction(action)};
    //   }
    // }

    // Key mapGlfwGamepadButtonCodeToKey(int glfwKey, int action)
    // {
    //   switch (glfwKey)
    //   {
    //     case GLFW_GAMEPAD_BUTTON_A:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_A, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_B:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_B, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_X:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_X, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_Y:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_Y, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_LEFT_BUMPER:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_LEFT_BUMPER, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_RIGHT_BUMPER:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_RIGHT_BUMPER, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_BACK:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_BACK, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_START:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_START, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_GUIDE:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_GUIDE, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_LEFT_THUMB:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_LEFT_THUMB, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_RIGHT_THUMB:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_RIGHT_THUMB, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_DPAD_UP:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_DPAD_UP, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_DPAD_RIGHT:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_DPAD_RIGHT, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_DPAD_DOWN:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_DPAD_DOWN, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_BUTTON_DPAD_LEFT:
    //       return Key{ KEYCODES::GAMEPAD_BUTTON_DPAD_LEFT, mapGlfwActionToKeyAction(action)};
    //     default:
    //       return Key{ KEYCODES::KEY_UNKNOWN, mapGlfwActionToKeyAction(action)};
    //   }
    // }

    // Key mapGlfwGamepadAxisCodeToKey(int glfwKey, int action)
    // {
    //   switch (glfwKey)
    //   {
    //     case GLFW_GAMEPAD_AXIS_LEFT_X:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_LEFT_X, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_AXIS_LEFT_Y:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_LEFT_Y, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_AXIS_RIGHT_X:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_RIGHT_X, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_AXIS_RIGHT_Y:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_RIGHT_Y, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_AXIS_LEFT_TRIGGER:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_LEFT_TRIGGER, mapGlfwActionToKeyAction(action)};
    //     case GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER:
    //       return Key{ KEYCODES::GAMEPAD_AXIS_RIGHT_TRIGGER, mapGlfwActionToKeyAction(action)};
    //     default:
    //       return Key{ KEYCODES::KEY_UNKNOWN, mapGlfwActionToKeyAction(action)};
    //   }
    // }

    Dictionary<SDL_Keycode, Keycode> sdlKeyToKeycode = new Dictionary<SDL_Keycode, Keycode>() {
      [SDL_Keycode.SDLK_UNKNOWN] = Keycode.Unknown,
      [SDL_Keycode.SDLK_RETURN] = Keycode.K_Return,
      [SDL_Keycode.SDLK_ESCAPE] = Keycode.K_Escape,
      [SDL_Keycode.SDLK_BACKSPACE] = Keycode.K_Backspace,
      [SDL_Keycode.SDLK_TAB] = Keycode.K_Tab,
      [SDL_Keycode.SDLK_SPACE] = Keycode.K_Space,
      [SDL_Keycode.SDLK_EXCLAIM] = Keycode.K_Exclaim,
      [SDL_Keycode.SDLK_QUOTEDBL] = Keycode.K_QuoteDbl,
      [SDL_Keycode.SDLK_HASH] = Keycode.K_Hash,
      [SDL_Keycode.SDLK_PERCENT] = Keycode.K_Percent,
      [SDL_Keycode.SDLK_DOLLAR] = Keycode.K_Dollar,
      [SDL_Keycode.SDLK_AMPERSAND] = Keycode.K_Ampersand,
      [SDL_Keycode.SDLK_QUOTE] = Keycode.K_Quote,
      [SDL_Keycode.SDLK_LEFTPAREN] = Keycode.K_LeftParen,
      [SDL_Keycode.SDLK_RIGHTPAREN] = Keycode.K_RightParen,
      [SDL_Keycode.SDLK_ASTERISK] = Keycode.K_Asterisk,
      [SDL_Keycode.SDLK_PLUS] = Keycode.K_Plus,
      [SDL_Keycode.SDLK_COMMA] = Keycode.K_Comma,
      [SDL_Keycode.SDLK_MINUS] = Keycode.K_Minus,
      [SDL_Keycode.SDLK_PERIOD] = Keycode.K_Period,
      [SDL_Keycode.SDLK_SLASH] = Keycode.K_Slash,
      [SDL_Keycode.SDLK_0] = Keycode.K_0,
      [SDL_Keycode.SDLK_1] = Keycode.K_1,
      [SDL_Keycode.SDLK_2] = Keycode.K_2,
      [SDL_Keycode.SDLK_3] = Keycode.K_3,
      [SDL_Keycode.SDLK_4] = Keycode.K_4,
      [SDL_Keycode.SDLK_5] = Keycode.K_5,
      [SDL_Keycode.SDLK_6] = Keycode.K_6,
      [SDL_Keycode.SDLK_7] = Keycode.K_7,
      [SDL_Keycode.SDLK_8] = Keycode.K_8,
      [SDL_Keycode.SDLK_9] = Keycode.K_9,
      [SDL_Keycode.SDLK_COLON] = Keycode.K_Colon,
      [SDL_Keycode.SDLK_SEMICOLON] = Keycode.K_Semicolon,
      [SDL_Keycode.SDLK_LESS] = Keycode.K_Less,
      [SDL_Keycode.SDLK_EQUALS] = Keycode.K_Equals,
      [SDL_Keycode.SDLK_GREATER] = Keycode.K_Greater,
      [SDL_Keycode.SDLK_QUESTION] = Keycode.K_Question,
      [SDL_Keycode.SDLK_AT] = Keycode.K_At,
      [SDL_Keycode.SDLK_LEFTBRACKET] = Keycode.K_LeftBracket,
      [SDL_Keycode.SDLK_BACKSLASH] = Keycode.K_Backslash,
      [SDL_Keycode.SDLK_RIGHTBRACKET] = Keycode.K_RightBracket,
      [SDL_Keycode.SDLK_CARET] = Keycode.K_Caret,
      [SDL_Keycode.SDLK_UNDERSCORE] = Keycode.K_Underscore,
      [SDL_Keycode.SDLK_BACKQUOTE] = Keycode.K_BackQuote,
      [SDL_Keycode.SDLK_a] = Keycode.K_A,
      [SDL_Keycode.SDLK_b] = Keycode.K_B,
      [SDL_Keycode.SDLK_c] = Keycode.K_C,
      [SDL_Keycode.SDLK_d] = Keycode.K_D,
      [SDL_Keycode.SDLK_e] = Keycode.K_E,
      [SDL_Keycode.SDLK_f] = Keycode.K_F,
      [SDL_Keycode.SDLK_g] = Keycode.K_G,
      [SDL_Keycode.SDLK_h] = Keycode.K_H,
      [SDL_Keycode.SDLK_i] = Keycode.K_I,
      [SDL_Keycode.SDLK_j] = Keycode.K_J,
      [SDL_Keycode.SDLK_k] = Keycode.K_K,
      [SDL_Keycode.SDLK_l] = Keycode.K_L,
      [SDL_Keycode.SDLK_m] = Keycode.K_M,
      [SDL_Keycode.SDLK_n] = Keycode.K_N,
      [SDL_Keycode.SDLK_o] = Keycode.K_O,
      [SDL_Keycode.SDLK_p] = Keycode.K_P,
      [SDL_Keycode.SDLK_q] = Keycode.K_Q,
      [SDL_Keycode.SDLK_r] = Keycode.K_R,
      [SDL_Keycode.SDLK_s] = Keycode.K_S,
      [SDL_Keycode.SDLK_t] = Keycode.K_T,
      [SDL_Keycode.SDLK_u] = Keycode.K_U,
      [SDL_Keycode.SDLK_v] = Keycode.K_V,
      [SDL_Keycode.SDLK_w] = Keycode.K_W,
      [SDL_Keycode.SDLK_x] = Keycode.K_X,
      [SDL_Keycode.SDLK_y] = Keycode.K_Y,
      [SDL_Keycode.SDLK_z] = Keycode.K_Z,
      [SDL_Keycode.SDLK_CAPSLOCK] = Keycode.K_CapsLock,
      [SDL_Keycode.SDLK_F1] = Keycode.K_F1,
      [SDL_Keycode.SDLK_F2] = Keycode.K_F2,
      [SDL_Keycode.SDLK_F3] = Keycode.K_F3,
      [SDL_Keycode.SDLK_F4] = Keycode.K_F4,
      [SDL_Keycode.SDLK_F5] = Keycode.K_F5,
      [SDL_Keycode.SDLK_F6] = Keycode.K_F6,
      [SDL_Keycode.SDLK_F7] = Keycode.K_F7,
      [SDL_Keycode.SDLK_F8] = Keycode.K_F8,
      [SDL_Keycode.SDLK_F9] = Keycode.K_F9,
      [SDL_Keycode.SDLK_F10] = Keycode.K_F10,
      [SDL_Keycode.SDLK_F11] = Keycode.K_F11,
      [SDL_Keycode.SDLK_F12] = Keycode.K_F12,
      [SDL_Keycode.SDLK_PRINTSCREEN] = Keycode.K_PrintScreen,
      [SDL_Keycode.SDLK_SCROLLLOCK] = Keycode.K_ScrollLock,
      [SDL_Keycode.SDLK_PAUSE] = Keycode.K_Pause,
      [SDL_Keycode.SDLK_INSERT] = Keycode.K_Insert,
      [SDL_Keycode.SDLK_HOME] = Keycode.K_Home,
      [SDL_Keycode.SDLK_PAGEUP] = Keycode.K_PageUp,
      [SDL_Keycode.SDLK_DELETE] = Keycode.K_Delete,
      [SDL_Keycode.SDLK_END] = Keycode.K_End,
      [SDL_Keycode.SDLK_PAGEDOWN] = Keycode.K_PageDown,
      [SDL_Keycode.SDLK_RIGHT] = Keycode.K_Right,
      [SDL_Keycode.SDLK_LEFT] = Keycode.K_Left,
      [SDL_Keycode.SDLK_DOWN] = Keycode.K_Down,
      [SDL_Keycode.SDLK_UP] = Keycode.K_Up,
      [SDL_Keycode.SDLK_NUMLOCKCLEAR] = Keycode.K_NumLockClear,
      [SDL_Keycode.SDLK_KP_DIVIDE] = Keycode.K_KpDivide,
      [SDL_Keycode.SDLK_KP_MULTIPLY] = Keycode.K_KpMultiply,
      [SDL_Keycode.SDLK_KP_MINUS] = Keycode.K_KpMinus,
      [SDL_Keycode.SDLK_KP_PLUS] = Keycode.K_KpPlus,
      [SDL_Keycode.SDLK_KP_ENTER] = Keycode.K_KpEnter,
      [SDL_Keycode.SDLK_KP_1] = Keycode.K_Kp1,
      [SDL_Keycode.SDLK_KP_2] = Keycode.K_Kp2,
      [SDL_Keycode.SDLK_KP_3] = Keycode.K_Kp3,
      [SDL_Keycode.SDLK_KP_4] = Keycode.K_Kp4,
      [SDL_Keycode.SDLK_KP_5] = Keycode.K_Kp5,
      [SDL_Keycode.SDLK_KP_6] = Keycode.K_Kp6,
      [SDL_Keycode.SDLK_KP_7] = Keycode.K_Kp7,
      [SDL_Keycode.SDLK_KP_8] = Keycode.K_Kp8,
      [SDL_Keycode.SDLK_KP_9] = Keycode.K_Kp9,
      [SDL_Keycode.SDLK_KP_0] = Keycode.K_Kp0,
      [SDL_Keycode.SDLK_KP_PERIOD] = Keycode.K_KpPeriod,
      [SDL_Keycode.SDLK_APPLICATION] = Keycode.K_Application,
      [SDL_Keycode.SDLK_POWER] = Keycode.K_Power,
      [SDL_Keycode.SDLK_KP_EQUALS] = Keycode.K_KpEquals,
      [SDL_Keycode.SDLK_F13] = Keycode.K_F13,
      [SDL_Keycode.SDLK_F14] = Keycode.K_F14,
      [SDL_Keycode.SDLK_F15] = Keycode.K_F15,
      [SDL_Keycode.SDLK_F16] = Keycode.K_F16,
      [SDL_Keycode.SDLK_F17] = Keycode.K_F17,
      [SDL_Keycode.SDLK_F18] = Keycode.K_F18,
      [SDL_Keycode.SDLK_F19] = Keycode.K_F19,
      [SDL_Keycode.SDLK_F20] = Keycode.K_F20,
      [SDL_Keycode.SDLK_F21] = Keycode.K_F21,
      [SDL_Keycode.SDLK_F22] = Keycode.K_F22,
      [SDL_Keycode.SDLK_F23] = Keycode.K_F23,
      [SDL_Keycode.SDLK_F24] = Keycode.K_F24,
      [SDL_Keycode.SDLK_EXECUTE] = Keycode.K_Execute,
      [SDL_Keycode.SDLK_HELP] = Keycode.K_Help,
      [SDL_Keycode.SDLK_MENU] = Keycode.K_Menu,
      [SDL_Keycode.SDLK_SELECT] = Keycode.K_Select,
      [SDL_Keycode.SDLK_STOP] = Keycode.K_Stop,
      [SDL_Keycode.SDLK_AGAIN] = Keycode.K_Again,
      [SDL_Keycode.SDLK_UNDO] = Keycode.K_Undo,
      [SDL_Keycode.SDLK_CUT] = Keycode.K_Cut,
      [SDL_Keycode.SDLK_COPY] = Keycode.K_Copy,
      [SDL_Keycode.SDLK_PASTE] = Keycode.K_Paste,
      [SDL_Keycode.SDLK_FIND] = Keycode.K_Find,
      [SDL_Keycode.SDLK_MUTE] = Keycode.K_Mute,
      [SDL_Keycode.SDLK_VOLUMEUP] = Keycode.K_VolumeUp,
      [SDL_Keycode.SDLK_VOLUMEDOWN] = Keycode.K_VolumeDown,
      [SDL_Keycode.SDLK_KP_COMMA] = Keycode.K_KpComma,
      [SDL_Keycode.SDLK_KP_EQUALSAS400] = Keycode.K_KpEqualsAs400,
      [SDL_Keycode.SDLK_ALTERASE] = Keycode.K_AltErase,
      [SDL_Keycode.SDLK_SYSREQ] = Keycode.K_SysReq,
      [SDL_Keycode.SDLK_CANCEL] = Keycode.K_Cancel,
      [SDL_Keycode.SDLK_CLEAR] = Keycode.K_Clear,
      [SDL_Keycode.SDLK_PRIOR] = Keycode.K_Prior,
      [SDL_Keycode.SDLK_RETURN2] = Keycode.K_Return2,
      [SDL_Keycode.SDLK_SEPARATOR] = Keycode.K_Separator,
      [SDL_Keycode.SDLK_OUT] = Keycode.K_Out,
      [SDL_Keycode.SDLK_OPER] = Keycode.K_Oper,
      [SDL_Keycode.SDLK_CLEARAGAIN] = Keycode.K_ClearAgain,
      [SDL_Keycode.SDLK_CRSEL] = Keycode.K_Crsel,
      [SDL_Keycode.SDLK_EXSEL] = Keycode.K_Exsel,
      [SDL_Keycode.SDLK_KP_00] = Keycode.K_Kp00,
      [SDL_Keycode.SDLK_KP_000] = Keycode.K_Kp000,
      [SDL_Keycode.SDLK_THOUSANDSSEPARATOR] = Keycode.K_ThousandsSeparator,
      [SDL_Keycode.SDLK_DECIMALSEPARATOR] = Keycode.K_DecimalSeparator,
      [SDL_Keycode.SDLK_CURRENCYUNIT] = Keycode.K_CurrencyUnit,
      [SDL_Keycode.SDLK_CURRENCYSUBUNIT] = Keycode.K_CurrencySubUnit,
      [SDL_Keycode.SDLK_KP_LEFTPAREN] = Keycode.K_KpLeftParen,
      [SDL_Keycode.SDLK_KP_RIGHTPAREN] = Keycode.K_KpRightParen,
      [SDL_Keycode.SDLK_KP_LEFTBRACE] = Keycode.K_KpLeftBrace,
      [SDL_Keycode.SDLK_KP_RIGHTBRACE] = Keycode.K_KpRightBrace,
      [SDL_Keycode.SDLK_KP_TAB] = Keycode.K_KpTab,
      [SDL_Keycode.SDLK_KP_BACKSPACE] = Keycode.K_KpBackspace,
      [SDL_Keycode.SDLK_KP_A] = Keycode.K_KpA,
      [SDL_Keycode.SDLK_KP_B] = Keycode.K_KpB,
      [SDL_Keycode.SDLK_KP_C] = Keycode.K_KpC,
      [SDL_Keycode.SDLK_KP_D] = Keycode.K_KpD,
      [SDL_Keycode.SDLK_KP_E] = Keycode.K_KpE,
      [SDL_Keycode.SDLK_KP_F] = Keycode.K_KpF,
      [SDL_Keycode.SDLK_KP_XOR] = Keycode.K_KpXor,
      [SDL_Keycode.SDLK_KP_POWER] = Keycode.K_KpPower,
      [SDL_Keycode.SDLK_KP_PERCENT] = Keycode.K_KpPercent,
      [SDL_Keycode.SDLK_KP_LESS] = Keycode.K_KpLess,
      [SDL_Keycode.SDLK_KP_GREATER] = Keycode.K_KpGreater,
      [SDL_Keycode.SDLK_KP_AMPERSAND] = Keycode.K_KpAmpersand,
      [SDL_Keycode.SDLK_KP_DBLAMPERSAND] = Keycode.K_KpDblAmpersand,
      [SDL_Keycode.SDLK_KP_VERTICALBAR] = Keycode.K_KpVerticalBar,
      [SDL_Keycode.SDLK_KP_DBLVERTICALBAR] = Keycode.K_KpDblVerticalBar,
      [SDL_Keycode.SDLK_KP_COLON] = Keycode.K_KpColon,
      [SDL_Keycode.SDLK_KP_HASH] = Keycode.K_KpHash,
      [SDL_Keycode.SDLK_KP_SPACE] = Keycode.K_KpSpace,
      [SDL_Keycode.SDLK_KP_AT] = Keycode.K_KpAt,
      [SDL_Keycode.SDLK_KP_EXCLAM] = Keycode.K_KpExclam,
      [SDL_Keycode.SDLK_KP_MEMSTORE] = Keycode.K_KpMemStore,
      [SDL_Keycode.SDLK_KP_MEMRECALL] = Keycode.K_KpMemRecall,
      [SDL_Keycode.SDLK_KP_MEMCLEAR] = Keycode.K_KpMemClear,
      [SDL_Keycode.SDLK_KP_MEMADD] = Keycode.K_KpMemAdd,
      [SDL_Keycode.SDLK_KP_MEMSUBTRACT] = Keycode.K_KpMemSubtract,
      [SDL_Keycode.SDLK_KP_MEMMULTIPLY] = Keycode.K_KpMemMultiply,
      [SDL_Keycode.SDLK_KP_MEMDIVIDE] = Keycode.K_KpMemDivide,
      [SDL_Keycode.SDLK_KP_PLUSMINUS] = Keycode.K_KpPlusMinus,
      [SDL_Keycode.SDLK_KP_CLEAR] = Keycode.K_KpClear,
      [SDL_Keycode.SDLK_KP_CLEARENTRY] = Keycode.K_KpClearEntry,
      [SDL_Keycode.SDLK_KP_BINARY] = Keycode.K_KpBinary,
      [SDL_Keycode.SDLK_KP_OCTAL] = Keycode.K_KpOctal,
      [SDL_Keycode.SDLK_KP_DECIMAL] = Keycode.K_KpDecimal,
      [SDL_Keycode.SDLK_KP_HEXADECIMAL] = Keycode.K_KpHexadecimal,
      [SDL_Keycode.SDLK_LCTRL] = Keycode.K_LeftCtrl,
      [SDL_Keycode.SDLK_LSHIFT] = Keycode.K_LeftShift,
      [SDL_Keycode.SDLK_LALT] = Keycode.K_LeftAlt,
      [SDL_Keycode.SDLK_LGUI] = Keycode.K_LeftGui,
      [SDL_Keycode.SDLK_RCTRL] = Keycode.K_RightCtrl,
      [SDL_Keycode.SDLK_RSHIFT] = Keycode.K_RightShift,
      [SDL_Keycode.SDLK_RALT] = Keycode.K_RightAlt,
      [SDL_Keycode.SDLK_RGUI] = Keycode.K_RightGui,
      [SDL_Keycode.SDLK_MODE] = Keycode.K_Mode,
      [SDL_Keycode.SDLK_AUDIONEXT] = Keycode.K_AudioNext,
      [SDL_Keycode.SDLK_AUDIOPREV] = Keycode.K_AudioPrev,
      [SDL_Keycode.SDLK_AUDIOSTOP] = Keycode.K_AudioStop,
      [SDL_Keycode.SDLK_AUDIOPLAY] = Keycode.K_AudioPlay,
      [SDL_Keycode.SDLK_AUDIOMUTE] = Keycode.K_AudioMute,
      [SDL_Keycode.SDLK_MEDIASELECT] = Keycode.K_MediaSelect,
      [SDL_Keycode.SDLK_WWW] = Keycode.K_Www,
      [SDL_Keycode.SDLK_MAIL] = Keycode.K_Mail,
      [SDL_Keycode.SDLK_CALCULATOR] = Keycode.K_Calculator,
      [SDL_Keycode.SDLK_COMPUTER] = Keycode.K_Computer,
      [SDL_Keycode.SDLK_AC_SEARCH] = Keycode.K_AcSearch,
      [SDL_Keycode.SDLK_AC_HOME] = Keycode.K_AcHome,
      [SDL_Keycode.SDLK_AC_BACK] = Keycode.K_AcBack,
      [SDL_Keycode.SDLK_AC_FORWARD] = Keycode.K_AcForward,
      [SDL_Keycode.SDLK_AC_STOP] = Keycode.K_AcStop,
      [SDL_Keycode.SDLK_AC_REFRESH] = Keycode.K_AcRefresh,
      [SDL_Keycode.SDLK_AC_BOOKMARKS] = Keycode.K_AcBookmarks,
      [SDL_Keycode.SDLK_BRIGHTNESSDOWN] = Keycode.K_BrightnessDown,
      [SDL_Keycode.SDLK_BRIGHTNESSUP] = Keycode.K_BrightnessUp,
      [SDL_Keycode.SDLK_DISPLAYSWITCH] = Keycode.K_DisplaySwitch,
      [SDL_Keycode.SDLK_KBDILLUMTOGGLE] = Keycode.K_KbdillumToggle,
      [SDL_Keycode.SDLK_KBDILLUMDOWN] = Keycode.K_KbdillumDown,
      [SDL_Keycode.SDLK_KBDILLUMUP] = Keycode.K_KbdillumuUp,
      [SDL_Keycode.SDLK_EJECT] = Keycode.K_Eject,
      [SDL_Keycode.SDLK_SLEEP] = Keycode.K_Sleep,
    };

    Key mapGlfwKeyboardCodeToKey(SDL_Keycode glfwKey, SDL_EventType action) {
      Keycode keycode = sdlKeyToKeycode.TryGetValue(glfwKey, out Keycode key) ? key : Keycode.Unknown;
      return new Key(keycode, mapGlfwActionToKeyAction(action));
    }
  };
}
