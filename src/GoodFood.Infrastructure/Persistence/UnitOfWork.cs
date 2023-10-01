using GoodFood.Domain.Contracts;

namespace GoodFood.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly GoodFoodDbContext _db;
    public IMenuRepository MenuRepository { get; }
    public IFoodRepository FoodRepository { get; }

    public UnitOfWork(GoodFoodDbContext db, IMenuRepository menuRepository, IFoodRepository foodRepository)
    {
        _db = db;

        MenuRepository = menuRepository;
        FoodRepository = foodRepository;
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
    }
}
