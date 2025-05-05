using InterviewTrainer.Application.DTOs.Technologies;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITechnologyRepository : IRepository<Technology>
{
    Task<bool> NameExistsAsync(string name, Guid? excludeTechnologyId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Technology>> GetPagedAsync(TechnologyFilterDto filterDto, CancellationToken cancellationToken);
}