namespace InterviewTrainer.Application.DTOs.Topic;

public record CreateTopicDto(string Name, bool Archived = false)
{
    public List<Guid> TechnologyIds { get; init; } = [];
}