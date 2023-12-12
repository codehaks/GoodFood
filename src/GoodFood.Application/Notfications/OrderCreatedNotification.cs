using MediatR;

namespace GoodFood.Application.Notfications;
public class OrderCreatedNotification : INotification
{
    public Guid OrderId { get; set; }
}
