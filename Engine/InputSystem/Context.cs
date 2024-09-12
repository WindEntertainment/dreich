using static SDL2.SDL;

namespace Wind {
  public partial class InputSystem {
    public struct MouseContext {
      double cursorX;
      double cursorY;
      double scrollX;
      double scrollY;

      //   void addPressedButton(KEYCODES keycode)
      //   {
      //     pressedButtons.insert(keycode);
      //   };
      //   void addHeldButton(KEYCODES keycode)
      //   {
      //     heldButtons.insert(keycode);
      //   };
      //   void addReleasedButton(KEYCODES keycode)
      //   {
      //     releasedButtons.insert(keycode);
      //   };

      //   void removePressedButton(KEYCODES keycode)
      //   {
      //     pressedButtons.erase(keycode);
      //   };
      //   void removeHeldButton(KEYCODES keycode)
      //   {
      //     heldButtons.erase(keycode);
      //   };
      //   void removeReleasedButton(KEYCODES keycode)
      //   {
      //     releasedButtons.erase(keycode);
      //   };

      public void moveCursor(double nextX, double nextY) {
        cursorX = nextX;
        cursorY = nextY;
      }

      public void moveScroll(double nextX, double nextY) {
        scrollX = nextX;
        scrollY = nextY;
      }
    };

    public struct KeyboardContext {
      HashSet<Keycode> pressedKeys;
      HashSet<Keycode> heldKeys;
      HashSet<Keycode> releasedKeys;
      public uint codepoint;

      public void setCodepoint(uint newCodepoint) {
        codepoint = newCodepoint;
      }
      public void addPressedKey(Keycode keycode) {
        pressedKeys.Add(keycode);
      }
      public void addHeldKey(Keycode keycode) {
        heldKeys.Add(keycode);
      }
      public void addReleasedKey(Keycode keycode) {
        releasedKeys.Add(keycode);
      }

      public void removeCodepoint() {
        codepoint = 0;
      }
      public void removePressedKey(Keycode keycode) {
        pressedKeys.Remove(keycode);
      }
      public void removeHeldKey(Keycode keycode) {
        heldKeys.Remove(keycode);
      }
      public void removeReleasedKey(Keycode keycode) {
        releasedKeys.Remove(keycode);
      }
    };

    public struct InputSystemContext {
      public KeyboardContext KeyboardContext { get; set; }
      public MouseContext MouseContext { get; set; }
    };
  };
}
