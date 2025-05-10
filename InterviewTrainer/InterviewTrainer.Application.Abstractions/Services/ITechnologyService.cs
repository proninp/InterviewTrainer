using InterviewTrainer.Application.Contracts.Technologies;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITechnologyService
{
    Task<Result<TechnologyDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TechnologyDto>> GetPagedAsync(
        TechnologyFilterDto technologyFilterDto, CancellationToken cancellationToken);
    
    Task<Result<TechnologyDto>> CreateAsync(CreateTechnologyDto createTechnologyDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateTechnologyDto updateTechnologyDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}