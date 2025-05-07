using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Roles;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> IsActiveRoleAsync(long roleId, CancellationToken cancellationToken);
    
    Task<bool> ExistsByNameAsync(string name, long? excludeRoleId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Role>> GetPagedAsync(RoleFilterDto filterDto, CancellationToken cancellationToken);
}