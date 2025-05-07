using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IRepository<T> 
    where T : IdentityModel
{
    Task<T?> GetAsync(long id, CancellationToken cancellationToken);
    
    Task<T> GetOrThrowAsync(long id, CancellationToken cancellationToken);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);
    
    void Update(T entity);
    
    bool Delete(long id);
    
    bool Delete(T entity);
}