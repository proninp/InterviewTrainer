namespace InterviewTrainer.Application.DTOs.Topic;

public record TopicDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public bool Archived { get; init; }

    public List<Guid> TechnologyIds { get; init; } = [];

    public TopicDto(Guid id, string name, bool archived)
    {
        Id = id;
        Name = name;
        Archived = archived;
    }
}