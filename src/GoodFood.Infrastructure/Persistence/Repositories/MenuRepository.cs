using GoodFood.Domain;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence.Repositories;
public class MenuRepository : IMenuRepository
{
    private readonly GoodFoodDbContext _db;

    public MenuRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public Menu GetMenu()
    {
        var lines = _db.MenuLines.Select(l => new MenuLine
        {
            Count = l.Count,
            FoodId=l.FoodId,
            Price=new Domain.Values.Money(l.Price)
        }).ToList();

        return new Menu(lines);
    }
}
