namespace Wind {
  public partial class InputSystem {
    void addGroupedTrigger(string groupName, Keys bindings) {
      addGroupedTrigger(groupName, bindings, new(delegate { }));
      // if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
      //   trigger.Bindings.UnionWith(bindings);
      // } else {
      //   groupedTriggers.Add(groupName, new Trigger(groupName, bindings));
      // }
    }

    void addGroupedTrigger(string groupName, Key binding) {
      addGroupedTrigger(groupName, binding, new(delegate { }));
      // if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
      //   trigger.Bindings.Add(binding);
      // } else {
      //   groupedTriggers.Add(groupName, new Trigger(groupName, CreateKeys(binding)));
      // }
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
      // if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
      //   trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
      //   trigger.Bindings.Add(binding);
      // } else {
      //   groupedTriggers.Add(groupName, new Trigger(groupName, CreateKeys(binding), callbacks));
      // }
    }

    void addGroupedTrigger(string groupName) {
      addGroupedTrigger(groupName, CreateKeys(), new(delegate { }));
      // if (!groupedTriggers.ContainsKey(groupName)) {
      //   groupedTriggers.Add(groupName, new Trigger(groupName, CreateKeys(), new Callbacks(delegate { })));
      // }
    }

    //

    void addGroupedTriggerBindings(string groupName, Key binding) {
      addGroupedTriggerBindings(groupName, CreateKeys(binding));
      // if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
      //   trigger.Bindings.Add(binding);
      // }
    }

    void addGroupedTriggerBindings(string groupName, Keys bindings) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        trigger.Bindings.UnionWith(bindings);
      }
    }

    //

    void addGroupedTriggerCallbacks(string groupName, Callbacks callbacks) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        trigger.Callbacks = (Callbacks)Delegate.Combine(trigger.Callbacks, callbacks);
      }
    }
  };
}
