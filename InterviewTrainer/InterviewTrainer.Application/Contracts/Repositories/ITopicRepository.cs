using InterviewTrainer.Application.DTOs.Topics;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITopicRepository : IRepository<Topic>
{
    Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<bool> ExistsByNameAsync(string name, Guid? excludeTopicId, CancellationToken cancellationToken);
}