using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Technologies;

public record TechnologyDto(Guid Id, string Name, bool Archived)
{
    public List<Guid> TopicIds { get; init; } = [];
}

public static class TechnologyDtoExtension
{
    public static TechnologyDto ToDto(this Technology technology)
    {
        return new TechnologyDto(technology.Id, technology.Name, technology.Archived)
        {
            TopicIds = technology.TopicTechnologies.Select(tt => tt.TopicId).ToList()
        };
    }
}