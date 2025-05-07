using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Tags;

public record TagDto(long Id, string Name)
{
    public List<long> QuestionIds { get; init; } = [];
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