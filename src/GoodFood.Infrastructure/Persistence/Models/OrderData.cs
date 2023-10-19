using GoodFood.Domain.Entities;

namespace GoodFood.Infrastructure.Persistence.Models;
public class OrderData
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderLineData>? Lines { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime LastUpdate { get; set; }

}

public class OrderLineData
{
    public Guid OrderId { get; set; }
    public OrderData? Order { get; set; }

    public int FoodId { get; set; }
    public FoodData? Food { get; set; }
    public int Quantity { get; set; }
    public decimal FoodPrice { get; set; }
}
