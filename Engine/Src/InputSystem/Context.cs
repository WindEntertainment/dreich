using static SDL2.SDL;

namespace Wind {
  public partial class InputSystem {
    public struct MouseContext() {
      public double CursorX { get; private set; }
      public double CursorY { get; private set; }
      public double ScrollX { get; private set; }
      public double ScrollY { get; private set; }

      private readonly HashSet<Keycode> pressedButtons = [];
      private readonly HashSet<Keycode> heldButtons = [];
      private readonly HashSet<Keycode> releasedButtons = [];

      public void AddPressedButton(Keycode keycode) {
        pressedButtons.Add(keycode);
      }
      public void AddHeldButton(Keycode keycode) {
        heldButtons.Add(keycode);
      }
      public void AddReleasedButton(Keycode keycode) {
        releasedButtons.Add(keycode);
      }

      public void RemovePressedButton(Keycode keycode) {
        pressedButtons.Remove(keycode);
      }
      public void RemoveHeldButton(Keycode keycode) {
        heldButtons.Remove(keycode);
      }
      public void RemoveReleasedButton(Keycode keycode) {
        releasedButtons.Remove(keycode);
      }

      public void MoveCursor(double nextX, double nextY) {
        CursorX = nextX;
        CursorY = nextY;
      }

      public void MoveScroll(double nextX, double nextY) {
        ScrollX = nextX;
        ScrollY = nextY;
      }
    };

    public struct KeyboardContext() {
      private readonly HashSet<Keycode> pressedKeys = [];
      private readonly HashSet<Keycode> heldKeys = [];
      private readonly HashSet<Keycode> releasedKeys = [];
      private uint codepoint = 0;

      public void SetCodepoint(uint newCodepoint) {
        codepoint = newCodepoint;
      }
      public void AddPressedKey(Keycode keycode) {
        pressedKeys.Add(keycode);
      }
      public void AddHeldKey(Keycode keycode) {
        heldKeys.Add(keycode);
      }
      public void AddReleasedKey(Keycode keycode) {
        releasedKeys.Add(keycode);
      }

      public void RemoveCodepoint() {
        codepoint = 0;
      }
      public void RemovePressedKey(Keycode keycode) {
        pressedKeys.Remove(keycode);
      }
      public void RemoveHeldKey(Keycode keycode) {
        heldKeys.Remove(keycode);
      }
      public void RemoveReleasedKey(Keycode keycode) {
        releasedKeys.Remove(keycode);
      }
    };

    public struct InputSystemContext {
      public KeyboardContext KeyboardContext { get; set; }
      public MouseContext MouseContext { get; set; }
    };
  };
}