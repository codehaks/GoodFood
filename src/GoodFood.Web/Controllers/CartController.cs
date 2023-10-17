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

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [Authorize]
    [HttpPost]
    [Route("api/cart/addline")]
    public async Task<IActionResult> AddToCartAsync(CartLineInput model)
    {
        await _cartService.AddToCartAsync(new CartLineAddModel(model.FoodId, User.GetUserInfo(), model.FoodPrice, 1));
        return Ok();
    }
}


