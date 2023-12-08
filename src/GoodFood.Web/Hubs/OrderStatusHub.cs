using GoodFood.Application.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace GoodFood.Web.Hubs;

public class OrderStatusHub : Hub<IOrderStatusHubClient>
{
}
