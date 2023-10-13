namespace GoodFood.Domain.Values;

public record CustomerInfo
{
    public CustomerInfo(string userId, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(userName);

        UserId = userId;
        UserName = userName;
    }

    public string UserId { get; }
    public string UserName { get; }
}
