namespace Wind.Utils {
  class EnumExtensions {
    public static TEnum ParseOrDefault<TEnum>(ReadOnlySpan<char> value, TEnum defaultValue) where TEnum : struct {
      if (Enum.TryParse(value, out TEnum action)) return action;
      return defaultValue;
    }
  }
}