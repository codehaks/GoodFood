using GoodFood.Application.Contracts;

namespace GoodFood.Infrastructure.Services;
public class FoodImageStorageService : IFoodImageStorageService
{
    public async Task StoreAsync(byte[] imageData, string fullFileName)
    {
        try
        {
            var directory = Path.GetDirectoryName(fullFileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllBytesAsync(fullFileName, imageData);
        }
        catch (Exception ex)
        {
            // Handle the exception (log or throw, depending on your needs)
            throw new InvalidOperationException($"Failed to store image: {ex.Message}", ex);
        }
    }
}
