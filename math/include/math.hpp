namespace wind {
namespace math {

template <typename T>
struct vec2 {
  T x;
  T y;

  vec2<T> operator+(vec2<T> rhs) {
    return {x + rhs.x, y + rhs.y};
  }

  vec2<T> operator-(vec2<T> rhs) {
    return {x - rhs.x, y - rhs.y};
  }

  vec2<T> operator*(vec2<T> rhs) {
    return {x * rhs.x, y * rhs.y};
  }

  vec2<T> operator/(vec2<T> rhs) {
    return {x / rhs.x, y / rhs.y};
  }
};

using vec2f = vec2<float>;
using vec2i = vec2<int>;

} // namespace math
} // namespace wind