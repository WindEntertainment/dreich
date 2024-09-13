namespace Wind {
  public partial class InputSystem {
    static Dictionary<Key, Callbacks> keycodeTriggers = [];
    static SortedDictionary<string, Trigger> groupedTriggers = [];


    public void addKeycodeTrigger(Keys bindings) {
      addKeycodeTrigger(bindings, new(delegate { }));
      // foreach (var binding in bindings) {
      //   keycodeTriggers[binding] = new Callbacks(delegate { });
      // };
    }

    public void addKeycodeTrigger(Key binding) {
      addKeycodeTrigger(CreateKeys(binding), new(delegate { }));
      // keycodeTriggers[binding] = new Callbacks(delegate { });
    }

    public void addKeycodeTrigger(Keys bindings, Callbacks callbacks) {
      foreach (var binding in bindings) {
        addKeycodeTrigger(binding, callbacks);
      };
    }

    public void addKeycodeTrigger(Key binding, Callbacks callbacks) {
      if (keycodeTriggers.ContainsKey(binding)) {
        keycodeTriggers.Add(binding, callbacks);
      } else {
        keycodeTriggers[binding] = callbacks;
      };
    }

    //

    public void addKeycodeTriggerBindings(string groupName, Key binding) {
      addKeycodeTriggerBindings(groupName, CreateKeys(binding));
      // if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger))
      //   return;

      // addKeycodeTrigger(binding, trigger.Callbacks);
    }

    public void addKeycodeTriggerBindings(string groupName, Keys bindings) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger))
        return;

      foreach (var binding in bindings) {
        addKeycodeTrigger(binding, trigger.Callbacks);
      };
    }

    //

    public void addKeycodeTriggerCallbacks(string groupName, Callbacks callbacks) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger))
        return;

      foreach (var binding in trigger.Bindings) {
        if (!keycodeTriggers.ContainsKey(binding))
          return;

        keycodeTriggers[binding] = callbacks;
      };
    }

    //

    public void removeKeycodeTrigger(Callbacks callbacks) {
      foreach (var trigger in keycodeTriggers) {
        removeKeycodeTrigger(trigger.Key, callbacks);
      };
    }

    public void removeKeycodeTrigger(Key binding, Callbacks callbacks) {
      keycodeTriggers.TryGetValue(binding, out Callbacks existingCallbacks);
      if (existingCallbacks == null) return;

      foreach (var callback in callbacks.GetInvocationList()) {
        existingCallbacks -= (Callbacks)callback;
      }

      keycodeTriggers[binding] = existingCallbacks;
    }
  };
}
