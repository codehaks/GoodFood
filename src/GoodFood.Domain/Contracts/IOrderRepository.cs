using GoodFood.Domain.Entities;

namespace GoodFood.Domain.Contracts;
public interface IOrderRepository
{
    Task Place(Order order);
    void Update(Order order);
}
