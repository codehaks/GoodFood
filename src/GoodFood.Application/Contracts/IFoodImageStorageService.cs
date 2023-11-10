namespace GoodFood.Application.Contracts;

public interface IFoodImageStorageService
{
    Task StoreAsync(byte[] imageData, string fullFileName);
}
