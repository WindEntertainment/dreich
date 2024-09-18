namespace Wind {
  public partial class InputSystem {
    void AddGroupedTrigger(string groupName, Keys bindings) {
      AddGroupedTrigger(groupName, bindings, new(delegate { }));
    }

    void AddGroupedTrigger(string groupName, Key binding) {
      AddGroupedTrigger(groupName, binding, new(delegate { }));
    }

    void AddGroupedTrigger(string groupName, Keys bindings, Callbacks callbacks) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
        trigger.Bindings.UnionWith(bindings);
      } else {
        groupedTriggers.Add(groupName, new(groupName, bindings, callbacks));
      }
    }

    void AddGroupedTrigger(string groupName, Key binding, Callbacks callbacks) {
      AddGroupedTrigger(groupName, CreateKeys(binding), callbacks);
    }

    void AddGroupedTrigger(string groupName) {
      AddGroupedTrigger(groupName, CreateKeys(), new(delegate { }));
    }

    //

    void AddGroupedTriggerBindings(string groupName, Key binding) {
      AddGroupedTriggerBindings(groupName, CreateKeys(binding));
    }

    void AddGroupedTriggerBindings(string groupName, Keys bindings) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger)) return;
      trigger.Bindings.UnionWith(bindings);
    }

    //

    void AddGroupedTriggerCallbacks(string groupName, Callbacks callbacks) {
      if (!groupedTriggers.TryGetValue(groupName, out Trigger trigger)) return;
      trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
    }
  };
}