using InterviewTrainer.Application.Contracts.Tags;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITagService
{
    Task<TagDto> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TagDto>> GetPagedAsync(TagFilterDto tagFilterDto, CancellationToken cancellationToken);
    
    Task<TagDto> CreateAsync(CreateTagDto createTagDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateTagDto updateTagDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}