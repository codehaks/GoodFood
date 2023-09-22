using GoodFood.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly GoodFoodDbContext _db;
    public IMenuRepository MenuRepository { get; }

    public UnitOfWork(GoodFoodDbContext db,IMenuRepository menuRepository)
    {
        MenuRepository = menuRepository;
        _db = db;
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
    }
}
