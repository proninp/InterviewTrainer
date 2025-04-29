namespace InterviewTrainer.Application.DTOs.Tags;

public record TagDto(Guid Id, string Name)
{
    public List<Guid> QuestionIds { get; init; } = [];
}