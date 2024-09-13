namespace Wind {
  public partial class InputSystem {
    static Dictionary<Key, Callbacks?> keycodeTriggers = [];
    static SortedDictionary<string, Trigger> groupedTriggers = [];

    public void AddKeycodeTrigger(Keys bindings) {
      AddKeycodeTrigger(bindings, new(delegate { }));
    }

    public void AddKeycodeTrigger(Key binding) {
      AddKeycodeTrigger(CreateKeys(binding), new(delegate { }));
    }

    public void AddKeycodeTrigger(Keys bindings, Callbacks callbacks) {
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

    public void AddKeycodeTriggerBindings(string groupName, Key binding) {
      AddKeycodeTriggerBindings(groupName, CreateKeys(binding));
    }

    public void AddKeycodeTriggerBindings(string groupName, Keys bindings) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger))
        return;

      foreach (var binding in bindings) {
        addKeycodeTrigger(binding, trigger.Callbacks);
      };
    }

    //

    public void AddKeycodeTriggerCallbacks(string groupName, Callbacks callbacks) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger))
        return;

      foreach (var binding in trigger.Bindings) {
        if (!keycodeTriggers.ContainsKey(binding))
          return;

        keycodeTriggers[binding] = callbacks;
      };
    }

    //

    public void RemoveKeycodeTrigger(Callbacks callbacks) {
      foreach (var trigger in keycodeTriggers) {
        RemoveKeycodeTrigger(trigger.Key, callbacks);
      };
    }

    public void RemoveKeycodeTrigger(Key binding, Callbacks callbacks) {
      keycodeTriggers.TryGetValue(binding, out Callbacks? existingCallbacks);
      if (existingCallbacks == null) return;

      foreach (var callback in callbacks.GetInvocationList()) {
        existingCallbacks -= (Callbacks)callback;
      }

      keycodeTriggers[binding] = existingCallbacks;
    }
  };
}
