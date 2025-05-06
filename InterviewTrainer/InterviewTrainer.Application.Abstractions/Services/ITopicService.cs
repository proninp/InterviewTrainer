using InterviewTrainer.Application.Contracts.Topics;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicService
{
    Task<TopicDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<TopicDto>> GetPagedAsync(
        TopicFilterDto topicFilterDto, CancellationToken cancellationToken);
    
    Task<TopicDto> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}