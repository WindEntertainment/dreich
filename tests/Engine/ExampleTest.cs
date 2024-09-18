using Xunit;

namespace Engine
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      // Arrange
      int value1 = 5;
      int value2 = 10;

      // Act
      int result = value1 + value2;

      // Assert
      Assert.Equal(15, result);
    }
  }
}
