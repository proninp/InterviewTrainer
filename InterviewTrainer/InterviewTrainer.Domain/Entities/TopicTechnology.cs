using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class TopicTechnology(Guid technologyId, Guid topicId)
{
    public Guid TechnologyId { get; set; } = technologyId;

    public Technology Technology { get; set; } = null!;

    public Guid TopicId { get; set; } = topicId;

    public Topic Topic { get; set; } = null!;
}