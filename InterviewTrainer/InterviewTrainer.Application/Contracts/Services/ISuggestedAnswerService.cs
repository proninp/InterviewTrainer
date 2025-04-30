using InterviewTrainer.Application.DTOs.SuggestedAnswers;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ISuggestedAnswerService
{
    Task<SuggestedAnswerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<SuggestedAnswerDto>> GetPagedAsync(
        SuggestedAnswerFilterDto suggestedAnswerFilterDto, CancellationToken cancellationToken);
    
    Task<SuggestedAnswerDto> CreateAsync(CreateSuggestedAnswerDto createSuggestedAnswerDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateSuggestedAnswerDto updateSuggestedAnswerDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}