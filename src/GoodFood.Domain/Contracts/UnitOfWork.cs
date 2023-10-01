namespace GoodFood.Domain.Contracts;
public interface IUnitOfWork
{
    public IMenuRepository MenuRepository { get; }
    public IFoodRepository FoodRepository { get; }
    Task CommitAsync();
}
