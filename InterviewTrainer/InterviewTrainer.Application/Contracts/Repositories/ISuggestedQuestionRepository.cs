using InterviewTrainer.Application.DTOs.SuggestedQuestions;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ISuggestedQuestionRepository : IRepository<SuggestedQuestion>
{
    Task<IEnumerable<Topic>> GetPagedAsync(SuggestedQuestionFilterDto filterDto, CancellationToken cancellationToken);
}