using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class QuestionTag(Guid questionId, string tag) : IdentityModel
{
    public Guid QuestionId { get; set; } = questionId;

    public Question Question { get; set; } = null!;

    public string Tag { get; set; } = tag;
}