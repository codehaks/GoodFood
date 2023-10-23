using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodFood.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GatewayController : ControllerBase
{
    private readonly IOrderService _orderService;

    public GatewayController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> GetAsync(string orderId) // TransactionId, OrderId
    {
        // Validation
        // Confirm Gateway

        // Confirm Order

        await _orderService.ConfirmedAsync(Guid.Parse(orderId));

        // Redirect to Order Page
        return Ok();
    }
}
