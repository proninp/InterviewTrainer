using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.Contracts.Questions;

public record QuestionDto(
    Guid Id,
    Guid TopicId,
    Difficulty Difficulty,
    QuestionStatus Status,
    string Text,
    string? Answer,
    bool Archived = false)
{
    public List<Guid> TagIds { get; init; } = [];
}

public static class QuestionDtoExtension
{
    public static QuestionDto ToDto(this Question question)
    {
        return new QuestionDto(question.Id, question.TopicId, question.Difficulty, question.Status, question.Text,
            question.Answer,
            question.Archived)
        {
            TagIds = question.QuestionTags.Select(t => t.TagId).ToList()
        };
    }
}