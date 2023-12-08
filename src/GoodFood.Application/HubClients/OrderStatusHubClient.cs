namespace GoodFood.Application.HubClients;
public interface IOrderStatusHubClient
{
    Task UpdateOrderStatus(string orderStatus);
}
