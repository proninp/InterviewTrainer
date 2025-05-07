using InterviewTrainer.Application.Contracts.Questions;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IQuestionTagService
{
    Task<List<QuestionDto>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken);
    
    Task<QuestionDto> AddTagToQuestionAsync(long questionId, long tagId, CancellationToken cancellationToken);
    
    Task<QuestionDto> RemoveTagFromQuestionAsync(long questionId, long tagId, CancellationToken cancellationToken);
}