using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class SuggestedAnswer(Guid suggestedQuestionId, string answer) : IdentityModel
{
    public Guid SuggestedQuestionId { get; set; } = suggestedQuestionId;

    public SuggestedQuestion SuggestedQuestion { get; set; } = null!;
    
    public string Answer { get; set; } = answer;
}