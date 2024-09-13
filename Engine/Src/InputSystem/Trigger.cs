global using Keys = System.Collections.Generic.HashSet<Wind.InputSystem.Key>;
global using Callbacks = System.Action<Wind.InputSystem.InputSystemContext>;

namespace Wind {
  public partial class InputSystem {
    public static Keys CreateKeys(params Key[] initialValues) => new(initialValues, new KeyEqualityComparer());

    public struct Trigger(string name, Keys bindings, Callbacks callbacks) {
      public string Name { get; set; } = name;
      public Keys Bindings { get; set; } = bindings;
      public Callbacks Callbacks { get; set; } = callbacks;

      public Trigger(string name, Keys bindings) : this(name, bindings, new(delegate { })) { }
      public Trigger(string name, Callbacks callbacks) : this(name, CreateKeys(), callbacks) { }
      public Trigger(string name) : this(name, CreateKeys(), new(delegate { })) { }
    };
  }
}
