using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;

Console.WriteLine("Hello, Browser!");

// [DllImport("mymath.dll")]
// static extern int square();



// [DllImport("mymath", EntryPoint = "main")]
// static extern int add(int argc, string[] argv);



public partial class MyClass {
  // [DllImport("mymath.wasm", EntryPoint = "square")]
  // static extern int square();
  [DllImport("mymath.dll")]
  static extern int square();

  [JSExport]
  internal static string Greeting() {
    try {
      var text = $"Hello, World! Greetings from {GetHRef()}";

      Console.WriteLine(text);
      Console.WriteLine(square());

      return text;
    } catch (Exception ex) {
      Console.WriteLine($"Error calling WASM function: {ex.Message}");
      Console.WriteLine($"Stack trace: {ex.StackTrace}");
      Console.WriteLine(ex);
      return "Error";
    }
  }

  [JSImport("window.location.href", "main.js")]
  internal static partial string GetHRef();
}
