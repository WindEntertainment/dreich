namespace Wind {
  public static class WindServices {
    private static ServiceLocator _instance;
    public static ServiceLocator Instance = _instance ??= new();
  }
}
