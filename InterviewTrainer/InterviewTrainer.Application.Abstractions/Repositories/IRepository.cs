using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface IRepository<T> 
    where T : IdentityModel
{
    Task<T?> GetAsync(long id, CancellationToken cancellationToken);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);
    
    void Update(T entity);
    
    Task<bool> TryDeleteAsync(long id, CancellationToken cancellationToken);
}