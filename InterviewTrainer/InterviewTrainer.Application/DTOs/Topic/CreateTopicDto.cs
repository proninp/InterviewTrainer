namespace InterviewTrainer.Application.DTOs.Topic;

public record CreateTopicDto
{
    public string Name { get; init; }
    
    public bool Archived { get; init; }

    public List<Guid> TechnologyIds { get; init; } = [];

    public CreateTopicDto(Guid id, string name, bool archived)
    {
        Name = name;
        Archived = archived;
    }
}