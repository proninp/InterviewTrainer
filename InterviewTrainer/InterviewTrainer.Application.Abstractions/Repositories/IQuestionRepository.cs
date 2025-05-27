using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Questions;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    Task<IEnumerable<Question>> GetPagedAsync(QuestionFilterDto filterDto, CancellationToken cancellationToken);

    Task<IEnumerable<Question>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken,
        bool includeRelated = true,
        bool disableTracking = false);
}