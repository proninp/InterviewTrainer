using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.Contracts.Questions;

public record UpdateQuestionDto(
    long Id,
    long? TopicId = null,
    Difficulty? Difficulty = null,
    QuestionStatus? Status = null,
    string? Text = null,
    string? Answer = null,
    bool? Archived = null);