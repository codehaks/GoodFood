using GoodFood.Application.Contracts;
using GoodFood.Application.Notfications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GoodFood.Infrastructure.NotificationHandlers;
public class OrderCreateSmsHandler : INotificationHandler<OrderCreatedNotification>
{
    private readonly ILogger<OrderCreateSmsHandler> _logger;
    private readonly IOrderService _orderService;
    public OrderCreateSmsHandler(ILogger<OrderCreateSmsHandler> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order Created SMS Handle started");
        await Task.Delay(10000, cancellationToken);
        var order = await _orderService.GetOrderDetailsAsync(notification.OrderId);
        _logger.LogInformation("Order Found => {Order}", order.Status);
        _logger.LogInformation("Order Created SMS handled => {Order}",order.TotalAmount);
        await Task.CompletedTask;
    }
}



public class OrderCreateEmailHandler : INotificationHandler<OrderCreatedNotification>
{
    private readonly ILogger<OrderCreateEmailHandler> _logger;

    public OrderCreateEmailHandler(ILogger<OrderCreateEmailHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order Created EMAIL Handle started");
        await Task.Delay(15000, cancellationToken);
        _logger.LogInformation("Order Created EMAIL handled");
        await Task.CompletedTask;
    }
}
