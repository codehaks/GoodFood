using GoodFood.Application.Contracts;
using GoodFood.Application.Notfications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoodFood.Infrastructure.NotificationHandlers;

public class OrderCreateEmailHandler : INotificationHandler<OrderCreatedNotification>
{
    private readonly ILogger<OrderCreateEmailHandler> _logger;

    public OrderCreateEmailHandler(ILogger<OrderCreateEmailHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {

        using var scope = notification.ServiceProvider.CreateScope();
        var emailQueueService =
            scope.ServiceProvider
                .GetRequiredService<IEmailQueueService>();

        emailQueueService.Enqueue(new EmailJobDto
        {
            EmailAddress = notification.UserEmail,
            EmailTitle = "New Order",
            EmailBody = "Order Confirmed"
        });

        _logger.LogInformation("OrderCreated email handled");
        await Task.CompletedTask;
    }
}
