using GoodFood.Application.Models;

namespace GoodFood.Application.Contracts;
public interface IOrderService
{
    Task ConfirmedAsync(Guid orderId);

    Task Place(UserInfo userInfo);
}
