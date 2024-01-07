using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Filters;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ValidateFoodNameAttribute : Attribute, IAsyncPageFilter
{
    private readonly IFoodService _foodService;
    private readonly string _inputName;

    private static bool IsPostHandler(PageHandlerExecutingContext context)
    {
        return string.Equals(context?.HttpContext?.Request?.Method, "POST", StringComparison.OrdinalIgnoreCase);
    }

    public ValidateFoodNameAttribute(IFoodService foodService, string inputName)
    {
        _foodService = foodService;
        _inputName = inputName;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (IsPostHandler(context))
        {

            var foodName = context.HttpContext.Request.Form[_inputName];
            if (!string.IsNullOrEmpty(foodName))
            {
                var isDuplicated = await _foodService.IsDuplicatedNameAsync(foodName);
                if (isDuplicated)
                {
                    context.ModelState.AddModelError(_inputName, "نام غذا تکراری است");
                    context.Result = new PageResult();
                }
            }
        }
        await next();
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }
}
