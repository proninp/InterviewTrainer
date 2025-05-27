using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Tags;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class TagRepository : ITagRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<Tag> _tags;

    public TagRepository(DatabaseContext context)
    {
        _context = context;
        _tags = context.Set<Tag>();
    }

    public async Task<bool> AnyAsync(long id, CancellationToken cancellationToken)
    {
        return await _tags
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Tag?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = _tags.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Tag> AddAsync(Tag entity, CancellationToken cancellationToken)
    {
        var resultAdd = await _tags.AddAsync(entity, cancellationToken);
        return resultAdd.Entity;
    }

    public async Task AddRangeAsync(ICollection<Tag> entities, CancellationToken cancellationToken)
    {
        var enumerable = entities as List<Tag> ?? entities.ToList();
        await _tags.AddRangeAsync(enumerable, cancellationToken);
    }

    public void Update(Tag entity)
    {
        _ = _tags.Update(entity);
    }

    public void UpdatePartial(Tag entity, params Expression<Func<Tag, object>>[] properties)
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

    public async Task<IEnumerable<Tag>> GetPagedAsync(TagFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = _tags.AsNoTracking();

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
        var query = _tags.AsNoTracking();
        if (excludeTagId.HasValue)
            query = query.Where(t => t.Id != excludeTagId);
        return await query
            .AnyAsync(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase),
                cancellationToken);
    }
}