using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Application.Contracts.Technologies;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicTechnologyService
{
    Task<List<TopicDto>> GetTopicsByTechnologyNameAsync(string technologyName, CancellationToken cancellationToken);
    
    Task<Result<TechnologyDto>> AddTopicAsync(long technologyId, long topicId, CancellationToken cancellationToken);
    
    Task<Result<TechnologyDto>> RemoveTopicAsync(long technologyId, long topicId, CancellationToken cancellationToken);
}