using GoodFood.Application.Contracts;
using GoodFood.Application.Notfications;
using GoodFood.Domain.Entities;
using GoodFood.Infrastructure.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodFood.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GatewayController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;
    public GatewayController(IOrderService orderService, UserManager<ApplicationUser> userManager, IMediator mediator, IServiceProvider serviceProvider)
    {
        _orderService = orderService;
        _userManager = userManager;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }

    [Route("{orderId:guid}")]
    public async Task<IActionResult> GetAsync(Guid orderId) // TransactionId, OrderId
    {
        await _orderService.ConfirmedAsync(orderId);

        var order = await _orderService.GetOrderDetailsAsync(orderId);
        var user = await _userManager.GetUserAsync(User);


        await _mediator.Publish(new OrderCreatedNotification
        {
            OrderId = orderId,
            OrderDetails = order,
            UserEmail = user?.Email!,
            ServiceProvider = _serviceProvider

        });

        // Redirect to Order Page
        return RedirectToPage("/Orders/Details", new { area = "user", OrderId = orderId });
    }
}
