using InterviewTrainer.Application.DTOs.SuggestedAnswers;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ISuggestedAnswerRepository : IRepository<SuggestedAnswer>
{
    Task<IEnumerable<SuggestedAnswer>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<IEnumerable<SuggestedAnswer>> GetPagedAsync(
        SuggestedAnswerFilterDto filterDto, CancellationToken cancellationToken);
}