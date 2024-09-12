using Wind;

internal class Game
{
    public static void Main(String[] args)
    {
        var engine = new WindEngine();
        // var assetDatabase = WindServices.Get<AssetDatabase>();
        // assetDatabase.LoadBundle("Data/Main");
        engine.Loop();
    }
}