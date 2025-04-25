using InterviewTrainer.Application.DTOs.Topic;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Repositories;

public interface ITopicRepository : IRepository<Topic>
{
    Task<IEnumerable<Topic>> GetPagedAsync(TopicFilterDto filterDto, CancellationToken cancellationToken);
}