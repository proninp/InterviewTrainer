﻿using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Roles;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(id, cancellationToken, asNoTracking: true);
        return role is null
            ? Result.Fail<RoleDto>(ErrorsFactory.NotFound(nameof(role), id))
            : Result.Ok(role.ToDto());
    }

    public async Task<bool?> IsActiveRoleAsync(long id, CancellationToken cancellationToken)
    {
        return await _roleRepository.IsActiveRoleAsync(id, cancellationToken);
    }

    public async Task<List<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetPagedAsync(roleFilterDto, cancellationToken);
        return roles.Select(role => role.ToDto()).ToList();
    }

    public async Task<Result<RoleDto>> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckRoleIdentityPropertiesAsync(null, createRoleDto.Name, cancellationToken);
        if (checkResult.IsFailed)
        {
            return Result.Fail<RoleDto>(checkResult.Errors);
        }
        
        var role = await _roleRepository.AddAsync(createRoleDto.ToRole(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(role.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(updateRoleDto.Id, cancellationToken);
        if (role is null)
        {
            return Result.Fail(ErrorsFactory.NotFound(nameof(role), updateRoleDto.Id));
        }
        
        var checkResult = await CheckRoleIdentityPropertiesAsync(updateRoleDto.Id, updateRoleDto.Name, cancellationToken);
        if (checkResult.IsFailed)
        {
            return checkResult;
        }

        var isNeedUpdate = false;


        if (updateRoleDto.Name is not null &&
            !string.Equals(role.Name, updateRoleDto.Name, StringComparison.OrdinalIgnoreCase))
        {
            role.Name = updateRoleDto.Name;
            isNeedUpdate = true;
        }

        if (!string.Equals(role.Description, updateRoleDto.Description, StringComparison.Ordinal))
        {
            role.Description = updateRoleDto.Description;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _roleRepository.Update(role);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        
        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _roleRepository.TryDeleteAsync(id, cancellationToken);
    }

    private async Task<Result> CheckRoleIdentityPropertiesAsync(long? excludeId, string? name, CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Fail(ErrorsFactory.Required(nameof(Role), nameof(name)));
            }
            
            var isNameAlreadyExists = await _roleRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isNameAlreadyExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(Role), nameof(name), name));
            }
        }

        return Result.Ok();
    }
}