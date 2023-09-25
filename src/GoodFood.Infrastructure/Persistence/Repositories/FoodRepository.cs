using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
namespace GoodFood.Infrastructure.Persistence.Repositories;
public class FoodRepository : IFoodRepository
{
    private readonly GoodFoodDbContext _db;

    public FoodRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public IList<Food> GetAll()
    {
        return _db.Foods
            .ProjectToType<Food>()
            .ToList();
    }
}
