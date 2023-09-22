using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

namespace GoodFood.Web.Areas.Admin.Pages.Menus;

public class IndexModel : PageModel
{
    private readonly IMenuService _menuService;

    public IndexModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public IList<MenuLineDto> MenuLines { get; set; }
    public void OnGet()
    {
        MenuLines = _menuService.GetAll();
    }
}
