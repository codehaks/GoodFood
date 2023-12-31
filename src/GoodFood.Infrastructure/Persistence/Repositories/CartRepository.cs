using System.Collections.ObjectModel;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using GoodFood.Infrastructure.Persistence.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoodFood.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly GoodFoodDbContext _db;
    private readonly TimeProvider _timeProvider;
    public CartRepository(GoodFoodDbContext db, TimeProvider timeProvider)
    {
        _db = db;
        _timeProvider = timeProvider;
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

        var lines = cartData.Lines?.Adapt<Collection<CartLine>>();

        var cart = new Cart(cartData.Id, lines, customer, cartData.TimeCreated, cartData.TimeUpdated,_timeProvider);
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
}
