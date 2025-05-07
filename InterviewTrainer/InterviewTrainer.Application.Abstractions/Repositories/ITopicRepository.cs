using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Topics;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface ITopicRepository : IRepository<Topic>
{
    Task<bool> ExistsByNameAsync(string name, long? excludeTopicId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Topic>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken);
    
}