using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Roles;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class RoleRepository(DatabaseContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public override async Task<Role?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true, bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Role>> GetPagedAsync(RoleFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(filterDto.Name))
        {
            query = query.Where(r =>
                string.Equals(r.Name, filterDto.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filterDto.Description))
        {
            query = query.Where(r => (r.Description != null) && r.Description.Contains(filterDto.Description));
        }

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> IsActiveRoleAsync(long roleId, CancellationToken cancellationToken)
    {
        return await Entities.AsNoTracking()
            .Where(r => r.Id == roleId && r.UserRoles.Any())
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, long? excludeRoleId, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();
        if (excludeRoleId.HasValue)
            query = query.Where(r => r.Id != excludeRoleId.Value);
        return await query
            .AnyAsync(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase),
                cancellationToken);
    }
}