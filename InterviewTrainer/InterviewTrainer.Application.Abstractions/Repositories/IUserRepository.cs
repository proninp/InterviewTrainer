using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Users;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetPagedAsync(UserFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName, CancellationToken cancellationToken);
    
    Task<IEnumerable<User>> GetUsersWithEmptyEmail(CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false);
    
    Task<bool> ExistsByTelegramIdAsync(long telegramId, long? excludeUserId, CancellationToken cancellationToken);
    
    Task<bool> ExistsByEmailAsync(string email, long? excludeUserId, CancellationToken cancellationToken);
}