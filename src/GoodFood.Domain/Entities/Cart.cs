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
    }
    public Cart(int id, Collection<CartLine>? lines, CustomerInfo customer)
    {
        Id = id;
        Lines = lines ?? [];
        Customer = customer;
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
            existingLine.Quantity = existingLine.Quantity + 1;
        }
        else
        {
            // If no existing line found, add the new cartLine to the collection.
            Lines.Add(cartLine);
        }
    }
}

public class CartLine
{
    public int Id { get; set; }

    public int FoodId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
