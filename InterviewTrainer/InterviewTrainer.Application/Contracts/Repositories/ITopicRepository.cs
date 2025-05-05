using InterviewTrainer.Application.DTOs.Topics;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITopicRepository : IRepository<Topic>
{
    Task<bool> ExistsByNameAsync(string name, Guid? excludeTopicId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Topic>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken);
    
}