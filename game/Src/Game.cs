using System.Runtime.InteropServices;
using Wind.Math;

internal class Game {
  [DllImport("wind.so", EntryPoint = "windInitRenderer", CallingConvention = CallingConvention.Cdecl)]
  public static extern bool WindInitRenderer();

  [DllImport("wind.so", EntryPoint = "windCreateWindow", CallingConvention = CallingConvention.Cdecl)]
  public static extern IntPtr WindCreateWindow(int position_x, int position_y, int width, int height, string title);

  [DllImport("wind.so", EntryPoint = "windPostInitRenderer", CallingConvention = CallingConvention.Cdecl)]
  public static extern bool WindPostInitRenderer();

  public static void Main(string[] args) {
    if (!WindInitRenderer())
      return;

    WindCreateWindow(100, 100, 800, 600, "Hello From C#");

    if (!WindPostInitRenderer())
      return;

    Task.Delay(1000).Wait();
    // WindEngine engine = new();
    // var assetDatabase = WindServices.Get<AssetDatabase>();
    // assetDatabase.LoadBundle("Data/Main");
    // engine.Loop();
  }
}