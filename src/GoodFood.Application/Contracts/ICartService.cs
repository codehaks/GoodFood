using GoodFood.Application.Models;
using GoodFood.Application.Services;

namespace GoodFood.Application.Contracts;

public interface ICartService
{
    Task<IList<CartLineModel>> GetByUserIdAsync(UserInfo userInfo);
    Task AddToCartAsync(CartLineAddModel model);

    Task Update(IList<CartLineModel> cartLines, UserInfo userInfo);
}
