using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Topics;

public record TopicDto(Guid Id, string Name, bool? Archived = null)
{
    public List<Guid> TechnologyIds { get; init; } = [];
}

public static class TopicDtoExtension
{
    public static TopicDto ToDto(this Topic topic)
    {
        return new TopicDto(topic.Id, topic.Name, topic.Archived)
        {
            TechnologyIds = topic.TopicTechnologies.Select(tt => tt.TechnologyId).ToList()
        };
    }
}