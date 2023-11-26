using GoodFood.Application.Models;
using GoodFood.Application.Services;
using GoodFood.Domain.Values;

namespace GoodFood.Application.Contracts;

public interface ICartService
{
    [Obsolete("Should be replaced by FindByUserIdAsync")]
    Task<IList<CartLineModel>> GetByUserIdAsync(UserInfo userInfo);
    Task AddToCartAsync(CartLineAddModel model);

    Task Update(IList<CartLineModel> cartLines, UserInfo userInfo);
    Task<CartDto> FindByUserIdAsync(UserInfo userInfo);
}

public class CartDto
{
    public int Id { get; set; }
    public CustomerInfo Customer { get; set; } = default!;
    public List<CartLineModel> Lines { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TimeUpdated { get; set; }
}
