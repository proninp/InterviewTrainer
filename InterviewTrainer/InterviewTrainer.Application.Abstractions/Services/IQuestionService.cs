using FluentResults;
using InterviewTrainer.Application.Contracts.Questions;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IQuestionService
{
    Task<Result<QuestionDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<QuestionDto>> GetPagedAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
    
    Task<Result<QuestionDto>> GetRandomAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
    
    Task<Result<QuestionDto>> CreateAsync(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}