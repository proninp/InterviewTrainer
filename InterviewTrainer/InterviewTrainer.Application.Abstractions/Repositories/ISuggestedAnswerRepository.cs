using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.SuggestedAnswers;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface ISuggestedAnswerRepository : IRepository<SuggestedAnswer>
{
    Task<IEnumerable<SuggestedAnswer>> GetByQuestionIdAsync(long questionId, CancellationToken cancellationToken,
        bool includeRelated = true,
        bool disableTracking = false);

    Task<IEnumerable<SuggestedAnswer>> GetPagedAsync(
        SuggestedAnswerFilterDto filterDto, CancellationToken cancellationToken);
}