using InterviewTrainer.Application.Contracts.Roles;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IRoleService
{
    Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool?> IsActiveRoleAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken);
    
    Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}