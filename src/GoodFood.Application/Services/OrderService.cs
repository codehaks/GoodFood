using GoodFood.Application.Contracts;
using GoodFood.Application.Models;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using Mapster;

namespace GoodFood.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Place(UserInfo userInfo)
    {
        var cart = _unitOfWork.CartRepository.FindByCustomerId(userInfo.Adapt<CustomerInfo>());
        if (cart == null)
        {
            throw new InvalidOperationException("Can not find cart");
        }

        var order = Order.FromCart(cart);
        await _unitOfWork.OrderRepository.Place(order);
        await _unitOfWork.CommitAsync();
    }
}
