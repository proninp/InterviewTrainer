using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.Questions;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class QuestionRepository(DatabaseContext context) : BaseRepository<Question>(context), IQuestionRepository 
{
    public override async Task<Question?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true, bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(q => q.Topic)
                .Include(q => q.Author)
                .Include(q => q.QuestionTags)
                .ThenInclude(qt => qt.Tag)
                .AsSplitQuery();

        return await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Question>> GetPagedAsync(QuestionFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (filterDto.TopicId.HasValue)
        {
            query = query.Where(q => q.TopicId == filterDto.TopicId.Value);
        }
        
        if (filterDto.AuthorId.HasValue)
        {
            query = query.Where(q => q.AuthorId == filterDto.AuthorId.Value);
        }
        
        if (filterDto.Difficulty.HasValue)
        {
            query = query.Where(q => q.Difficulty == filterDto.Difficulty.Value);
        }
        
        if (filterDto.Status.HasValue)
        {
            query = query.Where(q => q.Status == filterDto.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(filterDto.Text))
        {
            query = query.Where(q =>
                q.Text.Contains(filterDto.Text, StringComparison.InvariantCultureIgnoreCase));
        }

        if (filterDto.Archived.HasValue)
        {
            query = query.Where(q => q.Archived == filterDto.Archived.Value);
        }
        
        if (filterDto.IsAnswered.HasValue)
        {
            query = query.Where(q => q.Archived == filterDto.IsAnswered.Value);
        }

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Question>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken,
        bool includeRelated = true,
        bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(q => q.Topic)
                .Include(q => q.Author);
        
        return await query.Where(q => q.QuestionTags.Any(qt => qt.Tag.Name == tagName))
            .ToListAsync(cancellationToken);
    }
}