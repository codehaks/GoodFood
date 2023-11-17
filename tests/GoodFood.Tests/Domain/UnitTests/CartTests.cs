using System.Collections.ObjectModel;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using Moq;

namespace GoodFood.Tests.Domain.UnitTests;
public class CartTests
{
    private readonly TimeProvider timeProvider = TimeProvider.System;

    [Fact]
    public void New_Line_Is_Added_to_CartLines()
    {
        // Arrange
        var customer = new CustomerInfo("1", "u");
        var cart = new Cart(customer, timeProvider);
        var cartLine = new CartLine { FoodId = 1, Quantity = 1, Price = 10.0M };
        // Act
        cart.AddOrUpdate(cartLine);
        // Assert
        Assert.Single(cart.Lines);
        Assert.Equal(1, cart.Lines[0].FoodId);
    }

    [Fact]
    public void Update_Line_quantity_when_foodId_already_exists()
    {
        // Arrange
        var customer = new CustomerInfo("1", "u");
        var cart = new Cart(customer, timeProvider);
        var cartLine1 = new CartLine { FoodId = 1, Quantity = 1, Price = 10.0M };
        var cartLine2 = new CartLine { FoodId = 1, Quantity = 3, Price = 10.0M };
        // Act
        cart.AddOrUpdate(cartLine1);
        cart.AddOrUpdate(cartLine2);

        // Assert
        Assert.Single(cart.Lines);
        Assert.Equal(3, cart.Lines[0].Quantity);
    }

    [Fact]
    public void Update_Line_quantity_when_foodId_already_exists_with_loaded_cart()
    {
        // Arrange
        var customer = new CustomerInfo("1", "u");

        var cartLine1 = new CartLine { FoodId = 1, Quantity = 1, Price = 10.0M };
        var cartLine2 = new CartLine { FoodId = 2, Quantity = 3, Price = 10.0M };

        var cartLine3 = new CartLine { FoodId = 2, Quantity = 5, Price = 10.0M };

        var cartLines = new Collection<CartLine>() { cartLine1, cartLine2 };

        var cart = new Cart(1, cartLines, customer, DateTime.UtcNow, DateTime.UtcNow, timeProvider);
        // Act
        cart.AddOrUpdate(cartLine3);


        // Assert
        Assert.Equal(2, cart.Lines.Count);
        Assert.Equal(5, cart.Lines.First(l => l.FoodId == 2).Quantity);
    }
    [Fact]
    public void IsAvailable_ReturnsFalseAfter60Minutes()
    {
        // Arrange
        var customer = new CustomerInfo("1", "u");

        var timeCreated = new DateTimeOffset(DateTime.UtcNow);

        var timerMock = new Mock<TimeProvider>();

        timerMock.Setup(t => t.GetUtcNow()).Returns(timeCreated);
        var cart = new Cart(customer, timerMock.Object);

        timerMock.Setup(t => t.GetUtcNow()).Returns(timeCreated.AddMinutes(61));
        // Act
        var result = cart.IsAvailable();

        // Assert
        Assert.False(result);
    }
}
