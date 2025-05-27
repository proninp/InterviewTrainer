using System.Linq.Expressions;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Domain.Abstractions;
using InterviewTrainer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrainer.Infrastructure.Repositories.Abstractions;

public abstract class BaseRepository<T>(DatabaseContext context) : IRepository<T>
    where T : IdentityModel, new()
{
    private protected readonly DbSet<T> Entities = context.Set<T>();

    public async Task<bool> AnyAsync(long id, CancellationToken cancellationToken)
    {
        return await Entities
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public abstract Task<T?> GetAsync(long id, CancellationToken cancellationToken, bool includeRelated = true,
        bool disableTracking = false);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        var resultAdd = await Entities.AddAsync(entity, cancellationToken);
        return resultAdd.Entity;
    }

    public async Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken)
    {
        var enumerable = entities as List<T> ?? entities.ToList();
        await Entities.AddRangeAsync(enumerable, cancellationToken);
    }

    public void Update(T entity)
    {
        _ = Entities.Update(entity);
    }

    public void UpdatePartial(T entity, params Expression<Func<T, object>>[] properties)
    {
        context.Attach(entity);
        foreach (var property in properties)
        {
            context.Entry(entity).Property(property).IsModified = true;
        }
    }

    public void Delete(long id)
    {
        var entity = new T { Id = id };
        context.Entry(entity).State = EntityState.Deleted;
    }
}