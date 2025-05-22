using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Technologies;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<Technology> _technologies;

    public TechnologyRepository(DatabaseContext context)
    {
        _context = context;
        _technologies = _context.Set<Technology>();
    }

    public async Task<bool> AnyAsync(long id, CancellationToken cancellationToken)
    {
        return await _technologies
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Technology?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        if (!disableTracking && !includeRelated)
        {
            return await _technologies.FindAsync(id, cancellationToken);
        }
        
        IQueryable<Technology> query = _technologies;

        if (disableTracking)
            query = query.AsNoTracking();

        if (includeRelated)
            query = query.Include(t => t.TopicTechnologies);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Technology> AddAsync(Technology entity, CancellationToken cancellationToken)
    {
        var resultAdd = await _technologies.AddAsync(entity, cancellationToken);
        return resultAdd.Entity;
    }

    public async Task AddRangeAsync(ICollection<Technology> entities, CancellationToken cancellationToken)
    {
        var enumerable = entities as List<Technology> ?? entities.ToList();
        await _technologies.AddRangeAsync(enumerable, cancellationToken);
    }

    public void Update(Technology entity)
    {
        var resultUpdate = _technologies.Update(entity);
    }

    public void UpdatePartial(Technology entity, params Expression<Func<Technology, object>>[] properties)
    {
        _context.Attach(entity);
        foreach (var property in properties)
        {
            _context.Entry(entity).Property(property).IsModified = true;
        }
    }

    public void Delete(long id)
    {
        var entity = new Technology { Id = id };
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task<bool> NameExistsAsync(string name, long? excludeTechnologyId, CancellationToken cancellationToken)
    {
        return await _technologies
            .AsNoTracking()
            .AnyAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Technology>> GetPagedAsync(TechnologyFilterDto filterDto,
        CancellationToken cancellationToken)
    {
        var query = _technologies.AsQueryable();
        
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