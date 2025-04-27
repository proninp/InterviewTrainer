using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.DTOs.Questions;

public record UpdateQuestionDto(
    Guid QuestionId,
    Guid? TopicId = null,
    Difficulty? Difficulty = null,
    QuestionStatus? Status = null,
    string? Text = null,
    string? Answer = null,
    bool Archive = false);