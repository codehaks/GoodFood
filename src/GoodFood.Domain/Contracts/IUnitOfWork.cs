using GoodFood.Infrastructure.Persistence.Repositories;

namespace GoodFood.Domain.Contracts;
public interface IUnitOfWork
{
    public IMenuRepository MenuRepository { get; }
    public IFoodRepository FoodRepository { get; }
    public ICartRepository CartRepository { get; }
    Task CommitAsync();
}
