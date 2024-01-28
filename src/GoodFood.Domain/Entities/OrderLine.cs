using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

public class OrderLine
{
    public Guid OrderId { get; set; }
    public int FoodId { get; set; }

    public string FoodName { get; set; }
    public required Money FoodPrice { get; set; }

    public int Quantity { get; set; }

    public Money LineTotal { get { return new Money(FoodPrice.Value * Quantity); } }
}

