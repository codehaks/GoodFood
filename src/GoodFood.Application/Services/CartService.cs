using System.Reflection;
using GoodFood.Application.Contracts;
using GoodFood.Application.Models;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using Mapster;

namespace GoodFood.Application.Services;
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddToCartAsync(CartLineAddModel model)
    {
        var cart = _unitOfWork.CartRepository.FindByCustomerId(model.UserInfo.Adapt<CustomerInfo>());
        if (cart is null)
        {
            cart = new Cart(new Domain.Values.CustomerInfo(model.UserInfo.UserId, model.UserInfo.UserName));
            _unitOfWork.CartRepository.Add(cart);
            await _unitOfWork.CommitAsync();

            cart = _unitOfWork.CartRepository.FindByCustomerId(model.UserInfo.Adapt<CustomerInfo>());
        }

        cart.AddOrUpdate(new CartLine { FoodId = model.FoodId, Price = model.Price, Quantity = model.Quantity });

        _unitOfWork.CartRepository.Update(cart);

        await _unitOfWork.CommitAsync();
    }

    public async Task<IList<CartLineModel>> GetByUserIdAsync(UserInfo userInfo)
    {
        var cart = await Task.FromResult(_unitOfWork.CartRepository.FindByCustomerId(userInfo.Adapt<CustomerInfo>()));

        if (cart is null)
        {
            return new List<CartLineModel>();
        }
        return cart.Lines.Adapt<IList<CartLineModel>>();

    }

    public async Task Update(IList<CartLineModel> cartLines, UserInfo userInfo)
    {
        var cart = _unitOfWork.CartRepository.FindByCustomerId(userInfo.Adapt<CustomerInfo>());
        if (cart is null)
        {
            cart = new Cart(new Domain.Values.CustomerInfo(userInfo.UserId, userInfo.UserName));

            _unitOfWork.CartRepository.Add(cart);
            await _unitOfWork.CommitAsync();

        }

        cart.Clear();

        foreach (var item in cartLines)
        {
            cart.AddOrUpdate(new CartLine { FoodId = item.FoodId, Price = item.Price, Quantity = item.Quantity });
        }
        _unitOfWork.CartRepository.Update(cart);
        await _unitOfWork.CommitAsync();
    }
}

public record CartLineAddModel(int FoodId, UserInfo UserInfo, decimal Price, int Quantity);
public record CartLineModel
{
    public int FoodId { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }

    public decimal Price { get; init; }
    public int Quantity { get; set; }

    public decimal LineTotalPrice { get { return Price * Quantity; } }
};

