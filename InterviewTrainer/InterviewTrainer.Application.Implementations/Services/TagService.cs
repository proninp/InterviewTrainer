using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Tags;
using InterviewTrainer.Application.Implementations.Exceptions;

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

    public async Task<TagDto> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetOrThrowAsync(id, cancellationToken);
        return tag.ToDto();
    }

    public async Task<List<TagDto>> GetPagedAsync(TagFilterDto tagFilterDto, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetPagedAsync(tagFilterDto, cancellationToken);
        return tags.Select(tag => tag.ToDto()).ToList();
    }

    public async Task<TagDto> CreateAsync(CreateTagDto createTagDto, CancellationToken cancellationToken)
    {
        await CheckTagIdentityPropertiesAsync(null, createTagDto.Name, cancellationToken);
        
        var tag = await _tagRepository.AddAsync(createTagDto.ToTag(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return tag.ToDto();
    }

    public async Task UpdateAsync(UpdateTagDto updateTagDto, CancellationToken cancellationToken)
    {
        if (updateTagDto.Name is not null && string.IsNullOrWhiteSpace(updateTagDto.Name))
        {
            throw new BusinessRuleViolationException("Tag name cannot be empty.");
        }
        
        await CheckTagIdentityPropertiesAsync(updateTagDto.Id, updateTagDto.Name, cancellationToken);

        var isNeedUpdate = false;

        var tag = await _tagRepository.GetOrThrowAsync(updateTagDto.Id, cancellationToken);

        if (updateTagDto.Name is not null &&
            !string.Equals(tag.Name, updateTagDto.Name, StringComparison.OrdinalIgnoreCase))
        {
            tag.Name = updateTagDto.Name;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetAsync(id, cancellationToken);
        if (tag is not null)
        {
            _tagRepository.Delete(tag);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
    
    private async Task CheckTagIdentityPropertiesAsync(long? excludeId, string? name, CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            var isTagAlreadyExists =
                await _tagRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isTagAlreadyExists)
            {
                throw new BusinessRuleViolationException(
                    $"Tag with Name '{name}' already exists");
            }
        }
    }
}