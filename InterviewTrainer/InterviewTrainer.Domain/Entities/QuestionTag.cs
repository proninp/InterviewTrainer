using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class QuestionTag(long questionId, long tagId)
{
    public long QuestionId { get; set; } = questionId;

    public Question Question { get; set; } = null!;

    public long TagId { get; set; } = tagId;
    
    public Tag Tag { get; set; } = null!;
}