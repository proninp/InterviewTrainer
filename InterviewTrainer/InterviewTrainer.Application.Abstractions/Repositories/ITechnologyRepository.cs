using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Technologies;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface ITechnologyRepository : IRepository<Technology>
{
    Task<bool> NameExistsAsync(string name, long? excludeTechnologyId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Technology>> GetPagedAsync(TechnologyFilterDto filterDto, CancellationToken cancellationToken);
}