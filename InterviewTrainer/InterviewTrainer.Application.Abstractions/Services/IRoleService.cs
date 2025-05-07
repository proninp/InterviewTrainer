using InterviewTrainer.Application.Contracts.Roles;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IRoleService
{
    Task<RoleDto> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<bool?> IsActiveRoleAsync(long id, CancellationToken cancellationToken);
    
    Task<List<RoleDto>> GetPagedAsync(RoleFilterDto roleFilterDto, CancellationToken cancellationToken);
    
    Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}