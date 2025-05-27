using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Users;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class UserRepository(DatabaseContext context) : BaseRepository<User>(context), IUserRepository
{
    public override async Task<User?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);

        return await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetPagedAsync(UserFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (filterDto.TelegramId.HasValue)
        {
            query = query.Where(t => t.TelegramId == filterDto.TelegramId);
        }

        if (!string.IsNullOrWhiteSpace(filterDto.UserName))
        {
            query = query.Where(t =>
                t.UserName != null &&
                string.Equals(t.UserName, filterDto.UserName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filterDto.Email))
        {
            query = query.Where(t => (t.Email != null) && t.Email.Contains(filterDto.Email));
        }

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTrackingWithIdentityResolution();

        query = query
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur =>
                string.Equals(ur.Role.Name, roleName, StringComparison.OrdinalIgnoreCase)));
        return await query.ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetUsersWithEmptyEmail(CancellationToken cancellationToken,
        bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();
        
        if (disableTracking)
            query = query.AsNoTracking();
        
        if (includeRelated)
            query = query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);

        query = query
            .Where(u => string.IsNullOrEmpty(u.Email));
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTelegramIdAsync(long telegramId, long? excludeUserId,
        CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId);
        return await query
            .AnyAsync(t => t.TelegramId != null && t.TelegramId == telegramId, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeUserId, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId);
        return await query
            .AnyAsync(t => !string.IsNullOrEmpty(t.Email) && t.Email.Equals(email, StringComparison.OrdinalIgnoreCase),
                cancellationToken);
    }
}