using System.Runtime.InteropServices;
using Wind;
using Wind.Math;

enum EventType {
  NONE,

  MOUSE_MOTION,
  MOUSE_BUTTON_DOWN,
  MOUSE_BUTTON_UP,

  KEY_DOWN,
  KEY_UP,

  QUIT
};

struct WindEvent {
  public EventType type;

  public double x;
  public double y;

  public InputSystem.Keycode keycode;
};

internal class Game {
  [DllImport("wind.so", EntryPoint = "windInitRenderer", CallingConvention = CallingConvention.Cdecl)]
  public static extern bool WindInitRenderer();

  [DllImport("wind.so", EntryPoint = "windCreateWindow", CallingConvention = CallingConvention.Cdecl)]
  public static extern IntPtr WindCreateWindow(int position_x, int position_y, int width, int height, string title);

  [DllImport("wind.so", EntryPoint = "windPostInitRenderer", CallingConvention = CallingConvention.Cdecl)]
  public static extern bool WindPostInitRenderer();

  [DllImport("wind.so", EntryPoint = "windPollEvent", CallingConvention = CallingConvention.Cdecl)]
  public static extern WindEvent WindPollEvent();

  public static void Main(string[] args) {
    if (!WindInitRenderer())
      return;

    WindCreateWindow(100, 100, 800, 600, "Hello From C#");

    if (!WindPostInitRenderer())
      return;


    Console.WriteLine("Begin");

    bool running = true;
    while (running) {
      var _event = WindPollEvent();
      switch (_event.type) {
        case EventType.QUIT:
          running = false;
          break;
      }
    }

    Console.WriteLine("End");

    // WindEngine engine = new();
    // var assetDatabase = WindServices.Get<AssetDatabase>();
    // assetDatabase.LoadBundle("Data/Main");
    // engine.Loop();
  }
}