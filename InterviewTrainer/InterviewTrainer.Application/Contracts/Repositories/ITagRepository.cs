using InterviewTrainer.Application.DTOs.Tags;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetPagedAsync(TagFilterDto filterDto, CancellationToken cancellationToken);
    
    Task<bool> ExistsByNameAsync(string name, Guid? excludeTagId, CancellationToken cancellationToken);
}