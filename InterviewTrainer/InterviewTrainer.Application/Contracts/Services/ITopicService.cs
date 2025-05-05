using InterviewTrainer.Application.DTOs.Technologies;
using InterviewTrainer.Application.DTOs.Topics;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ITopicService
{
    Task<TopicDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<TopicDto>> GetPagedAsync(
        TopicFilterDto topicFilterDto, CancellationToken cancellationToken);
    
    Task<TopicDto> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}