using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Contracts.Tags;

namespace InterviewTrainer.Application.Abstractions.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetPagedAsync(TagFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<bool> ExistsByNameAsync(string name, long? excludeTagId, CancellationToken cancellationToken);
}