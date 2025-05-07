using InterviewTrainer.Application.Contracts.Technologies;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITechnologyService
{
    Task<TechnologyDto> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TechnologyDto>> GetPagedAsync(
        TechnologyFilterDto technologyFilterDto, CancellationToken cancellationToken);
    
    Task<TechnologyDto> CreateAsync(CreateTechnologyDto createTechnologyDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTechnologyDto updateTechnologyDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}