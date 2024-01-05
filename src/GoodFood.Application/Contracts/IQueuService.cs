namespace GoodFood.Application.Contracts;
public interface IEmailQueueService
{
    void Enqueue(EmailJobDto item);
    bool TryDequeue(out EmailJobDto item);

    bool AnyQueue();
}

public class EmailJobDto
{
    public EmailJobDto()
    {
        JobId = Guid.NewGuid();
    }
    public Guid JobId { get; }
    public required string EmailAddress { get; init; }
    public required string EmailTitle { get; init; }
    public required string EmailBody { get; init; }

}
