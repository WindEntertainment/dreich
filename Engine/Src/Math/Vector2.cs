using System.Numerics;
using System.Runtime.CompilerServices;

namespace Wind.Math;

public struct Vec2<T>(T x, T y) where T : INumber<T>
{
  public T x = x;
  public T y = y;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vec2<T> operator +(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vec2<T> operator -(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vec2<T> operator *(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x * rhs.x, lhs.y * rhs.y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vec2<T> operator /(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x / rhs.x, lhs.y / rhs.y);
}