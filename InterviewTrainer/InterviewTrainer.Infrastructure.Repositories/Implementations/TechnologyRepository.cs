using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Technologies;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class TechnologyRepository(DatabaseContext context) : BaseRepository<Technology>(context), ITechnologyRepository
{
    public override async Task<Technology?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }

        if (includeRelated)
        {
            query = query
                .Include(t => t.TopicTechnologies)
                .ThenInclude(tt => tt.Topic);
        }
        
        return await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> NameExistsAsync(string name, long? excludeTechnologyId, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (excludeTechnologyId.HasValue)
        {
            query = query.Where(t => t.Id != excludeTechnologyId.Value);
        }
        return await query
            .AnyAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Technology>> GetPagedAsync(TechnologyFilterDto filterDto,
        CancellationToken cancellationToken)
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