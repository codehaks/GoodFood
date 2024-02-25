using System.Collections.ObjectModel;
using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;


public class Cart
{
    public int Id { get; }
    public CustomerInfo Customer { get; init; }
    public Collection<CartLine> Lines { get; }

    private readonly TimeProvider _timeProvider;

    public Cart(CustomerInfo customer, TimeProvider timeProvider)
    {
        Id = default;
        Lines = [];
        Customer = customer;
        TimeCreated = timeProvider.GetUtcNow().UtcDateTime;//DateTime.UtcNow;
        TimeUpdated = TimeCreated;
        _timeProvider = timeProvider;
    }
    public Cart(int id, Collection<CartLine>? lines, CustomerInfo customer, DateTime timeCreated, DateTime timeUpdated, TimeProvider timeProvider)
    {
        Id = id;
        Lines = lines ?? [];
        Customer = customer;
        TimeCreated = timeCreated;
        TimeUpdated = timeUpdated;
        _timeProvider = timeProvider;
    }

    public void Clear()
    {
        Lines.Clear();
    }

    public void AddOrUpdate(CartLine cartLine)
    {
        var existingLine = Lines.FirstOrDefault(line => line.FoodId == cartLine.FoodId);

        if (existingLine != null)
        {
            // If a line with the same FoodId already exists, update the quantity.
            existingLine.Quantity = cartLine.Quantity;
        }
        else
        {
            // If no existing line found, add the new cartLine to the collection.
            Lines.Add(cartLine);
        }

        TimeUpdated = _timeProvider.GetUtcNow().UtcDateTime;
    }

    public bool IsAvailable()
    {
        var cartAgePerMinutes = (_timeProvider.GetUtcNow().UtcDateTime - TimeCreated).TotalMinutes;
        return cartAgePerMinutes < 1;
    }

    public DateTime TimeCreated { get; init; }
    public DateTime TimeUpdated { get; private set; }
}

public class CartLine
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public string? FoodName { get; set; }

    public string? FoodDescription { get; set; }

    public string? FoodImagePath { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
