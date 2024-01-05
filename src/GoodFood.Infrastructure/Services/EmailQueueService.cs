using System.Collections.Concurrent;
using GoodFood.Application.Contracts;

namespace GoodFood.Infrastructure.Services;
public class EmailQueueService : IEmailQueueService
{
    private readonly ConcurrentQueue<EmailJobDto> EmailQueue = new ConcurrentQueue<EmailJobDto>();

    public bool AnyQueue() => !EmailQueue.IsEmpty;

    public void Enqueue(EmailJobDto item)
    {
        EmailQueue.Enqueue(item);
    }

    public bool TryDequeue(out EmailJobDto item)
    {
        return EmailQueue.TryDequeue(out item);
    }
}
