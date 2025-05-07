using InterviewTrainer.Application.Contracts.SuggestedAnswers;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ISuggestedAnswerService
{
    Task<SuggestedAnswerDto> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<List<SuggestedAnswerDto>> GetPagedAsync(
        SuggestedAnswerFilterDto suggestedAnswerFilterDto, CancellationToken cancellationToken);

    Task<SuggestedAnswerDto> CreateAsync(CreateSuggestedAnswerDto createSuggestedAnswerDto,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateSuggestedAnswerDto updateSuggestedAnswerDto, CancellationToken cancellationToken);

    Task DeleteAsync(long id, CancellationToken cancellationToken);
}