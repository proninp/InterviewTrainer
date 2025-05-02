using InterviewTrainer.Application.DTOs.Technologies;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ITechnologyService
{
    Task<TechnologyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<TechnologyDto>> GetPagedAsync(
        TechnologyFilterDto technologyFilterDto, CancellationToken cancellationToken);
    
    Task<TechnologyDto> CreateAsync(CreateTechnologyDto createTechnologyDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTechnologyDto updateTechnologyDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    Task<TechnologyDto?> AddTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
    
    Task<TechnologyDto?> RemoveTopicAsync(Guid technologyId, Guid topicId, CancellationToken cancellationToken);
}