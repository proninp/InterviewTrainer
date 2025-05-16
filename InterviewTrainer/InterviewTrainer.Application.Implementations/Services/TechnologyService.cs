using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Technologies;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

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

    public async Task<Result<TechnologyDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetAsync(id, cancellationToken);
        return technology is null
            ? Result.Fail<TechnologyDto>(ErrorsFactory.NotFound(nameof(technology), id))
            : Result.Ok(technology.ToDto());
    }

    public async Task<List<TechnologyDto>> GetPagedAsync(TechnologyFilterDto technologyFilterDto,
        CancellationToken cancellationToken)
    {
        var technologies = await _technologyRepository.GetPagedAsync(technologyFilterDto, cancellationToken);
        return technologies.Select(t => t.ToDto()).ToList();
    }

    public async Task<Result<TechnologyDto>> CreateAsync(CreateTechnologyDto createTechnologyDto,
        CancellationToken cancellationToken)
    {
        var checkResult =
            await CheckTechnologyIdentityPropertiesAsync(null, createTechnologyDto.Name, cancellationToken);
        if (checkResult.IsFailed)
            return Result.Fail<TechnologyDto>(checkResult.Errors);

        var technology = await _technologyRepository.AddAsync(createTechnologyDto.ToTechnology(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(technology.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateTechnologyDto updateTechnologyDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckTechnologyIdentityPropertiesAsync(updateTechnologyDto.Id, updateTechnologyDto.Name,
            cancellationToken);
        if (checkResult.IsFailed)
            return checkResult;

        var technology = await _technologyRepository.GetAsync(updateTechnologyDto.Id, cancellationToken);
        if (technology is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(technology), updateTechnologyDto.Id));

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

        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _technologyRepository.TryDeleteAsync(id, cancellationToken);
    }

    private async Task<Result> CheckTechnologyIdentityPropertiesAsync(long? excludeId, string? name,
        CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Fail(ErrorsFactory.Required(nameof(Technology), nameof(name)));
            }

            var isNameExists = await _technologyRepository.NameExistsAsync(name, excludeId, cancellationToken);
            if (isNameExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(Technology), nameof(name), name));
            }
        }
        return Result.Ok();
    }
}