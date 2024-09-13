
using static SDL2.SDL;


namespace Wind {
  public partial class InputSystem {

    public struct Key(Keycode keycode = Keycode.Unknown, KeyAction action = KeyAction.Unknown) {
      public Keycode keycode = keycode;
      public KeyAction action = action;

      public readonly bool Equals(Key key) {
        return key.keycode == keycode && action == key.action;
      }
    };

    class KeyEqualityComparer : IEqualityComparer<Key> {
      public bool Equals(Key key1, Key key2) {
        return key1.Equals(key2);
      }

      public int GetHashCode(Key key) {
        return key.GetHashCode();
      }
    }

  };
}
