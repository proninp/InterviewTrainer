using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class QuestionTags(Guid questionId, Guid tagId)
{
    public Guid QuestionId { get; set; } = questionId;

    public Question Question { get; set; } = null!;

    public Guid TagId { get; set; } = tagId;
    
    public Tag Tag { get; set; } = null!;
}