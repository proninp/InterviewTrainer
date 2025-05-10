using InterviewTrainer.Application.Contracts.Topics;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicService
{
    Task<Result<TopicDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TopicDto>> GetPagedAsync(
        TopicFilterDto topicFilterDto, CancellationToken cancellationToken);
    
    Task<Result<TopicDto>> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}