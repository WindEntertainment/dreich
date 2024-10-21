using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

using static SDL2.SDL;

namespace Wind {
  public partial class InputSystem() {

    InputSystemContext context = new();

    // active / inactive state

    void GroupedEventToCycle(Key keycode) {
      if (!keycodeTriggers.TryGetValue(keycode, out Callbacks? action)) return;
      action?.Invoke(context);
    }

    public void OnKeyPress(string sdlWindow, SDL_Keycode key, int scancode, SDL_EventType action, int mods) {
      Key mappedKey = MapSdlKeyCode(key, action);

      switch (mappedKey.action) {
        case KeyAction.Pressed:
          context.KeyboardContext.AddPressedKey(mappedKey.keycode);
          context.KeyboardContext.RemoveHeldKey(mappedKey.keycode);
          context.KeyboardContext.RemoveReleasedKey(mappedKey.keycode);
          break;

        case KeyAction.Held:
          context.KeyboardContext.AddHeldKey(mappedKey.keycode);
          context.KeyboardContext.RemovePressedKey(mappedKey.keycode);
          context.KeyboardContext.RemoveReleasedKey(mappedKey.keycode);
          break;

        case KeyAction.Released:
          context.KeyboardContext.AddReleasedKey(mappedKey.keycode);
          context.KeyboardContext.RemovePressedKey(mappedKey.keycode);
          context.KeyboardContext.RemoveHeldKey(mappedKey.keycode);
          break;

        default:
          break;
      }

      GroupedEventToCycle(mappedKey);
      GroupedEventToCycle(new(Keycode.K_AllKeys, mappedKey.action));
      GroupedEventToCycle(new(Keycode.AllEvents, mappedKey.action));
    }

    public void OnMousePress(string sdlWindow, uint button, SDL_EventType action, int mods) {
      Key mappedButton = MapSdlMouseCode(button, action);

      switch (mappedButton.action) {
        case KeyAction.Pressed:
          context.MouseContext.AddPressedButton(mappedButton.keycode);
          context.MouseContext.RemoveHeldButton(mappedButton.keycode);
          context.MouseContext.RemoveReleasedButton(mappedButton.keycode);
          break;

        case KeyAction.Held:
          context.MouseContext.AddHeldButton(mappedButton.keycode);
          context.MouseContext.RemovePressedButton(mappedButton.keycode);
          context.MouseContext.RemoveReleasedButton(mappedButton.keycode);
          break;

        case KeyAction.Released:
          context.MouseContext.AddReleasedButton(mappedButton.keycode);
          context.MouseContext.RemovePressedButton(mappedButton.keycode);
          context.MouseContext.RemoveHeldButton(mappedButton.keycode);
          break;

        default:
          break;
      }

      GroupedEventToCycle(mappedButton);
      GroupedEventToCycle(new(Keycode.M_AllKeys, mappedButton.action));
      GroupedEventToCycle(new(Keycode.M_AllEvents, mappedButton.action));
      GroupedEventToCycle(new(Keycode.AllEvents, mappedButton.action));
    }

    public void OnMouseMove(string sdlWindow, double x, double y) {
      context.MouseContext.MoveCursor(x, y);

      GroupedEventToCycle(new(Keycode.M_Move, KeyAction.Unknown));
      GroupedEventToCycle(new(Keycode.M_AllEvents, KeyAction.Unknown));
      GroupedEventToCycle(new(Keycode.AllEvents, KeyAction.Unknown));
    }

    public void OnScroll(string sdlWindow, double x, double y) {
      if (y > 0) {
        GroupedEventToCycle(new(Keycode.M_ScrollDown, KeyAction.Unknown));
      }

      if (y < 0) {
        GroupedEventToCycle(new(Keycode.M_ScrollUp, KeyAction.Unknown));
      }

      context.MouseContext.MoveScroll(x, y);

      GroupedEventToCycle(new(Keycode.M_Scroll, KeyAction.Unknown));

      GroupedEventToCycle(new(Keycode.M_AllEvents, KeyAction.Unknown));
      GroupedEventToCycle(new(Keycode.AllEvents, KeyAction.Unknown));

      context.MouseContext.MoveScroll(0, 0);
    }

