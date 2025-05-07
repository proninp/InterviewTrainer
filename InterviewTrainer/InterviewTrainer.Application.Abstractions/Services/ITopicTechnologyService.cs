using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Application.Contracts.Technologies;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicTechnologyService
{
    Task<List<TopicDto>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<TechnologyDto> AddTopicAsync(long technologyId, long topicId, CancellationToken cancellationToken);
    
    Task<TechnologyDto> RemoveTopicAsync(long technologyId, long topicId, CancellationToken cancellationToken);
}