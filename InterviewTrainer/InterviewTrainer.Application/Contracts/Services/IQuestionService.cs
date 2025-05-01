using InterviewTrainer.Application.DTOs.Questions;

namespace InterviewTrainer.Application.Contracts.Services;

public interface IQuestionService
{
    Task<QuestionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<QuestionDto>> GetPagedAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
    
    Task<List<QuestionDto>> GetByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken);
    
    Task<QuestionDto> CreateAsync(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}