using GoodFood.Application.Models;

namespace GoodFood.Application.Contracts;
public interface IOrderService
{
    Task Place(UserInfo userInfo);
}
