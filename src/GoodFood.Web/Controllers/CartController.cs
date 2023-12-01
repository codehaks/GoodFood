using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using GoodFood.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodFood.Web.Controllers;

public record CartLineInput(int FoodId, decimal FoodPrice);

[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [Authorize]
    [HttpPost]
    [Route("api/cart/addline")]
    public async Task<IActionResult> AddToCartAsync(CartLineInput model)
    {
        await _cartService.AddToCartAsync(new CartLineAddModel(model.FoodId, User.GetUserInfo(), model.FoodPrice, 1));

        //_logger.LogInformation("Item added to cart {@FoodInput}", model);
        _logger.ItemAddedToCart(model);
        return Ok();
    }
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Item added to cart {cartLine}")]
    public static partial void ItemAddedToCart(this ILogger logger, [LogProperties] CartLineInput cartLine);
}
