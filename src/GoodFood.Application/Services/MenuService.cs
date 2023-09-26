using GoodFood.Application.Contracts;
using GoodFood.Domain;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoodFood.Application.Services;
public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddLine(MenuLineDto menuLine)
    {

        var line = new MenuLine
        {
            Count = menuLine.Count,
            FoodId = menuLine.FoodId,
            Price = new Domain.Values.Money(menuLine.Price)
        };

        var menu = _menuRepository.GetMenu();
        menu.Add(line);

        _unitOfWork.MenuRepository.Update(menu);
        await _unitOfWork.CommitAsync();


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

public record MyMenuLineDto(int FoodId,int Count,decimal Price);

public class MenuLineDto
{
    public required int FoodId { get; init; }
    public int Count { get; init; }
    public decimal Price { get; init; }
}
