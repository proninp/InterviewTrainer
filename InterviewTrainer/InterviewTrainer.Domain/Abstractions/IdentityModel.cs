namespace InterviewTrainer.Domain.Abstractions;

public abstract class IdentityModel
{
    public long Id { get; init; }
    
    public DateTime CreatedAt { get; init; }
}