using GoodFood.Application.Models;
using GoodFood.Domain.Entities;

namespace GoodFood.Application.Contracts;
public interface IOrderService
{
    Task ConfirmedAsync(Guid orderId);

    Task Place(UserInfo userInfo);

    Task<IList<OrderInfo>> GetAllAsync();
    Task<OrderDetails> GetOrderDetailsAsync(Guid orderId);
    Task ReadyForPickupAsync(Guid orderId);
}

public record OrderInfo(Guid Id, UserInfo UserInfo, DateTime LastUpdate, OrderStatus Status);

public record OrderDetails
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderLineDto>? Lines { get; init; }
    public OrderStatus Status { get; set; }
    public DateTime LastUpdate { get; set; }

}

public record OrderLineDto
{
    public Guid OrderId { get; set; }
    public int FoodId { get; set; }
    public required string FoodName { get; set; }
    public int Quantity { get; set; }
    public decimal FoodPriceValue { get; set; }
}
