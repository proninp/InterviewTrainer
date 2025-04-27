using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class SuggestedAnswer(Guid questionId, string answer) : IdentityModel
{
    public Guid QuestionId { get; set; } = questionId;

    public Question Question { get; set; } = null!;
    
    public string Answer { get; set; } = answer;
}