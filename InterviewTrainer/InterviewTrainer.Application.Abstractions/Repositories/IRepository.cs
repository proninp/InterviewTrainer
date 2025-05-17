using System.Linq.Expressions;
using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IRepository<T> 
    where T : IdentityModel
{
    Task<bool> AnyAsync(long id, CancellationToken cancellationToken);
    
    Task<T?> GetAsync(long id, CancellationToken cancellationToken, bool isInclude = true, bool asNoTracking = false);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);
    
    void Update(T entity);
    
    void UpdatePartial(T entity, params Expression<Func<T, object>>[] properties);
    
    //public void UpdatePartial(Question question, params Expression<Func<Question, object>>[] updatedProperties)
    // {
    //     _dbContext.Questions.Attach(question);
    //
    //     foreach (var property in updatedProperties)
    //     {
    //         _dbContext.Entry(question).Property(property).IsModified = true;
    //     }
    // }
    
    Task<bool> TryDeleteAsync(long id, CancellationToken cancellationToken);
}