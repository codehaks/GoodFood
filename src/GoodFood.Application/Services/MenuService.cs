using GoodFood.Application.Contracts;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Application.Services;
public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public IList<MenuLineDto> GetAll()
    {
        var menu = _menuRepository.GetMenu();
        return menu.Lines.Select(l => new MenuLineDto
        {
            Count = l.Count,
            FoodId = l.FoodId,
            Price = l.Price.Value
        }).ToList();
    }
}

public class MenuLineDto
{
    public int FoodId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}