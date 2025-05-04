using InterviewTrainer.Application.DTOs.Questions;
using InterviewTrainer.Application.DTOs.Tags;

namespace InterviewTrainer.Application.Contracts.Services;

public interface ITagService
{
    Task<TagDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<TagDto>> GetPagedAsync(TagFilterDto tagFilterDto, CancellationToken cancellationToken);
    
    Task<TagDto> CreateAsync(CreateTagDto createTagDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTagDto updateTagDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}