    public void OnCharPress(string sdlWindow, uint codepoint) {
      context.KeyboardContext.SetCodepoint(codepoint);
      GroupedEventToCycle(new(Keycode.K_AllChars, KeyAction.Unknown));
      context.KeyboardContext.RemoveCodepoint();
    }

    //

    void CreateTriggersFromFile(string path) {
      try {
        byte[] triggersData = File.ReadAllBytes(path);
        if (triggersData == null || triggersData.Length == 0) {
          Console.WriteLine($"Failed to open the file {path}");
          return;
        }

        string yamlContent = System.Text.Encoding.UTF8.GetString(triggersData);

        var deserializer = new DeserializerBuilder()
            // .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var config = deserializer.Deserialize<YamlMappingNode>(yamlContent);

        if (
          !config.Children.TryGetValue(new YamlScalarNode("triggers"), out YamlNode? triggersNode)
        ) {
          Console.WriteLine($"Can not load actions from file {path}. The 'triggers' key is missing.");
          return;
        }

        var triggers = (YamlSequenceNode?)triggersNode;

        if (triggers is null) {
          Console.WriteLine($"Can not load actions from file {path}. The 'triggers' key is  not a sequence.");
          return;
        }

        foreach (var triggerNode in triggers) {
          Keys bindings = [];

          foreach (var bindingNode in (YamlSequenceNode)triggerNode["bindings"]) {
            var key = MapStringToKeycode(bindingNode["key"].ToString());
            var actionType = MapStringToKeyAction(bindingNode["action"].ToString());

            bindings.Add(new(key, actionType));
          }

          var name = triggerNode["name"].ToString();

          AddTrigger(name, bindings);

          Console.WriteLine($"Loaded action {name}");
        }
      } catch (Exception ex) {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    //

    void AddTrigger(string groupName, Keys bindings, Callbacks callbacks) {
      AddGroupedTrigger(groupName, bindings, callbacks);
      AddKeycodeTrigger(bindings, callbacks);
    }

    void AddTrigger(string groupName, Key binding, Callbacks callbacks) {
      AddTrigger(groupName, CreateKeys(binding), callbacks);
    }

    void AddTrigger(string groupName, Key binding) {
      AddTrigger(groupName, CreateKeys(binding), new(delegate { }));
    }

    void AddTrigger(string groupName, Keys bindings) {
      AddTrigger(groupName, bindings, new(delegate { }));
    }

    void AddTrigger(string groupName) {
      AddGroupedTrigger(groupName, CreateKeys(), new(delegate { }));
    }

    //

    void AddTriggerBindings(string groupName, Keys bindings) {
      AddGroupedTriggerBindings(groupName, bindings);
      AddKeycodeTriggerBindings(groupName, bindings);
    }

    void AddTriggerBindings(string groupName, Key binding) {
      AddGroupedTriggerBindings(groupName, binding);
      AddKeycodeTriggerBindings(groupName, binding);
    }

    //

    void AddTriggerCallbacks(string groupName, Callbacks callbacks) {
      AddGroupedTriggerCallbacks(groupName, callbacks);
      AddKeycodeTriggerCallbacks(groupName, callbacks);
    }

    //

    void RemoveTrigger(string groupName) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        foreach (var binding in trigger.Bindings) {
          RemoveKeycodeTrigger(binding, trigger.Callbacks);
        }
      }

      groupedTriggers.Remove(groupName);
    }

    void RemoveTrigger(IEnumerable<string> groupNames) {
      foreach (var groupName in groupNames) {
        RemoveTrigger(groupName);
      }
    }
  };
}