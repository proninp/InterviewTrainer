namespace InterviewTrainer.Application.DTOs.Technologies;

public record CreateTechnologyDto(string Name, bool Archived = false)
{
    public List<Guid> TopicIds { get; init; } = [];
}