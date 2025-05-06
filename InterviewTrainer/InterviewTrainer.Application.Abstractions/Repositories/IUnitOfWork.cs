namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
}