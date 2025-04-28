namespace InterviewTrainer.Application.DTOs.Technologies;

public record CreateTechnologyDto
{
    public string Name { get; init; }
    
    public bool Archived { get; init; }

    public List<Guid> TopicIds { get; init; } = [];

    public CreateTechnologyDto(Guid id, string name, bool archived)
    {
        Name = name;
        Archived = archived;
    }
}