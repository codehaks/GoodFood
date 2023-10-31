using GoodFood.Domain.Values;

namespace GoodFood.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Money_ObjectsWithSameValue_ShouldBeEqual()
    {
        // Arrange
        decimal value1 = 100.50M;
        decimal value2 = 100.50M;

        // Act
        Money money1 = new Money(value1);
        Money money2 = new Money(value2);

        // Assert
        Assert.Equal(money1, money2);
        Assert.True(money1.Equals(money2));
        Assert.True(money1 == money2);
        Assert.False(money1 != money2);
    }

    [Fact]
    public void Money_Addition_ShouldCombineValues()
    {
        // Arrange
        Money money1 = new Money(100.50M);
        Money money2 = new Money(50.25M);

        // Act
        Money result = money1 + money2;

        // Assert
        Assert.Equal(150.75M, result.Value);
    }
}