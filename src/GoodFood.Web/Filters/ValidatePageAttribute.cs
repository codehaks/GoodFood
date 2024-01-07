using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Filters;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ValidatePageAttribute : Attribute, IPageFilter
{
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new PageResult();
        }
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }
}
