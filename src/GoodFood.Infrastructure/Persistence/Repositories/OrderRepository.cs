using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using GoodFood.Infrastructure.Persistence.Models;

namespace GoodFood.Infrastructure.Persistence.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly GoodFoodDbContext _db;

    public OrderRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public async Task Place(Order order)
    {
        var orderData = new OrderData
        {
            UserId = order.Customer.UserId,
            UserName = order.Customer.UserName,
            LastUpdate = order.LastUpdate,
            Status = order.Status,
            TotalAmount = order.TotalAmount.Value,
            Id = order.Id
        };

        var orderLines = new List<OrderLineData>();
        foreach (var line in order.Lines)
        {
            orderLines.Add(new OrderLineData
            {
                FoodId = line.FoodId,
                OrderId = orderData.Id,
                Quantity = line.Quantity,
                FoodPrice = line.FoodPrice.Value,
            });
        }

        _db.OrderLines.AddRange(orderLines);
        _db.Orders.Add(orderData);

    }

    public void Update(Order order)
    {

    }
}
