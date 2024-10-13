using System;
using System.Runtime.InteropServices;

public class Program
{
  [UnmanagedCallersOnly(EntryPoint = "add")]
  public static int add(int a, int b)
  {
    return a + b;
  }
}
