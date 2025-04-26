using InterviewTrainer.Application.DTOs.Questions;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    Task<IEnumerable<Question>> GetPagedAsync(QuestionFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<IEnumerable<Question>> GetByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken);
}