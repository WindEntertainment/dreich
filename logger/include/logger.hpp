#define __declspec(f)

template <typename T>
struct Vector2 {
  T x;
  T y;

  Vector2<T> operator+(Vector2<T> rhs) {
    return {x + rhs.x, y + rhs.y};
  }
};

using Vector2f = Vector2<float>;
using Vector2i = Vector2<int>;

extern "C" {
__declspec(dllexport) int testMathLib(int a, int b);
}