namespace GoodFood.Infrastructure.Persistence.Models;
public class CartData
{

    public int Id { get; set; }

    public required string UserId { get; set; }
    public ICollection<CartLineData>? Lines { get; set; }
}

public class CartLineData
{

    public int Id { get; set; }
    public int CartId { get; set; }
    public CartData? Cart { get; set; }

    public int FoodId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }


}
