using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IRepository<T> 
    where T : IdentityModel
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false);
    
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
    
    Task<T> AddAsync(T entity);
    
    Task AddRangeAsync(ICollection<T> entities);
    
    void Update(T entity);
    
    bool Delete(Guid id);
    
    bool Delete(T entity);
}