using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Roles;
using InterviewTrainer.Application.Exceptions;

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

    public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetOrThrowAsync(id, cancellationToken);
        return role.ToDto();
    }

    public async Task<bool?> IsActiveRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _roleRepository.IsActiveRoleAsync(id, cancellationToken);
    }

    public async Task<List<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetPagedAsync(roleFilterDto, cancellationToken);
        return roles.Select(role => role.ToDto()).ToList();
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken)
    {
        await CheckRoleIdentityPropertiesAsync(null, createRoleDto.Name, cancellationToken);
        
        var role = await _roleRepository.AddAsync(createRoleDto.ToRole(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return role.ToDto();
    }

    public async Task UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
    {
        if (updateRoleDto.Name is not null && string.IsNullOrWhiteSpace(updateRoleDto.Name))
        {
            throw new BusinessRuleViolationException("Role name cannot be empty.");
        }
        
        await CheckRoleIdentityPropertiesAsync(updateRoleDto.Id, updateRoleDto.Name, cancellationToken);

        var isNeedUpdate = false;

        var role = await _roleRepository.GetOrThrowAsync(updateRoleDto.Id, cancellationToken);

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
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(id, cancellationToken);
        if (role is not null)
        {
            _roleRepository.Delete(role);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    private async Task CheckRoleIdentityPropertiesAsync(Guid? excludeId, string? name, CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            var isNameAlreadyExists = await _roleRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isNameAlreadyExists)
            {
                throw new BusinessRuleViolationException($"Role with name '{name}' already exists.");
            }
        }
    }
}