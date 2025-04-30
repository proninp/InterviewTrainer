using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> IsActiveRoleAsync(Guid roleId, CancellationToken cancellationToken);
}