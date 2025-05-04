using InterviewTrainer.Application.DTOs.Technologies;
using InterviewTrainer.Application.DTOs.Topics;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ITopicTechnologyService
{
    Task<List<TopicDto>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<TechnologyDto> AddTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
    
    Task<TechnologyDto> RemoveTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
}