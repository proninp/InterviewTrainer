using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class SuggestedAnswer(long questionId, string answer) : IdentityModel
{
    public long QuestionId { get; set; } = questionId;

    public Question Question { get; set; } = null!;
    
    public string Answer { get; set; } = answer;
}