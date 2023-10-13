using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;

namespace GoodFood.Infrastructure.Persistence.Repositories;
public interface ICartRepository
{
    void Add(Cart cart);
    Cart? FindByCustomerId(CustomerInfo customer);
    void Update(Cart cart);
    void Remove(int cartId);
}
