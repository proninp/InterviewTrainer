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
        var filterDto = new RoleFilterDto(0, 0, Name: createRoleDto.Name);
        var roles = await _roleRepository.GetPagedAsync(filterDto, cancellationToken);
        if (roles.Any())
        {
            throw new EntityAlreadyExistsException("Role with the same name already exists.");
        }

        var role = createRoleDto.ToRole();
        role = await _roleRepository.AddAsync(role, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return role.ToDto();
    }

    public async Task UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
    {
        if (updateRoleDto.Name is not null && string.IsNullOrWhiteSpace(updateRoleDto.Name))
        {
            throw new BusinessRuleViolationException("Role name cannot be empty.");
        }

        var isNeedUpdate = false;

        var role = await _roleRepository.GetOrThrowAsync(updateRoleDto.Id, cancellationToken);

        if (updateRoleDto.Name is not null &&
            !string.Equals(role.Name, updateRoleDto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var isNameAlreadyExists =
                await _roleRepository.ExistsByNameAsync(updateRoleDto.Name, null, cancellationToken);
            if (isNameAlreadyExists)
            {
                throw new BusinessRuleViolationException($"Role with name '{updateRoleDto.Name}' already exists.");
            }

            role.Name = updateRoleDto.Name;
            isNeedUpdate = true;
        }

        if (!string.Equals(role.Name, updateRoleDto.Name, StringComparison.Ordinal))
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
        if (role is null)
        {
            return;
        }

        _roleRepository.Delete(role);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}