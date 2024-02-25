using System.Collections.ObjectModel;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using GoodFood.Infrastructure.Persistence.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodFood.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly GoodFoodDbContext _db;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<CartRepository> _logger;
    public CartRepository(GoodFoodDbContext db, TimeProvider timeProvider, ILogger<CartRepository> logger)
    {
        _db = db;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public void Add(Cart cart)
    {
        var cartData = cart.Adapt<CartData>();
        cartData.UserId = cart.Customer.UserId;
        _db.Carts.Add(cartData);

    }

    public void Remove(int cartId)
    {
        var cart = _db.Carts.FirstOrDefault(c => c.Id == cartId);
        if (cart is not null)
        {
            _db.Carts.Remove(cart);
        }
    }

    public Cart? FindByCustomerId(CustomerInfo customer)
    {
        var cartData = _db.Carts.Include(c => c.Lines)
            .OrderByDescending(c => c.TimeUpdated)
            .FirstOrDefault(c => c.UserId == customer.UserId);
        if (cartData is null)
        {
            return null;
        }

        var query = from cartLine in cartData.Lines
                    join food in _db.Foods on cartLine.FoodId equals food.Id
                    select new CartLine
                    {
                        Id = cartLine.Id,
                        FoodDescription = food.Description,
                        FoodName = food.Name,

                        FoodId = food.Id,
                        Price = cartLine.Price,
                        Quantity = cartLine.Quantity,
                        FoodImagePath = food.ImagePath,
                    };


        var lines = query.Adapt<Collection<CartLine>>();

        var cart = new Cart(cartData.Id, lines, customer, cartData.TimeCreated, cartData.TimeUpdated, _timeProvider);
        return cart;
    }

    public void Update(Cart cart)
    {
        var oldCart = _db.Carts.FirstOrDefault(c => c.Id == cart.Id);
        if (oldCart is not null)
        {
            oldCart.Lines = cart.Lines.Adapt<ICollection<CartLineData>>();
            _db.Entry(oldCart).State = EntityState.Modified;
        }

    }

    public void RemoveExpiredCarts()
    {
        var carts = _db.Carts
            .Select(c => new Cart(c.Id, null, new CustomerInfo(c.UserId, c.UserId), c.TimeCreated, c.TimeUpdated, _timeProvider))
            .ToList()
            .Where(c => !c.IsAvailable())
            .Select(c => c.Id);

        var cartsToRemove = _db.Carts.Where(c => carts.Contains(c.Id));

        _db.Carts.RemoveRange(cartsToRemove);

        _logger.LogInformation("{Count} expired carts found.", carts.Count());
    }
}
