using InterviewTrainer.Application.DTOs.Technologies;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITechnologyRepository : IRepository<Technology>
{
    Task<IEnumerable<Topic>> GetPagedAsync(TechnologyFilterDto filterDto, CancellationToken cancellationToken);
}