namespace InterviewTrainer.Application.DTOs.Topic;

public record TopicDto(Guid Id, string Name, bool Archived = false)
{
    public List<Guid> TechnologyIds { get; init; } = [];
}