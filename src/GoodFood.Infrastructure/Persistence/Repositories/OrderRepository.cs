using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using GoodFood.Infrastructure.Persistence.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoodFood.Infrastructure.Persistence.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly GoodFoodDbContext _db;

    public OrderRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public async Task<Order> FindByIdAsync(Guid orderId)
    {
        var orderData = await _db.Orders.FindAsync(orderId);

        ArgumentNullException.ThrowIfNull(orderData);
        // TODO: Add null check

        var customer = new Customer { UserId = orderData.UserId, UserName = orderData.UserName };

        var orderLinesData = await _db.OrderLines.Where(l => l.OrderId == orderId).ToListAsync();

        var order = new Order(customer, orderData.Status, orderData.LastUpdate)
        {
            Id = orderId
        };

        foreach (var line in orderLinesData)
        {
            order.AddLine(new OrderLine
            {
                FoodId = line.FoodId,
                FoodPrice = new Domain.Values.Money(line.FoodPrice),
                OrderId = line.OrderId,
                Quantity = line.Quantity,
            });
        }


        return order;
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

    public async Task UpdateAsync(Order order)
    {
        var orderData = await _db.Orders.FindAsync(order.Id);

        ArgumentNullException.ThrowIfNull(orderData);

        orderData.Status = order.Status;
        orderData.LastUpdate = order.LastUpdate;
        orderData.TotalAmount = order.TotalAmount.Value;

        orderData.Lines = order.Lines.Adapt<ICollection<OrderLineData>>();
        _db.Entry(orderData).State = EntityState.Modified;
    }

    public async Task<IList<Order>> GetAllAsync()
    {
        var orders = await _db.Orders.Select(o => new Order(
            new Customer { UserId = o.UserId, UserName = o.UserName }, o.Status, o.LastUpdate)
        { Id = o.Id })
            .ToListAsync();

        return orders;
    }
}
