namespace InterviewTrainer.Application.DTOs.Technologies;

public record TechnologyDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public bool Archived { get; init; }

    public List<Guid> TopicIds { get; init; } = [];

    public TechnologyDto(Guid id, string name, bool archived)
    {
        Id = id;
        Name = name;
        Archived = archived;
    }
}