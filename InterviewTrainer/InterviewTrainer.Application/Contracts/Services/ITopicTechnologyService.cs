using InterviewTrainer.Application.DTOs.Technologies;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ITopicTechnologyService
{
    Task<TechnologyDto?> AddTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
    
    Task<TechnologyDto?> RemoveTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
}