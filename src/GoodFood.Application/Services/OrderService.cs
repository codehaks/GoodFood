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

    public async Task ConfirmedAsync(Guid orderId)
    {
        // Validation 
        var order = await _unitOfWork.OrderRepository.FindByIdAsync(orderId);

        order.Confirm();

        await _unitOfWork.OrderRepository.UpdateAsync(order);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Guid> PlaceAsync(UserInfo userInfo)
    {
        var cart = _unitOfWork.CartRepository.FindByCustomerId(userInfo.Adapt<CustomerInfo>());
        if (cart == null)
        {
            throw new InvalidOperationException("Can not find cart");
        }

        var order = Order.FromCart(cart);

        order.Place();

        _unitOfWork.OrderRepository.Add(order);
        _unitOfWork.CartRepository.Update(cart);

        await _unitOfWork.CommitAsync();

        return order.Id;
    }

    public async Task<IList<OrderInfo>> GetAllAsync()
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        return orders.Select(o => new OrderInfo(o.Id,
            new UserInfo(o.Customer.UserId, o.Customer.UserName),
            o.LastUpdate, o.Status))
            .ToList();

    }

    public async Task<OrderDetails> GetOrderDetailsAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.FindByIdAsync(orderId);

        var orderDetails = new OrderDetails
        {
            UserId = order.Customer.UserId,
            UserName = order.Customer.UserName,
            Status = order.Status,
            TotalAmount = order.TotalAmount.Value,
            Lines = order.Lines.Adapt<IList<OrderLineDto>>(),
        };

        return orderDetails;
    }

    public async Task ReadyForPickupAsync(Guid orderId)
    {
        // Validation 
        var order = await _unitOfWork.OrderRepository.FindByIdAsync(orderId);

        order.ReadyForPickup();

        await _unitOfWork.OrderRepository.UpdateAsync(order);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IList<OrderInfo>> GetAllByUserIdAsync(string userId)
    {
        var orders= await _unitOfWork.OrderRepository.GetAllByUserIdAsync(userId);
        return orders.Select(o => new OrderInfo(o.Id,
         new UserInfo(o.Customer.UserId, o.Customer.UserName),
         o.LastUpdate, o.Status))
         .ToList();
    }
}
