using GoodFood.Domain.Contracts;
using GoodFood.Infrastructure.Persistence.Repositories;

namespace GoodFood.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly GoodFoodDbContext _db;
    public IMenuRepository MenuRepository { get; }
    public IFoodRepository FoodRepository { get; }
    public ICartRepository CartRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public UnitOfWork(GoodFoodDbContext db, IMenuRepository menuRepository, IFoodRepository foodRepository, ICartRepository cartRepository, IOrderRepository orderRepository)
    {
        _db = db;

        MenuRepository = menuRepository;
        FoodRepository = foodRepository;
        CartRepository = cartRepository;
        OrderRepository = orderRepository;
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task CommitAsync(CancellationToken stoppingToken)
    {
        await _db.SaveChangesAsync(stoppingToken);
    }
}
