using InterviewTrainer.Application.Contracts.Questions;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IQuestionService
{
    Task<QuestionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<QuestionDto>> GetPagedAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
    
    Task<QuestionDto> GetRandomAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
    
    Task<QuestionDto> CreateAsync(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}