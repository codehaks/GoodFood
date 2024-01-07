using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Filters;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ValidateImageAttribute(string modelName) : Attribute, IPageFilter
{
    private static bool IsPostHandler(PageHandlerExecutingContext context)
    {
        return string.Equals(context?.HttpContext?.Request?.Method, "POST", StringComparison.OrdinalIgnoreCase);
    }
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        // No Action
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (!IsPostHandler(context))
        {
            return;
        }

        var imageFile = context.HttpContext.Request.Form.Files[modelName];
        if (imageFile == null || imageFile.Length == 0)
        {
            context.ModelState.AddModelError(modelName, "عکس خالی است");
            context.Result = new PageResult();
            return;
        }

        if (imageFile.Length > 1 * 1024 * 1024) //1MB
        {
            context.ModelState.AddModelError(modelName, "عکس بزرگ است");
            context.Result = new PageResult();
            return;
        }


    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        //No Action
    }

    public string ModelName { get; }
}
