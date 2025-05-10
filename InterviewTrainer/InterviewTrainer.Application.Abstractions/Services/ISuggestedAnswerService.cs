using InterviewTrainer.Application.Contracts.SuggestedAnswers;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ISuggestedAnswerService
{
    Task<Result<SuggestedAnswerDto>> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<List<SuggestedAnswerDto>> GetPagedAsync(
        SuggestedAnswerFilterDto suggestedAnswerFilterDto, CancellationToken cancellationToken);

    Task<Result<SuggestedAnswerDto>> CreateAsync(CreateSuggestedAnswerDto createSuggestedAnswerDto,
        CancellationToken cancellationToken);

    Task<Result> UpdateAsync(UpdateSuggestedAnswerDto updateSuggestedAnswerDto, CancellationToken cancellationToken);

    Task DeleteAsync(long id, CancellationToken cancellationToken);
}