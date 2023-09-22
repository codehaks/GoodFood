using GoodFood.Application.Services;

namespace GoodFood.Application.Contracts;

public interface IMenuService
{
    IList<MenuLineDto> GetAll();
    Task AddLine(MenuLineDto menuLine);
}