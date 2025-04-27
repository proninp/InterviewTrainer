using InterviewTrainer.Application.DTOs.Roles;

namespace InterviewTrainer.Application.Contracts.Services;

public interface IRoleService
{
    Task<RoleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool?> IsActiveRoleAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken);
    
    Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}