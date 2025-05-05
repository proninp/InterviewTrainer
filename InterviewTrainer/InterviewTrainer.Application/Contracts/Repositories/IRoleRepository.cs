using InterviewTrainer.Application.DTOs.Roles;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> IsActiveRoleAsync(Guid roleId, CancellationToken cancellationToken);
    
    Task<bool> ExistsByNameAsync(string name, Guid? excludeRoleId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Role>> GetPagedAsync(RoleFilterDto filterDto, CancellationToken cancellationToken);
}