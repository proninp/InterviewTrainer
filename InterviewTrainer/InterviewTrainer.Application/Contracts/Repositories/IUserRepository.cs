using InterviewTrainer.Application.DTOs.Users;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetPagedAsync(UserFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<bool> ExistsByTelegramIdAsync(long telegramId, Guid? excludeUserId, CancellationToken cancellationToken);
    
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeUserId, CancellationToken cancellationToken);
}