using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IRepository<T> 
    where T : IdentityModel
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false);
    
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
    
    Task<T> GetOrThrowAsync(Guid id, CancellationToken cancellationToken);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);
    
    void Update(T entity);
    
    bool Delete(Guid id);
    
    bool Delete(T entity);
}