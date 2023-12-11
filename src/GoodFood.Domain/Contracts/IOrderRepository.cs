using GoodFood.Domain.Entities;

namespace GoodFood.Domain.Contracts;
public interface IOrderRepository
{
    Task<Order> FindByIdAsync(Guid orderId);
    Task<IList<Order>> GetAllAsync();
    Task<IList<Order>> GetAllByUserIdAsync(string userId);
    void Place(Order order);
    Task UpdateAsync(Order order);
}
