using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Users;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
        _users = context.Set<User>();
    }

    public async Task<bool> AnyAsync(long id, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<User?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        if (!disableTracking && !includeRelated)
        {
            return await _users.FindAsync(id, cancellationToken);
        }

        IQueryable<User> query = _users;

        if (disableTracking)
            query = query.AsNoTracking();

        if (includeRelated)
            query = query.Include(t => t.UserRoles);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<User> AddAsync(User entity, CancellationToken cancellationToken)
    {
        var resultAdd = await _users.AddAsync(entity, cancellationToken);
        return resultAdd.Entity;
    }

    public async Task AddRangeAsync(ICollection<User> entities, CancellationToken cancellationToken)
    {
        var enumerable = entities as List<User> ?? entities.ToList();
        await _users.AddRangeAsync(enumerable, cancellationToken);
    }

    public void Update(User entity)
    {
        _ = _users.Update(entity);
    }

    public void UpdatePartial(User entity, params Expression<Func<User, object>>[] properties)
    {
        _context.Attach(entity);
        foreach (var property in properties)
        {
            _context.Entry(entity).Property(property).IsModified = true;
        }
    }

    public void Delete(long id)
    {
        var entity = new User { Id = id };
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task<IEnumerable<User>> GetPagedAsync(UserFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = _users.AsQueryable();

        if (filterDto.TelegramId is not null)
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

        query = query.AsNoTracking();

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var query = _users.AsNoTrackingWithIdentityResolution();

        query = query
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur => string.Equals(ur.Role.Name, roleName, StringComparison.OrdinalIgnoreCase)));
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTelegramIdAsync(long telegramId, long? excludeUserId,
        CancellationToken cancellationToken)
    {
        var query = _users.AsNoTracking();
        if (excludeUserId is not null)
            query = query.Where(u => u.Id != excludeUserId);
        return await query
            .AnyAsync(t => t.TelegramId != null && t.TelegramId == telegramId, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeUserId, CancellationToken cancellationToken)
    {
        var query = _users.AsNoTracking();
        if (excludeUserId is not null)
            query = query.Where(u => u.Id != excludeUserId);
        return await query
            .AnyAsync(t => t.Email != null && t.Email.Equals(email, StringComparison.OrdinalIgnoreCase),
                cancellationToken);
    }
}