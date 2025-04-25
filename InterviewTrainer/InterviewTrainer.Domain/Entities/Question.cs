using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Question(Guid topicId, Guid difficultyId, string text, string answer, bool archive = false)
    : IdentityModel
{
    public Guid TopicId { get; set; } = topicId;

    public Topic Topic { get; set; } = null!;

    public Guid DifficultyId { get; set; } = difficultyId;

    public Difficulty Difficulty { get; set; } = null!;

    public string Text { get; set; } = text;

    public string Answer { get; set; } = answer;

    public bool Archive { get; set; } = archive;
}