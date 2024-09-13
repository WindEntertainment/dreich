namespace Wind {
  public partial class InputSystem {
    void addGroupedTrigger(string groupName, Keys bindings) {
      addGroupedTrigger(groupName, bindings, new(delegate { }));
    }

    void addGroupedTrigger(string groupName, Key binding) {
      addGroupedTrigger(groupName, binding, new(delegate { }));
    }

    void addGroupedTrigger(string groupName, Keys bindings, Callbacks callbacks) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
        trigger.Bindings.UnionWith(bindings);
      } else {
        groupedTriggers.Add(groupName, new(groupName, bindings, callbacks));
      }
    }

    void addGroupedTrigger(string groupName, Key binding, Callbacks callbacks) {
      addGroupedTrigger(groupName, CreateKeys(binding), callbacks);
    }

    void addGroupedTrigger(string groupName) {
      addGroupedTrigger(groupName, CreateKeys(), new(delegate { }));
    }

    //

    void addGroupedTriggerBindings(string groupName, Key binding) {
      addGroupedTriggerBindings(groupName, CreateKeys(binding));
    }

    void addGroupedTriggerBindings(string groupName, Keys bindings) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger)) return;
      trigger.Bindings.UnionWith(bindings);
    }

    //

    void addGroupedTriggerCallbacks(string groupName, Callbacks callbacks) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger)) return;
      trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
    }
  };
}
