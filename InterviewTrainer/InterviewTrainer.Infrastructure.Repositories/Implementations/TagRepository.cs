using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Tags;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext context) : base(context)
    {
    }

    public override async Task<Tag?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Tag>> GetPagedAsync(TagFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (!string.IsNullOrEmpty(filterDto.Name))
        {
            query = query.Where(t => string.Equals(t.Name, filterDto.Name, StringComparison.OrdinalIgnoreCase));
        }

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, long? excludeTagId, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();
        if (excludeTagId.HasValue)
            query = query.Where(t => t.Id != excludeTagId);
        return await query
            .AnyAsync(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase),
                cancellationToken);
    }
}