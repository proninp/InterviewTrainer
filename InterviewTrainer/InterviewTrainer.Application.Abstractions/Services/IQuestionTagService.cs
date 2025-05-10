using InterviewTrainer.Application.Contracts.Questions;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IQuestionTagService
{
    Task<List<QuestionDto>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken);
    
    Task<Result<QuestionDto>> AddTagToQuestionAsync(long questionId, long tagId, CancellationToken cancellationToken);
    
    Task<Result<QuestionDto>> RemoveTagFromQuestionAsync(long questionId, long tagId, CancellationToken cancellationToken);
}