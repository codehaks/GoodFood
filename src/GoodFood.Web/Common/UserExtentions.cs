using System.Security.Claims;
using GoodFood.Application.Models;

namespace GoodFood.Web.Common;

public static class UserExtentions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user is not null && user.Identity?.IsAuthenticated == true)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        return string.Empty;

    }

    public static UserInfo GetUserInfo(this ClaimsPrincipal user)
    {
        if (user?.Identity!.IsAuthenticated == true)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.Identity.Name;
            ArgumentException.ThrowIfNullOrEmpty(userName);
            ArgumentException.ThrowIfNullOrEmpty(userId);

            return new UserInfo(userId, userName);
        }
        else
        {
            throw new InvalidOperationException("User not authenticated");
        }

    }

}
