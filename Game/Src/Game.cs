using Wind.Math;
using System.Runtime.InteropServices;

internal class Game {
  [DllImport("renderer.dll", CallingConvention = CallingConvention.Cdecl)]
  public static extern int testRenderLib(int a, int b);

  public static void Main(String[] args) {
    Vec2<int> test = new(5, 6);

    var result = testRenderLib(5, 2);
    Console.WriteLine(result);
    // WindEngine engine = new();
    // var assetDatabase = WindServices.Get<AssetDatabase>();
    // assetDatabase.LoadBundle("Data/Main");
    // engine.Loop();
  }
}
