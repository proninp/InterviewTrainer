using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Technologies;
using InterviewTrainer.Application.Exceptions;

namespace InterviewTrainer.Application.Implementations.Services;

public class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TechnologyService(ITechnologyRepository technologyRepository, IUnitOfWork unitOfWork)
    {
        _technologyRepository = technologyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TechnologyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetOrThrowAsync(id, cancellationToken);
        return technology.ToDto();
    }

    public async Task<List<TechnologyDto>> GetPagedAsync(TechnologyFilterDto technologyFilterDto,
        CancellationToken cancellationToken)
    {
        var technologies = await _technologyRepository.GetPagedAsync(technologyFilterDto, cancellationToken);
        return technologies.Select(t => t.ToDto()).ToList();
    }

    public async Task<TechnologyDto> CreateAsync(CreateTechnologyDto createTechnologyDto,
        CancellationToken cancellationToken)
    {
        await CheckTechnologyIdentityPropertiesAsync(null, createTechnologyDto.Name, cancellationToken);

        var technology = await _technologyRepository.AddAsync(createTechnologyDto.ToTechnology(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return technology.ToDto();
    }

    public async Task UpdateAsync(UpdateTechnologyDto updateTechnologyDto, CancellationToken cancellationToken)
    {
        await CheckTechnologyIdentityPropertiesAsync(updateTechnologyDto.Id, updateTechnologyDto.Name,
            cancellationToken);

        var technology = await _technologyRepository.GetOrThrowAsync(updateTechnologyDto.Id, cancellationToken);

        var isNeedToUpdate = false;

        if (updateTechnologyDto.Name is not null &&
            !string.Equals(technology.Name, updateTechnologyDto.Name, StringComparison.OrdinalIgnoreCase))
        {
            technology.Name = updateTechnologyDto.Name;
            isNeedToUpdate = true;
        }

        if (updateTechnologyDto.Archived is not null && technology.Archived != updateTechnologyDto.Archived.Value)
        {
            technology.Archived = updateTechnologyDto.Archived.Value;
            isNeedToUpdate = true;
        }

        if (isNeedToUpdate)
        {
            _technologyRepository.Update(technology);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetAsync(id, cancellationToken);
        if (technology is not null)
        {
            _technologyRepository.Delete(technology);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    private async Task CheckTechnologyIdentityPropertiesAsync(Guid? excludeId, string? name,
        CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            var isNameExists = await _technologyRepository.NameExistsAsync(name, excludeId, cancellationToken);
            if (isNameExists)
            {
                throw new BusinessRuleViolationException($"Technology with name '{name}' already exists.");
            }
        }
    }
}