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

    public void Update(Menu menu)
    {
        var lines = _db.MenuLines;
        foreach (var line in lines)
        {
            lines.Remove(line);
        }

        foreach (var line in menu.Lines)
        {
            _db.MenuLines.Add(new Models.MenuLineData
            {
                Count = line.Count,
                FoodId = line.FoodId,
                Price = line.Price.Value
            });
        }
    }
}
