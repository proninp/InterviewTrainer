using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<Topic> _topics;

    public TopicRepository(DatabaseContext context)
    {
        _context = context;
        _topics = context.Set<Topic>();
    }

    public async Task<bool> AnyAsync(long id, CancellationToken cancellationToken)
    {
        return await _topics
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Topic?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        if (!disableTracking && !includeRelated)
        {
            return await _topics.FindAsync(id, cancellationToken);
        }

        IQueryable<Topic> query = _topics;

        if (disableTracking)
            query = query.AsNoTracking();

        if (includeRelated)
            query = query.Include(t => t.TopicTechnologies);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Topic> AddAsync(Topic entity, CancellationToken cancellationToken)
    {
        var resultAdd = await _topics.AddAsync(entity, cancellationToken);
        return resultAdd.Entity;
    }

    public async Task AddRangeAsync(ICollection<Topic> entities, CancellationToken cancellationToken)
    {
        var enumerable = entities as List<Topic> ?? entities.ToList();
        await _topics.AddRangeAsync(enumerable, cancellationToken);
    }

    public void Update(Topic entity)
    {
        _ = _topics.Update(entity);
    }

    public void UpdatePartial(Topic entity, params Expression<Func<Topic, object>>[] properties)
    {
        _context.Attach(entity);
        foreach (var property in properties)
        {
            _context.Entry(entity).Property(property).IsModified = true;
        }
    }

    public void Delete(long id)
    {
        var entity = new Topic { Id = id };
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task<bool> ExistsByNameAsync(string name, long? excludeTopicId, CancellationToken cancellationToken)
    {
        return await _topics
            .AsNoTracking()
            .AnyAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Topic>> GetTopicsByTechnologyNameAsync(string technologyName,
        CancellationToken cancellationToken, bool archived = false)
    {
        var query = _topics.AsNoTrackingWithIdentityResolution();
        
        query = query
            .Where(t => t.Archived == archived)
            .Include(t => t.TopicTechnologies)
            .ThenInclude(tt => tt.Technology)
            .Where(t => t.TopicTechnologies.Any(tt => tt.Technology.Name == technologyName));
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = _topics.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterDto.Name))
        {
            query = query.Where(t => t.Name.Contains(filterDto.Name));
        }

        if (filterDto.Archived.HasValue)
        {
            query = query.Where(t => t.Archived == filterDto.Archived.Value);
        }

        query = query.AsNoTracking();

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }
}