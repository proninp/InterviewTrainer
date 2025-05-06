using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Tags;

public record TagDto(Guid Id, string Name)
{
    public List<Guid> QuestionIds { get; init; } = [];
}

public static class TagDtoExtension
{
    public static TagDto ToDto(this Tag tag)
    {
        return new TagDto(tag.Id, tag.Name)
        {
            QuestionIds = tag.QuestionTags.Select(qt => qt.QuestionId).ToList()
        };
    }
}