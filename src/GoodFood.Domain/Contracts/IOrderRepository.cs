using GoodFood.Domain.Entities;

namespace GoodFood.Domain.Contracts;
public interface IOrderRepository
{
    Task<Order> FindByIdAsync(Guid orderId);
    Task<IList<Order>> GetAllAsync();
    Task Place(Order order);
    Task UpdateAsync(Order order);
}
