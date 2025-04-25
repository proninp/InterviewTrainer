using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<IEnumerable<UserRole>> GetByUserAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<IEnumerable<UserRole>> GetByRoleAsync(Guid roleId, CancellationToken cancellationToken);
    
    Task<UserRole?> GetByUserAndRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}