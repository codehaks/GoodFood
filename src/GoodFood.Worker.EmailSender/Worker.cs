using NetMQ;
using NetMQ.Sockets;

namespace GoodFood.Worker.EmailSender;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker Started");

        using var pullSocket = new PullSocket();
        pullSocket.Bind("tcp://127.0.0.1:5556"); // Bind to the PUSH socket
        while (!stoppingToken.IsCancellationRequested)
        {
            string msg = pullSocket.ReceiveFrameString();

            _logger.LogInformation("Worker running at: {time} -> {msg}", DateTimeOffset.Now.TimeOfDay, msg);
        }
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    if (_logger.IsEnabled(LogLevel.Information))
        //    {
        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //    }
        //    await Task.Delay(1000, stoppingToken);
        //}
    }
}
