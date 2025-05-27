using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Contracts.SuggestedAnswers;
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Infrastructure.EntityFramework;
using InterviewTrainer.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Implementations;

public class SuggestedAnswerRepository(DatabaseContext context)
    : BaseRepository<SuggestedAnswer>(context), ISuggestedAnswerRepository
{
    public override async Task<SuggestedAnswer?> GetAsync(long id, CancellationToken cancellationToken,
        bool includeRelated = true, bool disableTracking = false)
    {
        var query = Entities.AsQueryable();

        if (disableTracking)
            query = query.AsNoTrackingWithIdentityResolution();

        if (includeRelated)
            query = query
                .Include(sa => sa.Question);

        return await query.FirstOrDefaultAsync(sa => sa.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SuggestedAnswer>> GetByQuestionIdAsync(long questionId,
        CancellationToken cancellationToken,
        bool includeRelated = true,
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
                .Include(sa => sa.Question);
        }

        return await query
            .Where(sa => sa.Question.Id == questionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SuggestedAnswer>> GetPagedAsync(SuggestedAnswerFilterDto filterDto,
        CancellationToken cancellationToken)
    {
        var query = Entities.AsNoTracking();

        if (filterDto.QuestionId.HasValue)
        {
            query = query.Where(sa => sa.QuestionId == filterDto.QuestionId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filterDto.Answer))
        {
            query = query.Where(sa =>
                sa.Answer.Contains(filterDto.Answer, StringComparison.InvariantCultureIgnoreCase));
        }

        var skip = (filterDto.Page - 1) * filterDto.ItemsPerPage;
        query = query.Skip(skip).Take(filterDto.ItemsPerPage);

        return await query.ToListAsync(cancellationToken);
    }
}