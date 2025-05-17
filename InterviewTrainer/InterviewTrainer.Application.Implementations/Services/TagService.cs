using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Tags;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TagDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetAsync(id, cancellationToken, asNoTracking: true);
        return tag is null 
            ? Result.Fail<TagDto>(ErrorsFactory.NotFound(nameof(tag), id)) 
            : Result.Ok(tag.ToDto());
    }

    public async Task<List<TagDto>> GetPagedAsync(TagFilterDto tagFilterDto, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetPagedAsync(tagFilterDto, cancellationToken);
        return tags.Select(tag => tag.ToDto()).ToList();
    }

    public async Task<Result<TagDto>> CreateAsync(CreateTagDto createTagDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckTagIdentityPropertiesAsync(null, createTagDto.Name, cancellationToken);
        if (checkResult.IsFailed)
            return Result.Fail<TagDto>(checkResult.Errors);
        
        var tag = await _tagRepository.AddAsync(createTagDto.ToTag(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(tag.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateTagDto updateTagDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckTagIdentityPropertiesAsync(updateTagDto.Id, updateTagDto.Name, cancellationToken);
        if (checkResult.IsFailed)
            return checkResult;

        var isNeedUpdate = false;

        var tag = await _tagRepository.GetAsync(updateTagDto.Id, cancellationToken);
        if (tag is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(tag), updateTagDto.Id));

        if (!string.Equals(tag.Name, updateTagDto.Name, StringComparison.OrdinalIgnoreCase))
        {
            tag.Name = updateTagDto.Name;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _tagRepository.TryDeleteAsync(id, cancellationToken);
    }
    
    private async Task<Result> CheckTagIdentityPropertiesAsync(long? excludeId, string? name, CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail(ErrorsFactory.Required(nameof(Tag), nameof(name)));
            
            var isTagAlreadyExists =
                await _tagRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isTagAlreadyExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(Tag), nameof(name), name));
            }
        }
        return Result.Ok();
    }
}