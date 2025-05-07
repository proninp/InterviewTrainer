using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Technologies;

public record TechnologyDto(long Id, string Name, bool Archived)
{
    public List<long> TopicIds { get; init; } = [];
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