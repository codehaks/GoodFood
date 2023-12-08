

using GoodFood.Application.Contracts;

namespace GoodFood.Web.Common;

public class RemoveExpiredCartsWorker : BackgroundService
{
    private ILogger<RemoveExpiredCartsWorker> _logger;
    public IServiceProvider Services { get; }
    private readonly IConfiguration _configuration;


    public RemoveExpiredCartsWorker(ILogger<RemoveExpiredCartsWorker> logger, IServiceProvider services, IConfiguration configuration)
    {
        _logger = logger;
        Services = services;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var taskDelayInMiliSeconds = _configuration.GetValue<int?>("Workers:RemoveExpiredCartsWorkerTaskDelay") ?? 10_000;

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(taskDelayInMiliSeconds, stoppingToken);
            _logger.LogInformation("Checking carts.");

            await DoWorkAsync(stoppingToken);
        }
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = Services.CreateScope();
        var cartService =
            scope.ServiceProvider
                .GetRequiredService<ICartService>();

        await cartService.RemoveExpiredCartsAsync(stoppingToken);
    }
}
