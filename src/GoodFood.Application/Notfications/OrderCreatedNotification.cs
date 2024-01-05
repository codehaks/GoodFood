using GoodFood.Application.Contracts;
using MediatR;

namespace GoodFood.Application.Notfications;
public class OrderCreatedNotification : INotification
{
    public Guid OrderId { get; set; }
    public required string UserEmail { get; set; }
    public required OrderDetails OrderDetails { get; init; }
    public required IServiceProvider ServiceProvider { get; init; }

}
