using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GoodFood.Web.Common;

public class EmailSenderWorker : BackgroundService
{
    private readonly IEmailQueueService _emailQueueService;
    private readonly ILogger<EmailSenderWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    public EmailSenderWorker(IEmailQueueService emailQueueService, ILogger<EmailSenderWorker> logger, IServiceProvider serviceProvider)
    {
        _emailQueueService = emailQueueService;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("{Name} Started", nameof(EmailSenderWorker));
            await DoWork(stoppingToken);
            _logger.LogInformation("{Name} Stopped", nameof(EmailSenderWorker));
            await Task.Delay(3000, stoppingToken);
        }
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var EmailSender =
            scope.ServiceProvider
                .GetRequiredService<IEmailSender>();

        if (_emailQueueService.AnyQueue())
        {
            var taskList = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                var result = _emailQueueService.TryDequeue(out var emailJob);
                if (!result || emailJob == null)
                {
                    break;
                }
                _logger.LogInformation("Email Job dequeued: {Id}", emailJob.JobId);
                var sendEmailTask = EmailSender.SendEmailAsync(emailJob.EmailAddress, emailJob.EmailTitle, emailJob.EmailBody);
                taskList.Add(sendEmailTask);

            }
            await Task.WhenAll(taskList);
        }
    }
}
