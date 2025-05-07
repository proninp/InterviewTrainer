using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class TopicTechnology(long technologyId, long topicId)
{
    public long TechnologyId { get; set; } = technologyId;

    public Technology Technology { get; set; } = null!;

    public long TopicId { get; set; } = topicId;

    public Topic Topic { get; set; } = null!;
}