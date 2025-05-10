using InterviewTrainer.Application.Contracts.Roles;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IRoleService
{
    Task<Result<RoleDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<bool?> IsActiveRoleAsync(long id, CancellationToken cancellationToken);
    
    Task<List<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken);
    
    Task<Result<RoleDto>> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}