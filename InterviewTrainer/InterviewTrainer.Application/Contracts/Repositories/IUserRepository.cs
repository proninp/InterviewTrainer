using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> AddRoleAsync(Guid roleId, CancellationToken cancellationToken);
    
    Task<User> RemoveRoleAsync(Guid roleId, CancellationToken cancellationToken);
}