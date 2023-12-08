using GoodFood.Application.Services;

namespace GoodFood.Application.Contracts;

public interface IMenuService
{
    IList<MenuLineDto> GetAll();
    Task AddLine(MenuLineCreateDto menuLine);
    IList<MenuLineDto> GetAllFoods(int categoryId);
}
