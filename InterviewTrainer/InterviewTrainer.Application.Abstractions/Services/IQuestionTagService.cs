using InterviewTrainer.Application.Contracts.Questions;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IQuestionTagService
{
    Task<List<QuestionDto>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken);
    
    Task<QuestionDto> AddTagToQuestionAsync(Guid questionId, Guid tagId, CancellationToken cancellationToken);
    
    Task<QuestionDto> RemoveTagFromQuestionAsync(Guid questionId, Guid tagId, CancellationToken cancellationToken);
}