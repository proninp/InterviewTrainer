namespace InterviewTrainer.Application.DTOs.Questions;

public record UpdateQuestionDto(
    Guid QuestionId,
    Guid? TopicId = null,
    Guid? DifficultyId = null,
    string? Text = null,
    string? Answer = null,
    bool Archive = false);