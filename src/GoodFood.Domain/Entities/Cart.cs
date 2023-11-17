using System.Collections.ObjectModel;
using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;


public class Cart
{
    public int Id { get; }
    public CustomerInfo Customer { get; init; }
    public Collection<CartLine> Lines { get; }

    public Cart(CustomerInfo customer)
    {
        Id = default;
        Lines = [];
        Customer = customer;
        TimeCreated = DateTime.UtcNow;
        TimeUpdated = TimeCreated;
    }
    public Cart(int id, Collection<CartLine>? lines, CustomerInfo customer, DateTime timeCreated, DateTime timeUpdated)
    {
        Id = id;
        Lines = lines ?? [];
        Customer = customer;
        TimeCreated = timeCreated;
        TimeUpdated = timeUpdated;
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

        TimeUpdated = DateTime.UtcNow;
    }

    public bool IsAvailable()
    {
        var cartAgePerMinutes = (DateTime.UtcNow - TimeCreated).TotalMinutes;
        return cartAgePerMinutes < 60;
    }

    public DateTime TimeCreated { get; init; }
    public DateTime TimeUpdated { get; private set; }
}

public class CartLine
{
    public int Id { get; set; }

    public int FoodId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
