using InterviewTrainer.Application.Contracts.Topics;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITopicService
{
    Task<TopicDto> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TopicDto>> GetPagedAsync(
        TopicFilterDto topicFilterDto, CancellationToken cancellationToken);
    
    Task<TopicDto> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}