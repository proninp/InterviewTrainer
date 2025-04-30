namespace InterviewTrainer.Application.DTOs.Technologies;

public record TechnologyDto(Guid Id, string Name, bool Archived)
{
    public List<Guid> TopicIds { get; init; } = [];
}