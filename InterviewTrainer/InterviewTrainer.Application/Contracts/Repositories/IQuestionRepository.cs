using InterviewTrainer.Application.DTOs.Questions;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    Task<IEnumerable<Question>> GetByTopicIdAsync(Guid topicId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Question>> GetArchivedAsync(CancellationToken cancellationToken);
    
    Task<IEnumerable<Topic>> GetPagedAsync(QuestionFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<IEnumerable<Question>> GetByTagsAsync(IEnumerable<QuestionTag> tags, CancellationToken cancellationToken);
}