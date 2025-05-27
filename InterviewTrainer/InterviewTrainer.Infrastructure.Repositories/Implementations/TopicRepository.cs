using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class TopicRepository(DatabaseContext context) : BaseRepository<Topic>(context), ITopicRepository
{
    public override async Task<Topic?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();
        
        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(t => t.TopicTechnologies)
                .ThenInclude(tt => tt.Technology);

        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, long? excludeTopicId, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (excludeTopicId.HasValue)
        {
            query = query.Where(q => q.Id != excludeTopicId.Value);
        }
        return await query
            .AnyAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Topic>> GetTopicsByTechnologyNameAsync(string technologyName,
        CancellationToken cancellationToken, bool archived = false)
    {
        var query = Entities.AsNoTrackingWithIdentityResolution();
        
        query = query
            .Where(t => t.Archived == archived)
            .Include(t => t.TopicTechnologies)
            .ThenInclude(tt => tt.Technology)
            .Where(t => t.TopicTechnologies.Any(tt => tt.Technology.Name == technologyName));
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = Entities.AsQueryable();

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