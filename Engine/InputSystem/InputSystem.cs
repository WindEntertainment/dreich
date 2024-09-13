using System;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static SDL2.SDL;

namespace Wind {
  public partial class InputSystem() {

    InputSystemContext context = new InputSystemContext();

    void groupedEventToCycle(Key keycode) {
      if (!keycodeTriggers.TryGetValue(keycode, out Callbacks? action)) return;
      action?.Invoke(context);
    }

    public void onKeyPress(string sdlWindow, SDL_Keycode key, int scancode, SDL_EventType action, int mods) {
      Key mappedKey = mapSdlKeyCode(key, action);

      switch (mappedKey.action) {
        case KeyAction.Pressed:
          context.KeyboardContext.addPressedKey(mappedKey.keycode);
          context.KeyboardContext.removeHeldKey(mappedKey.keycode);
          context.KeyboardContext.removeReleasedKey(mappedKey.keycode);
          break;

        case KeyAction.Held:
          context.KeyboardContext.addHeldKey(mappedKey.keycode);
          context.KeyboardContext.removePressedKey(mappedKey.keycode);
          context.KeyboardContext.removeReleasedKey(mappedKey.keycode);
          break;

        case KeyAction.Released:
          context.KeyboardContext.addReleasedKey(mappedKey.keycode);
          context.KeyboardContext.removePressedKey(mappedKey.keycode);
          context.KeyboardContext.removeHeldKey(mappedKey.keycode);
          break;

        default:
          break;
      }

      groupedEventToCycle(mappedKey);
      groupedEventToCycle(new Key(Keycode.K_AllKeys, mappedKey.action));
      groupedEventToCycle(new Key(Keycode.AllEvents, mappedKey.action));
    }

    public void onMousePress(string sdlWindow, uint button, SDL_EventType action, int mods) {
      Key mappedButton = mapSdlMouseCode(button, action);

      switch (mappedButton.action) {
        case KeyAction.Pressed:
          context.MouseContext.addPressedButton(mappedButton.keycode);
          context.MouseContext.removeHeldButton(mappedButton.keycode);
          context.MouseContext.removeReleasedButton(mappedButton.keycode);
          break;

        case KeyAction.Held:
          context.MouseContext.addHeldButton(mappedButton.keycode);
          context.MouseContext.removePressedButton(mappedButton.keycode);
          context.MouseContext.removeReleasedButton(mappedButton.keycode);
          break;

        case KeyAction.Released:
          context.MouseContext.addReleasedButton(mappedButton.keycode);
          context.MouseContext.removePressedButton(mappedButton.keycode);
          context.MouseContext.removeHeldButton(mappedButton.keycode);
          break;

        default:
          break;
      }

      groupedEventToCycle(mappedButton);
      groupedEventToCycle(new Key(Keycode.M_AllKeys, mappedButton.action));
      groupedEventToCycle(new Key(Keycode.M_AllEvents, mappedButton.action));
      groupedEventToCycle(new Key(Keycode.AllEvents, mappedButton.action));
    }

    public void onMouseMove(string sdlWindow, double x, double y) {
      context.MouseContext.moveCursor(x, y);

      groupedEventToCycle(new Key(Keycode.M_Move, KeyAction.Unknown));
      groupedEventToCycle(new Key(Keycode.M_AllEvents, KeyAction.Unknown));
      groupedEventToCycle(new Key(Keycode.AllEvents, KeyAction.Unknown));
    }

    public void onScroll(string sdlWindow, double x, double y) {
      if (y > 0) {
        groupedEventToCycle(new Key(Keycode.M_ScrollDown, KeyAction.Unknown));
      }

      if (y < 0) {
        groupedEventToCycle(new Key(Keycode.M_ScrollUp, KeyAction.Unknown));
      }

      context.MouseContext.moveScroll(x, y);

      groupedEventToCycle(new Key(Keycode.M_Scroll, KeyAction.Unknown));

      groupedEventToCycle(new Key(Keycode.M_AllEvents, KeyAction.Unknown));
      groupedEventToCycle(new Key(Keycode.AllEvents, KeyAction.Unknown));

      context.MouseContext.moveScroll(0, 0);
    }

    public void onCharPress(string sdlWindow, uint codepoint) {
      context.KeyboardContext.setCodepoint(codepoint);
      groupedEventToCycle(new Key(Keycode.K_AllChars, KeyAction.Unknown));
      context.KeyboardContext.removeCodepoint();
    }

    //

    void createTriggersFromFile(string path) {
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
            var key = mapStringToKeycode(bindingNode["key"].ToString());
            var actionType = mapStringToKeyAction(bindingNode["action"].ToString());

            bindings.Add(new Key(key, actionType));
          }

          var name = triggerNode["name"].ToString();

          addTrigger(name, bindings);

          Console.WriteLine($"Loaded action {name}");
        }
      } catch (Exception ex) {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    //

    void addTrigger(string groupName, Keys bindings, Callbacks callbacks) {
      addGroupedTrigger(groupName, bindings, callbacks);
      addKeycodeTrigger(bindings, callbacks);
    }

    void addTrigger(string groupName, Key binding, Callbacks callbacks) {
      addTrigger(groupName, CreateKeys(binding), callbacks);
    }

    void addTrigger(string groupName, Key binding) {
      addTrigger(groupName, CreateKeys(binding), new(delegate { }));
    }

    void addTrigger(string groupName, Keys bindings) {
      addTrigger(groupName, bindings, new(delegate { }));
    }

    void addTrigger(string groupName) {
      addGroupedTrigger(groupName, CreateKeys(), new(delegate { }));
    }

    //

    void addTriggerBindings(string groupName, Keys bindings) {
      addGroupedTriggerBindings(groupName, bindings);
      addKeycodeTriggerBindings(groupName, bindings);
    }

    void addTriggerBindings(string groupName, Key binding) {
      addGroupedTriggerBindings(groupName, binding);
      addKeycodeTriggerBindings(groupName, binding);
    }

    //

    void addTriggerCallbacks(string groupName, Callbacks callbacks) {
      addGroupedTriggerCallbacks(groupName, callbacks);
      addKeycodeTriggerCallbacks(groupName, callbacks);
    }

    //

    void removeTrigger(string groupName) {
      if (groupedTriggers.TryGetValue(groupName, out Trigger trigger)) {
        foreach (var binding in trigger.Bindings) {
          removeKeycodeTrigger(binding, trigger.Callbacks);
        }
      }

      groupedTriggers.Remove(groupName);
    }

    void removeTrigger(IEnumerable<string> groupNames) {
      foreach (var groupName in groupNames) {
        removeTrigger(groupName);
      }
    }
  };
}
