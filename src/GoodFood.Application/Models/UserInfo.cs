namespace GoodFood.Application.Models;

public record UserInfo
{
    public UserInfo(string userId, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(userName);

        UserId = userId;
        UserName = userName;
    }

    public string UserId { get; set; }
    public string UserName { get; set; }

}
