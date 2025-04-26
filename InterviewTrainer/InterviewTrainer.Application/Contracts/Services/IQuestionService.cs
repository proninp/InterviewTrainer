using InterviewTrainer.Application.DTOs.Questions;

namespace InterviewTrainer.Application.Contracts.Services;

public interface IQuestionService
{
    Task<QuestionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<QuestionDto>> GetByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken);
    
    Task<int> CreateAsync(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<QuestionDto>> GetPagedAsync(
        QuestionFilterDto questionFilterDto, CancellationToken cancellationToken);
}