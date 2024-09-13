using static SDL2.SDL;

namespace Wind {
  public partial class InputSystem {
    public struct MouseContext {
      public double cursorX { get; private set; }
      public double cursorY { get; private set; }
      public double scrollX { get; private set; }
      public double scrollY { get; private set; }

      HashSet<Keycode> pressedButtons;
      HashSet<Keycode> heldButtons;
      HashSet<Keycode> releasedButtons;

      public void addPressedButton(Keycode keycode) {
        pressedButtons.Add(keycode);
      }
      public void addHeldButton(Keycode keycode) {
        heldButtons.Add(keycode);
      }
      public void addReleasedButton(Keycode keycode) {
        releasedButtons.Add(keycode);
      }

      public void removePressedButton(Keycode keycode) {
        pressedButtons.Remove(keycode);
      }
      public void removeHeldButton(Keycode keycode) {
        heldButtons.Remove(keycode);
      }
      public void removeReleasedButton(Keycode keycode) {
        releasedButtons.Remove(keycode);
      }

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
