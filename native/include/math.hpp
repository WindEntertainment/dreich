namespace wind {
namespace math {

template <typename T>
struct Vec2 {
  T x;
  T y;

  Vec2<T> operator+(Vec2<T> rhs) {
    return {x + rhs.x, y + rhs.y};
  }

  Vec2<T> operator-(Vec2<T> rhs) {
    return {x - rhs.x, y - rhs.y};
  }

  Vec2<T> operator*(Vec2<T> rhs) {
    return {x * rhs.x, y * rhs.y};
  }

  Vec2<T> operator/(Vec2<T> rhs) {
    return {x / rhs.x, y / rhs.y};
  }
};

using Vec2f = Vec2<float>;
using Vec2i = Vec2<int>;

} // namespace math
} // namespace wind