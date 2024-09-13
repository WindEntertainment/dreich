using Wind;

internal class Game {
  public static void Main(String[] args) {
    WindEngine engine = new();
    // var assetDatabase = WindServices.Get<AssetDatabase>();
    // assetDatabase.LoadBundle("Data/Main");
    engine.Loop();
  }
}
