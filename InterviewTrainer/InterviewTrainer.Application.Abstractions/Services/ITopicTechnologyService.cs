using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Application.Contracts.Technologies;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicTechnologyService
{
    Task<List<TopicDto>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<TechnologyDto> AddTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
    
    Task<TechnologyDto> RemoveTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
}