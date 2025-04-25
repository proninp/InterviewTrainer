using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class SuggestedQuestion(Guid topicId, Guid difficultyId, string questionText) : IdentityModel
{
    public Guid TopicId { get; set; } = topicId;

    public Topic Topic { get; set; } = null!;

    public Guid DifficultyId { get; set; } = difficultyId;

    public Difficulty Difficulty { get; set; } = null!;

    public string QuestionText { get; set; } = questionText;
}