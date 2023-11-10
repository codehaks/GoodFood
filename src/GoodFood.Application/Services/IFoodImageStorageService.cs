
namespace GoodFood.Application.Services;

public interface IFoodImageStorageService
{
    Task StoreAsync(byte[] imageData, string fullFileName);
}
