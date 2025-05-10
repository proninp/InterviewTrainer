using InterviewTrainer.Application.Contracts.Tags;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface ITagService
{
    Task<Result<TagDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<TagDto>> GetPagedAsync(TagFilterDto tagFilterDto, CancellationToken cancellationToken);
    
    Task<Result<TagDto>> CreateAsync(CreateTagDto createTagDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateTagDto updateTagDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}